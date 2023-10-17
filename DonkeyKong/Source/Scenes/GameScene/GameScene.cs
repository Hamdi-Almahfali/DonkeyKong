using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonkeyKong.Source.Engine;
using DonkeyKong.Source.Managers;
using Microsoft.Xna.Framework.Input;
using DonkeyKong.Source.Scenes.GameScene.Components;
using System.IO;
using System.Reflection.Metadata;
using System.Diagnostics;
using Microsoft.Xna.Framework.Media;

namespace DonkeyKong.Source.Scenes.GameScene
{
    internal class GameScene : Component
    {
        public Player player;
        private HUD hud;
        private ScoreManager scoreManager;

        public const int maxHearts = 3;
        public static int currentHearts = maxHearts;

        public static int totalCements;

        public static Tile[,] tileArray;
        public static int tileSize = 32;

        private List<Enemy> enemyList;
        private List<Umbrella> pointlist;
        private List<Hammer> hammerList;
        private List<Cement> cementList;

        public Button button;
        public Barrel barrel;
        public DonkeyK donkeyKong;
        public Lover lover;

        private Texture2D bg;
        GameTime gameTime;

        Timer bonusTimer;
        public static int bonus;

        internal override void LoadContent(ContentManager content)
        {
            hud = new HUD();
            bg = content.Load<Texture2D>("Sprites\\background");
            scoreManager = new ScoreManager();
            scoreManager.LoadContent(content);

            enemyList = new List<Enemy>();
            pointlist = new List<Umbrella>(); // List that includes all the umberllas that mario can take to gain extra score
            hammerList = new List<Hammer>();
            cementList = new List<Cement>();

            CreateLevel("level0.txt", content);

            bonus = 4;
            bonusTimer = new Timer();
            bonusTimer.ResetAndStart(18.0f);
        }

        internal override void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            player.Update(gameTime);
            hud.Update(gameTime, currentHearts);
            bonusTimer.Update(gameTime);
            if (bonusTimer.IsDone())
            {
                bonus--;
                bonus = Math.Max(1, bonus);
                bonusTimer.ResetAndStart(12.0f);
            }

            foreach (Enemy en in enemyList)
            {
                en.Update(gameTime, player, scoreManager);
            }
            foreach (Hammer hammer in hammerList)
            {
                hammer.Update(gameTime, player);
            }
            foreach (Umbrella um in pointlist)
            {
                um.Update(gameTime, player, scoreManager);
            }
            foreach (Cement cem in cementList)
            {
                cem.Update(player);
            }
            button.Update(gameTime, player);
            barrel.Update(gameTime, button);
            donkeyKong.Update();

            if (totalCements <= 0)
                WinGame();
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bg, new Vector2(0, 0), Color.Black);
            foreach (Tile t in tileArray)
            {
                t.Draw(spriteBatch);
            }
            foreach (Umbrella point in pointlist)
            {
                point.Draw(spriteBatch);
            }
            foreach (Hammer hammer in hammerList)
            {
                hammer.Draw(spriteBatch);
            }
            foreach (Cement cem in cementList)
            {
                cem.Draw(spriteBatch);
            }
            foreach (Enemy en in enemyList)
            {
                en.Draw(spriteBatch);
            }
            button.Draw(spriteBatch);
            barrel.Draw(spriteBatch);

            donkeyKong.Draw(spriteBatch);
            player.Draw(spriteBatch);
            lover.Draw(spriteBatch);

            hud.Draw(spriteBatch);
            scoreManager.Draw(spriteBatch);
            // Draw bonus points
            Vector2 bonusPos = new Vector2(32, 176);
            spriteBatch.Draw(ContentLoader.texBonus, bonusPos, Color.White);
            spriteBatch.DrawString(ScoreManager.font, GameScene.bonus.ToString() + "x", new Vector2(bonusPos.X + 25, bonusPos.Y + 15), Color.White);
        }
        public List<string> ReadFromFile(string fileName)
        {
            StreamReader streamReader = new StreamReader(fileName);
            List<string> result = new List<string>();

            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                result.Add(line);
                Debug.WriteLine(line);
            }
            streamReader.Close();
            return result;
        } // Read from input file
        public void CreateLevel(string fileName, ContentManager content) // Create level from a file to read from
        {
            List<string> list = ReadFromFile("level0.txt");

            tileArray = new Tile[list[0].Length, list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[0].Length; j++)
                {
                    if (list[i][j] == 'H') // Ladder tile
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), ContentLoader.texLadder, true, true);
                    }
                    else if (list[i][j] == '+') // Nothing tile
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), ContentLoader.texAir, false, false);
                    }
                    else if (list[i][j] == '#') // Platform + ladder tile
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), ContentLoader.texCombined, true, true);
                    }
                    else if (list[i][j] == '=') // Platform tile
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), ContentLoader.texBridge, false, false);
                    }
                    else if (list[i][j] == 'J') // Umbrella item
                    {
                        Umbrella point = new Umbrella(new Vector2(j * tileSize, i * tileSize));
                        pointlist.Add(point);
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), ContentLoader.texAir, true, false);
                    }
                    else if (list[i][j] == 'T') // Hammer item
                    {
                        Hammer hammer = new Hammer(new Vector2(j * tileSize, i * tileSize));
                        hammerList.Add(hammer);
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), ContentLoader.texAir, true, false);
                    }
                    else if (list[i][j] == '0') // Enemy fire
                    {
                        Enemy enemy = new Enemy(new Vector2(j * tileSize, i * tileSize), i);
                        enemy.LoadContent(content);
                        enemyList.Add(enemy);
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), ContentLoader.texAir, true, false);
                    }
                    else if (list[i][j] == 'z') // Cement / pie item
                    {
                        Cement cement = new Cement(new Vector2(j * tileSize, i * tileSize));
                        cementList.Add(cement);
                        totalCements++;
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), ContentLoader.texAir, true, false);
                    }
                    else if (list[i][j] == '5') // Button object
                    {
                        button = new Button(new Vector2(j * tileSize, i * tileSize));
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), ContentLoader.texAir, true, false);
                    }
                    else if (list[i][j] == 'G') // Barrel object
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), ContentLoader.texAir, false, false);
                        barrel = new Barrel(new Vector2(j * tileSize, i * tileSize), tileArray[j,i]);
                    }
                    else if (list[i][j] == 'D') // Donkey Kong
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), ContentLoader.texAir, false, false);
                        donkeyKong = new DonkeyK(new Vector2(j * tileSize, i * tileSize));

                    }
                    else if (list[i][j] == 'p') // Lover
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), ContentLoader.texAir, false, false);
                        lover = new Lover(new Vector2(j * tileSize, i * tileSize));

                    }
                    else if (list[i][j] == 'M') // Mario's location
                    {
                        player = new Player(new Vector2(j * tileSize, i * tileSize), 150);
                        player.LoadContent(content);
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), ContentLoader.texAir, true, false);
                    }
                    else // Air / nothing
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), ContentLoader.texAir, true, false);
                    }
                }

            }
        }
        public static bool GetTileAtPosition(Vector2 pos) // Returns true or false if the tile at specified position is walkable or not
        {
            return tileArray[(int)pos.X / tileSize, (int)pos.Y / tileSize].walkable;
        }
        public static bool GetLadderAtPosition(Vector2 pos) // Returns true or false if the tile at specified position is climbable or not
        {
            return tileArray[(int)pos.X / tileSize, (int)pos.Y / tileSize].isClimbable;
        }
        void WinGame()
        {
            scoreManager.UpdateHighScores();
            GameStateManager.State = GameStateManager.GameState.Won;
            MediaPlayer.Stop();
            MediaPlayer.Play(GameStateManager.winSong);
        }
    }
}
