using DonkeyKong.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonkeyKong.Source.Scenes.GameScene;
using DonkeyKong.Source.Scenes.MenuScene;
using DonkeyKong.Source.Scenes.LostScene;
using DonkeyKong.Source.Scenes.WinScene;

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
        public static GameState State { get; set; } = GameState.Menu;

        private MenuScene menuScene = new MenuScene();
        private GameScene gameScene = new GameScene();
        private LostScene lostScene = new LostScene();
        private WinScene winScene = new WinScene();


        internal override void LoadContent(ContentManager content)
        {
            menuScene.LoadContent(content);
            gameScene.LoadContent(content);
            lostScene.LoadContent(content);
            winScene.LoadContent(content);
        }

        internal override void Update(GameTime gameTime)
        {
            KeyMouseReader.Update();
            switch (State)
            {
                case GameState.Menu:
                    menuScene.Update(gameTime);
                    KeyMouseReader.Update();
                    break;
                case GameState.Game:
                    gameScene.Update(gameTime);
                    break;
                case GameState.Settings:
                    break;
                case GameState.Lost:
                    lostScene.Update(gameTime);
                    break;
                case GameState.Won:
                    winScene.Update(gameTime);
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
                    lostScene.Draw(spriteBatch);
                    break;
                case GameState.Won:
                    winScene.Draw(spriteBatch);
                    break;
            }
        }
    }
}
