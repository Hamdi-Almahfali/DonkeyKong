using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DonkeyKong.Source.Scenes.GameScene.Components
{
    internal class Button
    {
        Vector2 position;
        Texture2D texture;

        public bool isPressed;

        public Rectangle rect { get; private set; }

        public Button(Vector2 pos)
        {
            position = pos;
            texture = ContentLoader.texButton;
            isPressed = false;
            rect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
        public void Update(GameTime gameTime, Player player)
        {
            // Collect item if player collides with it
            if (rect.Intersects(player.GetRect()))
            {
                PressButton();
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle rect = new Rectangle();
            if (!isPressed)
                rect = new Rectangle(0, 0, 32, 32);
            else
                rect = new Rectangle(32, 0, 32, 32);
            spriteBatch.Draw(texture, position, rect, Color.White);
        }
        private void PressButton() // Turns the item into a collected one
        {
            isPressed = true;
        }
    }
}
