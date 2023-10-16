using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonkeyKong.Source.Engine;
using DonkeyKong.Source.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DonkeyKong.Source.Scenes.GameScene.Components
{
    internal class Umbrella
    {
        Texture2D texture;
        Vector2 position;

        private int score;
        private float displayTimer; 
        private bool displayScore;

        public bool collected { get; private set; }
        public Rectangle rect { get; private set; }

        public Umbrella(Vector2 position)
        {
            texture = TextureHandler.texUmbrella;
            this.position = position;
            score = 500;
            displayTimer = 0f;
            displayScore = false;
            rect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        internal void LoadContent(ContentManager content)
        {
        }

        internal void Update(GameTime gameTime, Player player, ScoreManager scoreManager)
        {
            if (displayScore)
            {
                displayTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Hide the score after 2 seconds
                if (displayTimer >= 2f)
                    displayScore = false;
            }
            // Collect item if player collides with it
            if (rect.Intersects(player.GetRect()) && !collected)
            {
                CollectItem(scoreManager);
            }
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            if (!collected)
                spriteBatch.Draw(texture, position, Color.White);
            if (displayScore)
            {
                Vector2 textSize = ScoreManager.font.MeasureString(score.ToString());
                Vector2 textPosition = position + new Vector2((texture.Width - textSize.X) / 2, (texture.Height - textSize.Y) / 2);

                // Drawing the score centered on the item
                spriteBatch.DrawString(ScoreManager.font, score.ToString(), textPosition, Color.White);
            }
        }
        public void CollectItem(ScoreManager scoreManager) // Turns the item into a collected one
        {
            collected = true;
            displayScore = true;
            scoreManager.UpdateCurrentScore(score);
            
        }
    }
}
