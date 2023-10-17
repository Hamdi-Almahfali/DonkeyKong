using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DonkeyKong.Source.Managers;
using Microsoft.Xna.Framework.Audio;

namespace DonkeyKong.Source.Scenes.GameScene.Components
{
    internal class Barrel
    {
        Vector2 position;
        Texture2D texture;

        Tile tile;

        SoundEffectInstance sndInstance;
        Managers.Timer detonationTimer;

        public enum State { Safe, detonated, Exploded}
        State state;

        public Barrel(Vector2 pos, Tile tile)
        {
            this.tile = tile;
            position = pos;
            state = State.Safe;
            texture = ContentLoader.texBarrel;

            detonationTimer = new Managers.Timer();

            sndInstance = ContentLoader.sndExplode.CreateInstance();
        }
        public void Update(GameTime gameTime, Button button)
        {
            if (button.isPressed && state == State.Safe)
            {
                Detonate();
            }
            if (state == State.detonated && detonationTimer.IsDone())
            {
                Explode();
            }

            //Update timer
            if (state == State.detonated)
            {
                detonationTimer.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle rectangle = new Rectangle();
            switch (state)
            {
                case State.Safe:
                    rectangle = new Rectangle(0,0,32,64);
                    break;
                case State.detonated:
                    rectangle = new Rectangle(32, 0, 32, 64);
                    break;
                case State.Exploded:
                    rectangle = new Rectangle(0, 0, 0, 0);
                    break;
            }
            Vector2 drawPosition = new Vector2(position.X, position.Y - 32);
            spriteBatch.Draw(texture, drawPosition, rectangle, Color.White);
        }
        private void Detonate()
        {
            state = State.detonated;
            detonationTimer.ResetAndStart(2.0f);
        }
        private void Explode()
        {
            if (state == State.detonated)
            {
                state = State.Exploded;
                sndInstance.Play();
            }
            tile.walkable = true;
        }
    }
}
