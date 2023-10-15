using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonkeyKong.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DonkeyKong.Source.Scenes.GameScene.Components
{
    internal class Hammer
    {
        Texture2D texture;
        Vector2 position;

        public bool collected { get; private set; }
        public Rectangle rect { get; private set; }

        public Hammer(Vector2 position)
        {
            texture = TextureHandler.texHammer;
            this.position = position;
            rect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        internal void LoadContent(ContentManager content)
        {
        }

        internal void Update(GameTime gameTime, Player player)
        {
            // Collect item if player collides with it
            if (rect.Intersects(player.GetRect()) && !collected)
            {
                CollectItem(player);
            }
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            if (!collected)
                spriteBatch.Draw(texture, position, Color.White);
        }
        private void CollectItem(Player player) // Turns the item into a collected one
        {
            if (player.isAttacking == false)
            {
                collected = true;
                player.ApplySuperPower();
            }
        }
    }
}
