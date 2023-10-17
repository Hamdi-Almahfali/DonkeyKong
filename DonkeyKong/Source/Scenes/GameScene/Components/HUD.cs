using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonkeyKong.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DonkeyKong.Source.Scenes.GameScene.Components
{
    internal class HUD
    {
        const int TileSize = 32;
        int maxHearts = GameScene.maxHearts;
        const int size = TileSize + 38; // Gap between the hearts 

        float heartsRatio;
        int heartsToDisplay;

        internal void Update(GameTime gameTime, int hearts)
        {
            heartsRatio = (float)hearts / maxHearts;
            heartsToDisplay = (int)Math.Ceiling(heartsRatio * maxHearts);
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < heartsToDisplay; i++)
            {
                spriteBatch.Draw(ContentLoader.texHeart, new Vector2(i * size + 32, 32), Color.White);
            }
        }
    }
}
