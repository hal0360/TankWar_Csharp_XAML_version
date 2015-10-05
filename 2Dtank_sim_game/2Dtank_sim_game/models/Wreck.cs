using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2Dtank_sim_game.models
{
    class Wreck
    {
        public double posX;
        public double posY;
        public double angle;
        public dynamic img;
        public int type;

        public Wreck(double x, double y, double a, int t)
        {
            posX = x;
            posY = y;
            angle = a;
            type = t;
        }
    }
}
