using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DonkeyKong.Source.Scenes.GameScene
{
    internal class Tile
    {
        public Vector2 position;
        public Texture2D texture;
        public bool notWalkable;

        public Tile(Vector2 pos, Texture2D tex, bool notWalkable)
        {
            position = pos;
            texture = tex;
            this.notWalkable = notWalkable;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);

        }
    }
}

