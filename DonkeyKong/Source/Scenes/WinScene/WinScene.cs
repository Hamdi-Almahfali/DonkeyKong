using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonkeyKong.Source.Engine;
using DonkeyKong.Source.Managers;
using DonkeyKong.Source.Scenes.GameScene;
using DonkeyKong.Source.Scenes.GameScene.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DonkeyKong.Source.Scenes.WinScene
{
    internal class WinScene : Component
    {
        public static Tile[,] tileArray;
        public static int tileSize = 32;

        public DonkeyK donkeyKong;
        private ScoreManager scoreManager;

        public static bool displayScores;

        WonPlayer wonPlayer;


        internal override void LoadContent(ContentManager content)
        {
            scoreManager = new ScoreManager();
            CreateLevel("level1.txt", content);
            donkeyKong.isFalling = true;
        }

        internal override void Update(GameTime gameTime)
        {
            donkeyKong.Update();
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            foreach(Tile tile in  tileArray)
            {
                tile.Draw(spriteBatch);
            }
            donkeyKong.Draw(spriteBatch);
            wonPlayer.Draw(spriteBatch);
            scoreManager.DisplayHighscores(spriteBatch);
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
            List<string> list = ReadFromFile("level1.txt");

            tileArray = new Tile[list[0].Length, list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[0].Length; j++)
                {
                    if (list[i][j] == 'H') // Ladder tile
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), ContentLoader.texLadder, true, true);
                    }
                    else if (list[i][j] == '#') // Platform + ladder tile
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), ContentLoader.texCombined, true, true);
                    }
                    else if (list[i][j] == '=') // Platform tile
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), ContentLoader.texBridge, false, false);
                    }
                    else if (list[i][j] == 'D') // Donkey Kong
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), ContentLoader.texAir, false, false);
                        donkeyKong = new DonkeyK(new Vector2(j * tileSize, i * tileSize));

                    }
                    else if (list[i][j] == 'm') // Donkey Kong
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), ContentLoader.texAir, false, false);
                        wonPlayer = new (new Vector2(j * tileSize, i * tileSize));

                    }
                    else // Air / nothing
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), ContentLoader.texAir, true, false);
                    }
                }

            }
        }
    }
}
