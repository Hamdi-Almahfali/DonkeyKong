using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonkeyKong.Source.Engine;
using DonkeyKong.Source.Managers;
using Microsoft.Xna.Framework.Input;
using Template.Source.Components;

namespace DonkeyKong.Source.Scenes
{
    internal class GameScene : Component
    {
        public Player player = new Player();
        private Texture2D bg;

        internal override void LoadContent(ContentManager content)
        {
            bg = content.Load<Texture2D>("Sprites\\background");
            player.LoadContent(content);
        }

        internal override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bg, new Vector2(0, 0), Color.White);
            player.Draw(spriteBatch);
        }
    }
}
