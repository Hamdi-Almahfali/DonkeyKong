using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.Source.Managers
{
    internal class Timer
    {
        private double currentTime = 0.0;
        public void ResetAndStart(double delay)
        {
            currentTime = delay;
        }
        public bool IsDone()
        {
            return currentTime <= 0.0;
        }
        public void Update(GameTime gameTime)
        {
            currentTime -= gameTime.ElapsedGameTime.TotalSeconds;
        }

    }
}
