using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.Source.Scenes.GameScene
{
    internal class ContentLoader
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
        public static Texture2D texCement;
        public static Texture2D texButton;
        public static Texture2D texBarrel;
        public static Texture2D texBonus;

        public static Texture2D texDonkeyK;
        public static Texture2D texPauline;
        public static Texture2D texLovers;


        public static SoundEffect sndExplode;
        public static SoundEffect sndPling;
        public static SoundEffect sndFireDeath;
        public static SoundEffect sndHit;

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
            texCement = content.Load<Texture2D>("Sprites\\cement");
            texButton = content.Load<Texture2D>("Sprites\\button");
            texBarrel = content.Load<Texture2D>("Sprites\\barrel");
            texBonus = content.Load<Texture2D>("Sprites\\bonus");

            texDonkeyK = content.Load<Texture2D>("Sprites\\DonkeyKong");
            texPauline = content.Load<Texture2D>("Sprites\\pauline");
            texLovers = content.Load<Texture2D>("Sprites\\playerWon");

            // SOUNDS
            sndExplode = content.Load<SoundEffect>("Sounds\\jump");
            sndPling = content.Load<SoundEffect>("Sounds\\coin_pling");
            sndFireDeath = content.Load<SoundEffect>("Sounds\\fireDeath");
            sndHit = content.Load<SoundEffect>("Sounds\\hit2");
        }
    }
}
