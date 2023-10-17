using DonkeyKong.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.Source.Scenes.GameScene.Components
{
    internal class WonPlayer : Component
    {
        Texture2D texture;
        Vector2 position;

        public WonPlayer(Vector2 pos)
        {
            position = pos;
            texture = ContentLoader.texLovers;
        }
        internal override void LoadContent(ContentManager content)
        {
        }

        internal override void Update(GameTime gameTime)
        {
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
