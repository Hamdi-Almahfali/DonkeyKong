using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonkeyKong.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DonkeyKong.Source.Scenes.GameScene.Components
{
    internal class Hammer : Component
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

        internal override void LoadContent(ContentManager content)
        {
        }

        internal override void Update(GameTime gameTime)
        {
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            if (!collected)
                spriteBatch.Draw(texture, position, Color.White);
        }
        public void CollectItem() // Turns the item into a collected one
        {
            collected = true;
        }
    }
}
