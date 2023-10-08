using DonkeyKong.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonkeyKong.Source.Scenes;

namespace DonkeyKong.Source.Managers
{
    internal class GameStateManager : Component
    {
        public enum GameState
        {
            Menu,
            Game,
            Settings,
            Lost,
            Won
        }
        public static GameState State { get; set; } = GameState.Game;

        private MenuScene menuScene = new MenuScene();
        private GameScene gameScene = new GameScene();

        internal override void LoadContent(ContentManager content)
        {
            menuScene.LoadContent(content);
            gameScene.LoadContent(content);
        }

        internal override void Update(GameTime gameTime)
        {
            switch (State)
            {
                case GameState.Menu:
                    menuScene.Update(gameTime);
                    break;
                case GameState.Game:
                    gameScene.Update(gameTime);
                    break;
                case GameState.Settings:
                    break;
                case GameState.Lost:
                    break;
                case GameState.Won:
                    break;
            }
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            switch (State)
            {
                case GameState.Menu:
                    menuScene.Draw(spriteBatch);
                    break;
                case GameState.Game:
                    gameScene.Draw(spriteBatch);
                    break;
                case GameState.Settings:
                    break;
                case GameState.Lost:
                    break;
                case GameState.Won:
                    break;
            }
        }
    }
}
