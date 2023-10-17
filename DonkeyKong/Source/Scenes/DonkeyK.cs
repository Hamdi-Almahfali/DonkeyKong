using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonkeyKong.Source.Scenes.GameScene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DonkeyKong.Source.Scenes
{
    internal class DonkeyK
    {
        Vector2 position;
        Texture2D texture;

        public bool isFalling;

        public DonkeyK(Vector2 pos)
        {
            position = pos;
            texture = ContentLoader.texDonkeyK;
            isFalling = false;
        }
        public void Update(GameTime gameTime)
        {
            if (isFalling && position.Y <= 440)
            {
                position.Y += 4;
            }
            else
            {
                WinScene.WinScene.displayScores = true;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle rect;
            if (!isFalling)
                rect = new Rectangle(0, 0, texture.Width / 2, texture.Height);
            else
                rect = new Rectangle(texture.Width/2, 0, texture.Width / 2, texture.Height);
            spriteBatch.Draw(texture, new Vector2(position.X - 25, position.Y), rect, Color.White);
        }
    }
}
