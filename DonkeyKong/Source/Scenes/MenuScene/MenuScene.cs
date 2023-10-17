using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonkeyKong.Source.Engine;
using DonkeyKong.Source.Managers;
using DonkeyKong.Source.Scenes.GameScene;

namespace DonkeyKong.Source.Scenes.MenuScene
{
    internal class MenuScene : Component
    {
        private Texture2D bg;
        

        internal override void LoadContent(ContentManager content)
        {
            bg = content.Load<Texture2D>("Sprites\\start");
        }

        internal override void Update(GameTime gameTime)
        {
            if (KeyMouseReader.KeyPressed(Keys.Space))
            {
                GameStateManager.State = GameStateManager.GameState.Game;
                MediaPlayer.Stop();
                MediaPlayer.Play(GameStateManager.stageSong);
            }
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bg, Vector2.Zero, Color.White);
        }
    }
}
