using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2Dtank_sim_game.models
{
    class House
    {
        public double posX;
        public double posY;
        public double dirY;
        public double dirX;
        public double w;
        public double h;
        public double x1;
        public double y1;
        public double x2;
        public double y2;
        public double x3;
        public double y3;
        public double x4;
        public double y4;
        public double angle;
        public dynamic img;
        public double radius;

        public House(double x, double y, double a)
        {
            this.posX = x;
            this.posY = y;
            this.angle = a;
            this.dirX = Math.Sin(Math.PI * angle / 180.0);
            this.dirY = -1 * Math.Cos(Math.PI * angle / 180.0);
            this.w = 45;
            this.h = 72;
            this.x1 = this.posX + this.h * this.dirX + this.w * this.dirY;
            this.y1 = this.posY + this.h * this.dirY - this.w * this.dirX;
            this.x2 = this.posX + this.h * this.dirX - this.w * this.dirY;
            this.y2 = this.posY + this.h * this.dirY + this.w * this.dirX;
            this.x3 = this.posX - this.h * this.dirX + this.w * this.dirY;
            this.y3 = this.posY - this.h * this.dirY - this.w * this.dirX;
            this.x4 = this.posX - this.h * this.dirX - this.w * this.dirY;
            this.y4 = this.posY - this.h * this.dirY + this.w * this.dirX;
            this.radius = Math.Sqrt(Math.Pow(this.w, 2) + Math.Pow(this.h, 2));
        }
    }
}
