using DonkeyKong.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace DonkeyKong.Source.Scenes.GameScene.Components
{
    internal class Player : Component
    {
        public enum State { Moving, Climbing }
        State state = State.Moving;

        Texture2D texture;
        Vector2 position;
        Vector2 direction;
        Vector2 destination;
        float speed;
        SpriteEffects spriteEffect = SpriteEffects.None; // To mirror mario

        bool isAttacking;
        bool isMoving;

        int frameWidth = 32;
        int frameHeight = 32;
        int frame = 0;
        float frameInterval = 0.1f;  // Animation speed
        double frameTimer = 0;
        GameTime gameTime;

        public Player(Vector2 pos, float speed)
        {
            position = pos;
            direction = new Vector2(0, 0);
            this.speed = speed;
            isMoving = false;
            destination = Vector2.Zero;
        }

        internal override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Sprites\\marioMoving");
        }

        internal override void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            KeyMouseReader.Update();
            if (!isMoving)
            {
                if (KeyMouseReader.KeyPressed(Keys.Up))
                {
                    ChangeDirection(new Vector2(0, -1));
                }
                else if (KeyMouseReader.KeyPressed(Keys.Left))
                {
                    ChangeDirection(new Vector2(-1, 0));
                }
                else if (KeyMouseReader.KeyPressed(Keys.Down))
                {
                    ChangeDirection(new Vector2(0, 1));
                }
                else if (KeyMouseReader.KeyPressed(Keys.Right))
                {
                    ChangeDirection(new Vector2(1, 0));
                }
            }
            else
            {
                position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Vector2.Distance(position, destination) < 1)
                {
                    position = destination;
                    isMoving = false;
                }
            }
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle srcRect = ApplyTexture();
            spriteBatch.Draw(texture, position, srcRect, Color.White, 0f, Vector2.Zero, 1f, spriteEffect, 0f);
        }
        public void ChangeDirection(Vector2 dir)
        {
            direction = dir;
            Vector2 newDestination = position + direction * 32;

            if (!GameScene.GetTileAtPosition(newDestination))
            {
                destination = newDestination;
                isMoving = true;
            }
        }
        private void ApplyFrames()
        {
            if (state == State.Moving)
            {
                frameTimer -= gameTime.ElapsedGameTime.TotalSeconds;

                // If enough time has passed for the next frame
                if (frameTimer <= 0)
                {
                    frameTimer = frameInterval;
                    frame++;
                    if (frame > 2)
                    {
                        frame = 0;
                    }
                }
            }
            else if (state == State.Climbing)
            {
                frameTimer -= gameTime.ElapsedGameTime.TotalSeconds / 2;

                // If enough time has passed for the next frame
                if (frameTimer <= 0)
                {
                    frameTimer = frameInterval;
                    frame++;
                    if (frame > 1)
                    {
                        frame = 0;
                    }
                }
            }

        }
        public Rectangle ApplyTexture()
        {
            Rectangle srcRect = new Rectangle(0, 0, 16, 16);

            if (state == State.Moving && !isMoving)
            {
                srcRect = new Rectangle(0, 0, frameWidth, frameHeight);
            }
            else if (state == State.Moving)
            {
                ApplyFrames();
                srcRect = new Rectangle(frame * frameWidth, 0, frameWidth, frameHeight);
            }
            else
            {
                ApplyFrames();
                srcRect = new Rectangle(frame * frameWidth, 32, frameWidth, frameHeight);
            }
            return srcRect;
        }
    }
}
