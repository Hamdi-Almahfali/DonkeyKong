using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonkeyKong.Source.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using static System.Formats.Asn1.AsnWriter;

namespace DonkeyKong.Source.Scenes.GameScene.Components
{
    internal class Cement
    {
        Vector2 position;
        Texture2D texture;

        bool collected;
        public Rectangle rect { get; private set; }

        SoundEffectInstance sndInstance;

        public Cement(Vector2 pos)
        {
            position = pos;
            texture = ContentLoader.texCement;

            collected = false;

            rect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            sndInstance = ContentLoader.sndPling.CreateInstance();
        }

        public void Update(Player player)
        {
            if (rect.Intersects(player.GetRect()) && !collected)
            {
                CollectItem();
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 loweredPosition = new Vector2(position.X, position.Y + texture.Height - 9);

            if (!collected)
                spriteBatch.Draw(texture, loweredPosition, Color.White);
        }
        public void CollectItem()
        {
            collected = true;
            GameScene.totalCements--;
            sndInstance.Play();
        }
    }
}
