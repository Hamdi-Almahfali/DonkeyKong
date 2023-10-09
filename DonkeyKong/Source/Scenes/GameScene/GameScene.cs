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

        internal override void LoadContent(ContentManager content)
        {
            bg = content.Load<Texture2D>("Sprites\\background");

            CreateLevel("labyrint.txt");
            player = new Player(new Vector2(6 * tileSize, 14 * tileSize), 80);
            player.LoadContent(content);
        }

        internal override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bg, new Vector2(0, 0), Color.White);
            foreach (Tile t in tileArray)
            {
                t.Draw(spriteBatch);
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
        }
        public void CreateLevel(string fileName)
        {
            List<string> list = ReadFromFile("labyrint.txt");

            tileArray = new Tile[list[0].Length, list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[0].Length; j++)
                {
                    if (list[i][j] == 'x')
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), TextureHandler.texLadder, false);
                    }
                    else if (list[i][j] == '+')
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), TextureHandler.texBarrier, true);
                    }
                    else if (list[i][j] == '#')
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), TextureHandler.texCombined, false);
                    }
                    else if (list[i][j] == '=')
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), TextureHandler.texBridge, true);
                    }
                    else
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), TextureHandler.texAir, false);
                    }
                }

            }
        }
        public static bool GetTileAtPosition(Vector2 pos)
        {
            return tileArray[(int)pos.X / tileSize, (int)pos.Y / tileSize].notWalkable;
        }
    }
}
