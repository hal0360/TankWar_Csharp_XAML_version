using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace _2Dtank_sim_game.models
{
    public enum Act { left, right, up, down, stop };

    class Tank
    {
        public Game game;
        public int live;
        public int initi;
        public string imgName;     
        public double posX;
        public double posY;
        public double dirY;
        public double dirX;
        public double offX;
        public double w;
        public double h;
        public double offY;
        public double speed;
        public double turnRate;
        public double angle;
        public dynamic img;
        public dynamic smokeimg;
        public int smokecount;
        public double radius;
        public Act act;
        public Turret turret;
        public int rand;
        public Boolean flag;
        public int mode;
        public int maxlive;

        public Tank(double x, double y, double a, Game g)
        {
            smokecount = 0;
            game = g;
            posX = x;
            posY = y;
            angle = a;
            dirX = Math.Sin(angle * Math.PI / 180);
            dirY = -1 * Math.Cos(angle * Math.PI / 180);
            act = Act.stop;
            rand = 21;
            flag = true;
            mode = 0;
        }

        public void autodrive()
        {
            if (this.rand > 20)
            {
                if (this.flag)
                {
                    this.mode = game.rnd.Next(1, 7);
                }
                else
                {
                    this.mode = game.rnd.Next(1, 3);
                    this.flag = true;
                }
                this.rand = 0;
            }
            if (this.mode == 1)
            {
                act = Act.left;
                if (action() == 1)
                {
                    this.mode = 2;
                }
            }
            else if (this.mode == 2)
            {
                act = Act.right;
                if (action() == 1)
                {
                    this.mode = 1;
                }
            }
            else if (this.mode == 3)
            {
                act = Act.down;
                int H = action();
                if (H > 0)
                {

                    if (H == 1 || H == 3)
                    {
                        this.mode = 5;
                        this.flag = false;
                    }
                    else
                    {
                        this.mode = game.rnd.Next(1, 3);
                    }
                }
            }
            else
            {
                act = Act.up;
                int H = action();
                if (H > 0)
                {

                    if (H == 1 || H == 3)
                    {
                        this.mode = 3;
                        this.flag = false;
                    }
                    else
                    {
                        this.mode = game.rnd.Next(1, 3);
                    }
                }
            }
        }

        public Boolean colli_building()
        {
            double x1, y1, y2, x2, x3, y3, x4, y4;
            for (int i = 0; i < game.houses.Count; i = i + 1)
            {
                if (game.detect_radius(this.posX, this.posY, game.houses[i].posX, game.houses[i].posY, this.radius + game.houses[i].radius))
                {
                    x1 = this.posX + this.h * this.dirX + this.w * this.dirY;
                    y1 = this.posY + this.h * this.dirY - this.w * this.dirX;
                    x2 = this.posX + this.h * this.dirX - this.w * this.dirY;
                    y2 = this.posY + this.h * this.dirY + this.w * this.dirX;
                    x3 = this.posX - this.h * this.dirX + this.w * this.dirY;
                    y3 = this.posY - this.h * this.dirY - this.w * this.dirX;
                    x4 = this.posX - this.h * this.dirX - this.w * this.dirY;
                    y4 = this.posY - this.h * this.dirY + this.w * this.dirX;
                    if (game.detect_square(x1, y1, game.houses[i].posX, game.houses[i].posY, game.houses[i].dirX, game.houses[i].dirY, game.houses[i].w, game.houses[i].h) ||
                       game.detect_square(x2, y2, game.houses[i].posX, game.houses[i].posY, game.houses[i].dirX, game.houses[i].dirY, game.houses[i].w, game.houses[i].h) ||
                       game.detect_square(x3, y3, game.houses[i].posX, game.houses[i].posY, game.houses[i].dirX, game.houses[i].dirY, game.houses[i].w, game.houses[i].h) ||
                       game.detect_square(x4, y4, game.houses[i].posX, game.houses[i].posY, game.houses[i].dirX, game.houses[i].dirY, game.houses[i].w, game.houses[i].h) ||
                       game.detect_square(game.houses[i].x1, game.houses[i].y1, this.posX, this.posY, this.dirX, this.dirY, this.w, this.h) ||
                       game.detect_square(game.houses[i].x2, game.houses[i].y2, this.posX, this.posY, this.dirX, this.dirY, this.w, this.h) ||
                       game.detect_square(game.houses[i].x3, game.houses[i].y3, this.posX, this.posY, this.dirX, this.dirY, this.w, this.h) ||
                       game.detect_square(game.houses[i].x4, game.houses[i].y4, this.posX, this.posY, this.dirX, this.dirY, this.w, this.h))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Boolean colli_border()
        {
            if (this.posX < 0 || this.posX > 1024 || this.posY < 0 || this.posY > 800)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean detect_object()
        {
            if (imgName != "tank0.png")
            {
                if (game.detect_radius(this.posX, this.posY, game.player.posX, game.player.posY, (this.w + game.player.h + this.h + game.player.w)/2))
                {
                    return true;
                }
            }
            for (int i = 0; i < game.enemyTanks.Count; i = i + 1)
            {
                if (this != game.enemyTanks[i] && game.detect_radius(this.posX, this.posY, game.enemyTanks[i].posX, game.enemyTanks[i].posY, (this.h + game.enemyTanks[i].h + this.w + game.enemyTanks[i].w) / 2))
                {
                    return true;
                }
            }
            return false;
        }

        public void setPosition(double x, double y)
        {
            posX = x;
            posY = y;
            turret.setPosition(x, y);
        }

        public int action()
        {
            switch (act)
            {
                case Act.left:
                    angle = angle - turnRate;
                    dirX = Math.Sin(angle * Math.PI / 180);
                    dirY = -1 * Math.Cos(angle * Math.PI / 180);
                    if (colli_building())
                    {
                        angle = angle + turnRate;
                        dirX = Math.Sin(angle * Math.PI / 180);
                        dirY = -1 * Math.Cos(angle * Math.PI / 180);
                        return 1;
                    }
                    break;
                case Act.right:
                    angle = angle + turnRate;
                    dirX = Math.Sin(angle * Math.PI / 180);
                    dirY = -1 * Math.Cos(angle * Math.PI / 180);
                    if (colli_building())
                    {
                        angle = angle - turnRate;
                        dirX = Math.Sin(angle * Math.PI / 180);
                        dirY = -1 * Math.Cos(angle * Math.PI / 180);
                        return 1;
                    }
                    break;
                case Act.up:
                    setPosition(posX + speed * dirX, posY + speed * dirY);
                    bool A = colli_building();
                    bool B = detect_object();
                    bool C = colli_border();
                    if (initi > 0)
                    {
                        C = false;
                    }
                    if (A || B || C)
                    {
                        setPosition(posX - speed * dirX, posY - speed * dirY);
                        if (C)
                        {
                            return 3;
                        }
                        else if (B)
                        {
                            return 2;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    break;
                case Act.down:
                    setPosition(posX - speed * dirX, posY - speed * dirY);
                    bool a = colli_building();
                    bool b = detect_object();
                    bool c = colli_border();
                    if (initi > 0)
                    {
                        c = false;
                    }
                    if (a || b || c)
                    {
                        setPosition(posX + speed * dirX, posY + speed * dirY);
                        if (c)
                        {
                            return 3;
                        }
                        else if (b)
                        {
                            return 2;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    break;
            }
            return 0;
        }
    }

    class PlayerTank: Tank
    {
        public PlayerTank(double x, double y, double a, Game g): base(x, y, a, g)
        {
            turret = new PlayerTurret(x, y, a * Math.PI / 180, g);
            maxlive = 30;
            live = maxlive;
            offX = 18.5;
            offY = 31.5;
            h = 30;
            w = 18;
            turnRate = 4;
            speed = 3.4;
            imgName = "tank0.png";
            radius = Math.Sqrt(Math.Pow(this.w, 2) + Math.Pow(this.h, 2));
            initi = 0;
        }
    }

    class SmallTank : Tank
    {
        public SmallTank(double x, double y, double a, Game g) : base(x, y, a, g)
        {
            turret = new SmallTurret(x, y, a * Math.PI / 180, g);
            maxlive = 7;
            live = maxlive;
            offX = 18.5;
            offY = 31.5;
            h = 30;
            w = 18;
            turnRate = 3.4377467708;
            speed = 2.7;
            imgName = "tank1.png";
            radius = Math.Sqrt(Math.Pow(this.w, 2) + Math.Pow(this.h, 2));
            initi = 40;
        }
    }

    class BigTank : Tank
    {
        public BigTank(double x, double y, double a, Game g) : base(x, y, a, g)
        {
            turret = new BigTurret(x, y, a * Math.PI / 180, g);
            maxlive = 25;
            live = maxlive;
            offX = 26.5;
            offY = 47.5;
            h = 46;
            w = 26;
            turnRate = 2.29183118;
            speed = 2.0;
            imgName = "tank2.png";
            radius = Math.Sqrt(Math.Pow(this.w, 2) + Math.Pow(this.h, 2));
            initi = 50;
        }
    }

}
