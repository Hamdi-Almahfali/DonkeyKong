using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DonkeyKong.Source.Managers
{
    public class ScoreManager
    {
        public static int currentScore;
        private static List<int> highScores;
        public static SpriteFont font;
        int tileSize = 32;
        private string filePath = "highscores.txt";

        public int CurrentScore { get; private set; }

        public List<int> HighScores
        {
            get { return highScores; }
        }

        public ScoreManager()
        {
            highScores = new List<int>();
            LoadHighScores();
        }
        public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Font");
        }
        public void UpdateCurrentScore(int scoreToAdd)
        {
            currentScore += scoreToAdd;
        }

        public void LoadHighScores()
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (int.TryParse(line, out int score))
                    {
                        highScores.Add(score);
                    }
                }

                highScores.Sort();
                highScores.Reverse();
            }
        }

        public void UpdateHighScores()
        {
            highScores.Add(currentScore);
            highScores.Sort();
            highScores.Reverse();

            // Keep only the top 5 high scores
            highScores = highScores.Take(5).ToList();

            SaveHighScores();
        }

        public void SaveHighScores()
        {
            File.WriteAllLines(filePath, highScores.Select(score => score.ToString()));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 pos = new Vector2(32, 144);
            spriteBatch.DrawString(font, "Score " + currentScore.ToString(), pos, Color.White);

        }
        public void DisplayHighscores(SpriteBatch spriteBatch)
        {
            // Display up to the top 5 high scores
            int numScoresToDisplay = Math.Min(5, highScores.Count);
            for (int i = 0; i < numScoresToDisplay; i++)
            {
                if (i == 0)
                {
                    WriteText(spriteBatch, tileSize * 3, font, "Your score", Color.Yellow,Main.graphics.GraphicsDevice);
                    WriteText(spriteBatch, tileSize * 4, font, currentScore.ToString(), Color.White,Main.graphics.GraphicsDevice);
                    WriteText(spriteBatch, tileSize * 6, font, "Highscores", Color.Yellow,Main.graphics.GraphicsDevice);
                    WriteText(spriteBatch, tileSize * 7 + (20 * i), font, highScores[i].ToString(), Color.White, Main.graphics.GraphicsDevice);

                }
                else
                    WriteText(spriteBatch, tileSize * 7 + (20 * i), font, highScores[i].ToString(), Color.White, Main.graphics.GraphicsDevice);
            }
        }
        // Function to write text from the middle of the screen
        public void WriteText(SpriteBatch spriteBatch, float y, SpriteFont font, string text, Color color, GraphicsDevice graphicsDevice)
        {
            // Measure the size of the text
            Vector2 textSize = font.MeasureString(text);

            // Calculate the position to center the text
            float x = (graphicsDevice.Viewport.Width - textSize.X) / 2;

            // Iterate through the characters in the text and draw them
            for (int i = 0; i < text.Length; i++)
            {
                float alpha = (float)i / text.Length; // Alpha based on the position of the character
                Color characterColor = new Color(color.R, color.G, color.B, alpha);

                spriteBatch.DrawString(font, text[i].ToString(), new Vector2(x, y), characterColor);

                x += font.MeasureString(text[i].ToString()).X;
            }
        }
    }

}
