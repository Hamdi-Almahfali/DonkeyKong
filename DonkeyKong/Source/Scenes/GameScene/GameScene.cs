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

namespace DonkeyKong.Source.Scenes.GameScene
{
    internal class GameScene : Component
    {
        public Player player;
        private Texture2D bg;

        public static Tile[,] tileArray;
        public static int tileSize = 32;
        public List<Enemy> enemyList;

        internal override void LoadContent(ContentManager content)
        {
            bg = content.Load<Texture2D>("Sprites\\background");
            enemyList = new List<Enemy>();

            CreateLevel("labyrint.txt", content);
        }

        internal override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            foreach (Enemy en in enemyList)
            {
                en.Update(gameTime);
            }
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bg, new Vector2(0, 0), Color.White);
            foreach (Tile t in tileArray)
            {
                t.Draw(spriteBatch);
            }
            foreach (Enemy en in enemyList)
            {
                en.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
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
            List<string> list = ReadFromFile("labyrint.txt");

            tileArray = new Tile[list[0].Length, list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[0].Length; j++)
                {
                    if (list[i][j] == 'x')
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), TextureHandler.texLadder, true, true);
                    }
                    else if (list[i][j] == '+')
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), TextureHandler.texAir, false, false);
                    }
                    else if (list[i][j] == '#')
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), TextureHandler.texCombined, true, true);
                    }
                    else if (list[i][j] == '=')
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), TextureHandler.texBridge, false, false);
                    }
                    else if (list[i][j] == '0')
                    {
                        Enemy enemy = new Enemy(new Vector2(j * tileSize, i * tileSize));
                        enemy.LoadContent(content);
                        enemyList.Add(enemy);
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), TextureHandler.texAir, true, false);
                    }
                    else if (list[i][j] == 'M')
                    {
                        player = new Player(new Vector2(j * tileSize, i * tileSize), 100);
                        player.LoadContent(content);
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), TextureHandler.texAir, true, false);
                    }
                    else
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), TextureHandler.texAir, true, false);
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
    }
}
