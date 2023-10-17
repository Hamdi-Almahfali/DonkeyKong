using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.Source.Engine
{
    internal static class Data
    {
        public static int ScreenW { get; private set; } = 992;
        public static int ScreenH { get; private set; } = 636;

        // CHANGE SCREEN SIZE TO PREFERRED SIZE
        public static void SetScreenSize(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferWidth = ScreenW;
            graphics.PreferredBackBufferHeight = ScreenH;
            graphics.ApplyChanges();
        }
    }
}
