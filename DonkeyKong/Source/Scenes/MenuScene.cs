using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonkeyKong.Source.Engine;
using DonkeyKong.Source.Managers;

namespace DonkeyKong.Source.Scenes
{
    internal class MenuScene : Component
    {
        private const int BUTTONS_COUNT = 0;
        private Texture2D[] buttons = new Texture2D[BUTTONS_COUNT];
        private Rectangle[] buttonsBounds = new Rectangle[BUTTONS_COUNT];

        private MouseState mouseState, oldMouseState;
        private Rectangle mouseRect;

        internal override void LoadContent(ContentManager content)
        {
            // CREATE MENU BUTTONS
            const int INCREMENT = 200;
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = content.Load<Texture2D>($"button{i}");
                buttonsBounds[i] = new Rectangle(0, 0 + (INCREMENT * i), buttons[i].Width, buttons[i].Height);
            }
        }

        internal override void Update(GameTime gameTime)
        {
            // GET MOUSE STATE
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
            mouseRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            if (mouseState.LeftButton == ButtonState.Pressed && mouseRect.Intersects(buttonsBounds[0]))
            {
                GameStateManager.State = GameStateManager.GameState.Game;
            }
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            // DRAW MENU BUTTONS
            for (int i = 0; i < buttons.Length;i++)
            {
                spriteBatch.Draw(buttons[i], buttonsBounds[i], Color.White);
                if (mouseRect.Intersects(buttonsBounds[i]))
                {
                    spriteBatch.Draw(buttons[i], buttonsBounds[i], Color.LightGray);
                }
            }
        }
    }
}
