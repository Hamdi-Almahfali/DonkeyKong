﻿using DonkeyKong.Source.Managers;
using DonkeyKong.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace DonkeyKong.Source.Scenes.GameScene.Components
{
    internal class Enemy
    {
        Texture2D texture;
        Vector2 position;
        float speed = 50;
        private int direction = 1;

        Random random = new Random();
        Managers.Timer speedTimer;
        Managers.Timer AppearTimer;

        SoundEffectInstance sndInstanceDeath;
        SoundEffectInstance sndInstanceHit;

        bool hasAppeared;
        bool defeated;
        int speedChangeInterval = 3;
        float appearTime;

        int frameWidth = 32;
        int frameHeight = 32;
        int frame = 0;
        float frameInterval = 0.1f;  // Animation speed
        double frameTimer = 0;

        public Enemy(Vector2 pos, int time)
        {
            position = pos;
            texture = ContentLoader.texFire;
            appearTime = time;
            hasAppeared = false;
        }
        internal void LoadContent(ContentManager content)
        {
            speedTimer = new Managers.Timer();
            speedTimer.ResetAndStart(speedChangeInterval);

            AppearTimer = new Managers.Timer();
            AppearTimer.ResetAndStart(appearTime / 3);

            defeated = false;

            sndInstanceDeath = ContentLoader.sndFireDeath.CreateInstance();
            sndInstanceHit = ContentLoader.sndHit.CreateInstance();
        }

        internal void Update(GameTime gameTime, Player player, ScoreManager scoreManager)
        {
            if (!defeated && hasAppeared)
            {
                MoveToTarget(gameTime);
                speedTimer.Update(gameTime);
                if (speedTimer.IsDone())
                {
                    ChangeDirection();
                    speedTimer.ResetAndStart(speedChangeInterval);
                }
                CheckHammerCollision(player, scoreManager);
            }
            if (AppearTimer.IsDone())
            {
                hasAppeared = true;
            }
            else
            {
                AppearTimer.Update(gameTime);
            }
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            Rectangle srcRect = new Rectangle(frame * frameWidth, 0, frameWidth, frameHeight);
            if (!defeated && hasAppeared)
                spriteBatch.Draw(texture, position, srcRect, Color.White);
        }
        private void ChangeDirection()
        {
            int randomInt = random.Next(0, 2);
            if (randomInt > 0)
            {
                speed = 50;
            }
            else
            {
                speed = 100;
            }
        }
        /// <summary>
        /// Move to target, moves object to a tile based distance in a set speed
        /// </summary>
        /// <param name="gameTime"></param>
        public void MoveToTarget(GameTime gameTime)
        {
            float movementAmount = speed * direction * (float)gameTime.ElapsedGameTime.TotalSeconds;
            float nextPositionX = position.X + movementAmount;

            int nextTileX = (int)((nextPositionX + (direction > 0 ? frameWidth / 2 : -frameWidth / 2)) / frameWidth);

            // Check if the next tile is walkable
            bool isNextTileWalkable = GameScene.GetTileAtPosition(new Vector2(nextTileX * frameWidth, position.Y));

            if (!isNextTileWalkable)
            {
                direction = -direction;
                position.X += movementAmount * 2 * direction;
            }
            else
            {
                position.X = nextPositionX;
            }

            // Update the frame timer for the fire animations
            frameTimer = (frameTimer <= 0) ? frameInterval : frameTimer - (float)gameTime.ElapsedGameTime.TotalSeconds;
            frame = (frameTimer <= 0) ? 1 - frame : frame;
        }

        private Rectangle GetBounds()
        {
            Rectangle rect = new Rectangle((int)position.X, (int)position.Y, 32, 32);
            return rect;

        }
        private void CheckHammerCollision(Player player, ScoreManager scoreManager)
        {
            if (player.GetRect().Intersects(GetBounds()) && player.isAttacking)
            {
                Defeat(scoreManager);
            }
            else if (player.GetRect().Intersects(GetBounds()) && !player.isHit)
            {
                player.GetHit();
                ScoreManager.currentScore -= 150;
                sndInstanceHit.Play();
            }
        }
        private void Defeat(ScoreManager scoreManager)
        {
            if (!defeated)
            {
                scoreManager.UpdateCurrentScore(200);
                sndInstanceDeath.Play();
                defeated = true;
            }
        }

    }
}
