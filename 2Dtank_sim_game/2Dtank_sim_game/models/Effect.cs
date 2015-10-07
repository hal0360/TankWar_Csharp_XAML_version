using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2Dtank_sim_game.models
{
    class Effect
    {
        public double posX;
        public double posY;
        public double angle;
        public int type;
        public int count;
        public int duration;
        public dynamic img;
        public int livespan;

        public Effect(double x, double y, double a, int t, int d)
        {
            count = 0;
            type = t;
            angle = a;
            posX = x;
            posY = y;
            duration = d;
            livespan = 300;
        }
    }
   
}
