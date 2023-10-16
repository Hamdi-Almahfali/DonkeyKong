using DonkeyKong.Source.Managers;
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
        public enum State { Moving, Climbing, Attacking }
        public State state = State.Moving;

        Texture2D texture;
        Vector2 position;
        Vector2 direction;
        Vector2 destination;

        float speed;
        public Rectangle playerRect { get; private set; }
        public Rectangle attackRect { get; private set; }
        private Timer superPowerTimer;
        public Timer hitTimer;
        SpriteEffects spriteEffect = SpriteEffects.None; // To mirror mario

        public bool isAttacking;
        bool isMoving;
        bool isClimbing;
        public bool isHit;

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
            playerRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            attackRect = new Rectangle((int)position.X - texture.Width, (int)position.Y - texture.Height, texture.Width + texture.Width, texture.Height + texture.Height);
            superPowerTimer = new Timer();
            hitTimer = new Timer();
            isAttacking = false;
            isHit = false;
        }

        internal override void Update(GameTime gameTime)
        {
            // Update attack rectangle
            attackRect = new Rectangle((int)position.X - frameWidth, (int)position.Y - frameHeight, texture.Width, texture.Height - frameHeight);
            this.gameTime = gameTime;
            KeyMouseReader.Update();
            SuperPowerBehavior(gameTime);
            HitBehavior(gameTime);
            if (!isMoving)
            {
                if (KeyMouseReader.KeyPressed(Keys.W))
                {
                    ChangeClimbing(new Vector2(0, -1));
                }
                else if (KeyMouseReader.KeyPressed(Keys.A))
                {
                    ChangeDirection(new Vector2(-1, 0));
                    spriteEffect = SpriteEffects.None;
                }
                else if (KeyMouseReader.KeyPressed(Keys.S))
                {
                    ChangeClimbing(new Vector2(0, 1));
                }
                else if (KeyMouseReader.KeyPressed(Keys.D))
                {
                    ChangeDirection(new Vector2(1, 0));
                    spriteEffect = SpriteEffects.FlipHorizontally;
                }
            }
            else
            {
                position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Vector2.Distance(position, destination) < 1)
                {
                    position = destination;
                    isMoving = false;
                    if (!GameScene.GetLadderAtPosition(position))
                    {
                        state = State.Moving;
                        isClimbing = false;
                        frame = 0;
                    }
                    else
                    {
                        isClimbing = false;
                        frame = 0;
                    }
                }
            }
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle srcRect = ApplyTexture();
            if (!isHit)
                spriteBatch.Draw(texture, position, srcRect, Color.White, 0f, Vector2.Zero, 1f, spriteEffect, 0f);
            else
                spriteBatch.Draw(texture, position, srcRect, Color.Red, 0f, Vector2.Zero, 1f, spriteEffect, 0f);

            // Play animation of player attacking or stop
            if (isAttacking)
            {
                Rectangle hammerRect;
                if (frame == 0)
                {
                     hammerRect = new Rectangle(0, 0, frameWidth * 3, frameHeight * 3);
                }
                else if (frame != 0 && state != State.Climbing)
                {
                     hammerRect = new Rectangle(frameWidth * 3, 0, frameWidth * 3, frameHeight * 3);
                }
                else
                {
                    hammerRect = new Rectangle(0, 0, frameWidth * 3, frameHeight * 3);
                }
                spriteBatch.Draw(TextureHandler.texHammerAttack, position - new Vector2(frameWidth, frameHeight), hammerRect, Color.White, 0f, Vector2.Zero, 1f, spriteEffect, 0f);
            }
        }
        private void ChangeDirection(Vector2 dir)
        {
            direction = dir;
            Vector2 newDestination = position + direction * 32;

            if ((state == State.Moving && GameScene.GetTileAtPosition(newDestination)) || (state == State.Climbing && !GameScene.GetTileAtPosition(new Vector2(position.X, position.Y + 32))))
            {
                destination = newDestination;
                isMoving = true;
                state = State.Moving;
            }
        }
        private void ChangeClimbing(Vector2 dir)
        {
            direction = dir;
            Vector2 newDestination = position + direction * 32;

            if ((GameScene.GetTileAtPosition(newDestination) && GameScene.GetLadderAtPosition(position)) || (GameScene.GetLadderAtPosition(newDestination)))
            {
                destination = newDestination;
                isMoving = true;
                isClimbing = true;
                state = State.Climbing;
            }
        }
        private void ApplyFrames()
        {
            if (state == State.Moving && !isAttacking)
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
            else if (state == State.Moving && isAttacking)
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
        private Rectangle ApplyTexture()
        {
            Rectangle srcRect;

            if (state == State.Moving) // If the character isnt in a state of climbing
            {
                if (!isMoving && !isAttacking) // if the character is standing still
                    srcRect = new Rectangle(0, 0, frameWidth, frameHeight);
                else if (!isAttacking) // if the characer is moving but not attacking
                {
                    srcRect = new Rectangle(frame * frameWidth, 0, frameWidth, frameHeight);
                    ApplyFrames();
                }
                else // if the character is attacking
                {
                    ApplyFrames();
                    srcRect = new Rectangle(frame * frameWidth, 64, frameWidth, frameHeight); // Apply hammer frames
                }
            }
            else if (state == State.Climbing)
            {
                if (!isClimbing) // if the character is on the ladder but isnt climbing
                    srcRect = new Rectangle(frame * frameWidth, 32, frameWidth, frameHeight);
                else // if the character is climbing
                {
                    ApplyFrames();
                    srcRect = new Rectangle(frame * frameWidth, 32, frameWidth, frameHeight);
                }
            }
            else
            {
                ApplyFrames();
                srcRect = new Rectangle(frame * frameWidth, 32, frameWidth, frameHeight);
            }

            return srcRect;
        }

        public Rectangle GetRect() // Returns the player's bounds
        {
            if (isAttacking)
            {
                return attackRect;
            }
            else
            {
                playerRect = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);
                return playerRect;
            }
            
        }
        public void ApplySuperPower()
        {
            superPowerTimer.ResetAndStart(7.0f);
            isAttacking = true;
        }
        private void SuperPowerBehavior(GameTime gameTime) // Manage all the behavior for the hammer
        {
            superPowerTimer.Update(gameTime);
            if (superPowerTimer.IsDone())
            {
                isAttacking = false;
                return;
            }
            
        }
        public void GetHit()
        {
            isHit = true;
            hitTimer.ResetAndStart(2.0f);
            GameScene.currentHearts--;
        }
        private void HitBehavior(GameTime gameTime)
        {
            hitTimer.Update(gameTime);
            if (hitTimer.IsDone())
            {
                isHit = false;
                return;
            }
        }
    }
}
