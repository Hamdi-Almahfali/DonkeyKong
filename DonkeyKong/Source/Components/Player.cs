using DonkeyKong.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Template.Source.Components
{
    internal class Player : Component
    {
        public enum State { Moving, Climbing}
        State state = State.Moving;

        Texture2D texture;
        Vector2 position;
        Vector2 targetPosition;
        Vector2 velocity;
        SpriteEffects spriteEffect = SpriteEffects.None; // To mirror mario

        private const int TileSize = 32;
        private const float MaxSpeed = TileSize / 16f;
        private const float Acceleration = TileSize / 32f;
        private const float Deceleration = TileSize / 8f;
        const float ClimbSpeed = 1f;
        bool isAttacking;

        int frameWidth = 32;
        int frameHeight = 32;
        int frame = 0;
        float frameInterval = 0.1f;  // Animation speed
        double frameTimer = 0;
        GameTime gameTime;


        internal override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Sprites\\marioMoving");
            position = new Vector2(0,256);
        }

        internal override void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            HandleInput();
            ApplyMovement();
            Debug.Print(position.X.ToString());
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle srcRect = ApplyTexture();
            spriteBatch.Draw(texture, position, srcRect, Color.White, 0f, Vector2.Zero, 1f, spriteEffect, 0f);
        }

        private void HandleInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (state == State.Moving)
            {
                if (keyboardState.IsKeyDown(Keys.A) && position.X % TileSize == 0)
                {
                    targetPosition.X = (float)(Math.Floor(position.X / TileSize) * TileSize) - TileSize;
                    spriteEffect = SpriteEffects.None;

                }
                else if (keyboardState.IsKeyDown(Keys.D) && position.X % TileSize == 0)
                {
                    targetPosition.X = (float)(Math.Ceiling(position.X / TileSize) * TileSize) + TileSize;
                    spriteEffect = SpriteEffects.FlipHorizontally;
                }


            }
            // Climbing behavior
            if (state == State.Climbing)
            {
                velocity.X = 0;
                spriteEffect = SpriteEffects.None;
                if (keyboardState.IsKeyDown(Keys.W))
                {
                    velocity.Y -= ClimbSpeed;
                }
                else if (keyboardState.IsKeyDown(Keys.S))
                {
                    velocity.Y += ClimbSpeed;
                }
                else
                {
                    velocity.Y = 0;
                }
            }
        }
        private void ApplyMovement()
        {
            float directionX = Math.Sign(targetPosition.X - position.X);
            KeyboardState keyboardState = Keyboard.GetState();

            // Apply acceleration
            velocity.X += directionX * Acceleration;

            // Limit speed
            if (Math.Abs(velocity.X) > MaxSpeed)
            {
                velocity.X = Math.Sign(velocity.X) * MaxSpeed;
            }

            // Apply velocity to the position
            position.X += velocity.X;

            // Snap to grid when close to the target position
            if (Math.Abs(targetPosition.X - position.X) < Math.Abs(velocity.X))
            {
                position.X = targetPosition.X;
                velocity.X = 0f;
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
            else if (velocity.Y != 0)
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
            Rectangle srcRect = new Rectangle(0,0,16,16);

            if (state == State.Moving && velocity.X == 0)
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
