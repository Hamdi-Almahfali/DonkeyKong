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
        private int currentScore;
        private List<int> highScores;
        public static SpriteFont font;

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
            Vector2 pos = new Vector2(210, 40);
            spriteBatch.DrawString(font, "Score " + currentScore.ToString(), pos, Color.White);
            spriteBatch.DrawString(font, "Highscores", new Vector2(32, 112), Color.Yellow);

            // Display up to the top 5 high scores
            int numScoresToDisplay = Math.Min(5, highScores.Count);
            for (int i = 0; i < numScoresToDisplay; i++)
            {
                if (i == 0)
                    spriteBatch.DrawString(font, "High Score " + highScores[i].ToString(), new Vector2(32, 144 + (20 * i)), Color.White);
                else
                    spriteBatch.DrawString(font, "Score " + highScores[i].ToString(), new Vector2(32, 144 + (20 * i)), Color.White);
            }
        }
    }

}
