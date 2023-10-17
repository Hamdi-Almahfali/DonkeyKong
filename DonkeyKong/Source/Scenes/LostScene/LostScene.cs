using DonkeyKong.Source.Engine;
using DonkeyKong.Source.Managers;
using DonkeyKong.Source.Scenes.GameScene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.Source.Scenes.LostScene
{
    internal class LostScene : Component
    {
        Texture2D bg;
        internal override void LoadContent(ContentManager content)
        {
            bg = content.Load<Texture2D>("Sprites\\lost");
        }

        internal override void Update(GameTime gameTime)
        {
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bg, Vector2.Zero, Color.White);
        }
    }
}
