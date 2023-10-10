using DonkeyKong.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.Source.Scenes.GameScene.Components
{
    internal class Enemy : Component
    {
        Texture2D texture;
        Vector2 position;
        float speed = 50;

        Random random = new Random();
        GameTime gameTime;

        bool isMoving;

        int frameWidth = 32;
        int frameHeight = 32;
        int frame = 0;
        float frameInterval = 0.1f;  // Animation speed
        double frameTimer = 0;

        public Enemy(Vector2 pos)
        {
            position = pos;
            texture = TextureHandler.texFire;
        }
        internal override void LoadContent(ContentManager content)
        {
        }

        internal override void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            MoveToTarget(gameTime);

        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle srcRect = new Rectangle(frame * frameWidth, 0, frameWidth, frameHeight);
            spriteBatch.Draw(texture, position, srcRect, Color.White);
        }
        public void MoveToTarget(GameTime gameTime)
        {
            float movementAmount = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            float nextPositionX = position.X + movementAmount;
            int nextTileX = (int)((nextPositionX + (speed > 0 ? frameWidth / 2 : -frameWidth / 2)) / frameWidth);

            // Check if the next tile is walkable
            bool isNextTileWalkable = GameScene.GetTileAtPosition(new Vector2(nextTileX * frameWidth, position.Y));

            if (!isNextTileWalkable)
            {
                speed = -speed;
                position.X += movementAmount * 2 * Math.Sign(speed);
            }
            else
            {
                position.X = nextPositionX;
            }

            // Update the frame timer for the fire animations
            frameTimer = (frameTimer <= 0) ? frameInterval : frameTimer - (float)gameTime.ElapsedGameTime.TotalSeconds;
            frame = (frameTimer <= 0) ? 1 - frame : frame;
        }

    }
}
