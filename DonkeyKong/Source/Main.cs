using DonkeyKong.Source.Engine;
using DonkeyKong.Source.Managers;
using DonkeyKong.Source.Scenes.GameScene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DonkeyKong.Source
{
    public class Main : Game
    {
        public static GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameStateManager gameStateManager;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // SET SCREEN SIZE
            Data.SetScreenSize(graphics);

            // INITIALIZE GAME STATE
            gameStateManager = new();

            Window.Title = "Donkey Kong classic";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentLoader.LoadTextures(Content);
            gameStateManager.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            gameStateManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            gameStateManager.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}