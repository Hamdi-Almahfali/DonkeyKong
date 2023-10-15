using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.Source.Scenes.GameScene
{
    internal class TextureHandler
    {
        public static Texture2D texBridge;
        public static Texture2D texLadder;
        public static Texture2D texCombined;
        public static Texture2D texBarrier;
        public static Texture2D texAir;

        public static Texture2D texFire;
        public static Texture2D texHeart;

        public static Texture2D texHammer;
        public static Texture2D texUmbrella;
        public static Texture2D texHammerAttack;

        public static void LoadTextures(ContentManager content)
        {
            texBridge = content.Load<Texture2D>("Sprites\\bridge");
            texLadder = content.Load<Texture2D>("Sprites\\ladder");
            texCombined = content.Load<Texture2D>("Sprites\\combined");
            texBarrier = content.Load<Texture2D>("Sprites\\barrier");
            texAir = content.Load<Texture2D>("Sprites\\air");

            texFire = content.Load<Texture2D>("Sprites\\fire");
            texHeart = content.Load<Texture2D>("Sprites\\heart");

            texHammer = content.Load<Texture2D>("Sprites\\extra");
            texHammerAttack = content.Load<Texture2D>("Sprites\\hammerAttack");
            texUmbrella = content.Load<Texture2D>("Sprites\\umbrella");
        }
    }
}
