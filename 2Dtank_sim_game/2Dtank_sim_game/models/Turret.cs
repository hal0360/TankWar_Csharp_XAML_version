using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2Dtank_sim_game.models
{
    abstract class Turret
    {
        public Game game;
        public double posX;
        public string imgName;
        public double posY;
        public double dirY;
        public double dirX;
        public double offX;
        public double offY;
        public int range;
        public double turnRate;
        public int fireRate;
        public double angle = 0;
        public double targetAngle;
        public dynamic img;
        public int count;
        public int randcount;

        public Turret(double x, double y, double a, Game g)
        {
            game = g;   
            angle = a;
            targetAngle = a;
            dirX = Math.Sin(angle);
            dirY = -1 * Math.Cos(angle);
            setPosition(x, y);
            count = 0;
            randcount = 0;
        }

        public void cool()
        {
            if (count > 0)
            {
                count = count - 1;
            }
        }

        public abstract void fire();

        public void setPosition(double x, double y)
        {
            posX = x;
            posY = y;
        }

        public void detect()
        {
            if (Math.Sqrt(Math.Pow(this.posX - game.player.posX, 2) + Math.Pow(this.posY - game.player.posY, 2)) <= this.range)
            {
                this.targetAngle = Math.Atan2(game.player.posX - this.posX, this.posY - game.player.posY);
                if (this.aim())
                {
                    this.fire();
                }
            }
            else
            {
                if (this.randcount > 100)
                {
                    this.targetAngle = Math.PI * (2 * game.rnd.NextDouble() - 1);
                    this.randcount = 0;
                }
                this.aim();
            }
        }

        public Boolean aim()
        {
            if (targetAngle - angle > 0.04)
            {
                if (targetAngle - angle < Math.PI)
                {
                    angle = angle + turnRate;
                    if (angle > Math.PI)
                    {
                        angle = angle - 2 * Math.PI;
                    }
                }
                else
                {
                    angle = angle - turnRate;
                    if (angle < -1 * Math.PI)
                    {
                        angle = 2 * Math.PI + angle;
                    }
                }
                return false;
            }
            else if (angle - targetAngle > 0.04)
            {
                if (angle - targetAngle <= Math.PI)
                {
                    angle = angle - turnRate;
                    if (angle < -1 * Math.PI)
                    {
                        angle = 2 * Math.PI + angle;
                    }
                }
                else
                {
                    angle = angle + turnRate;
                    if (angle > Math.PI)
                    {
                        angle = angle - 2 * Math.PI;
                    }
                }
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    class PlayerTurret: Turret
    {
        public PlayerTurret(double x, double y, double a, Game g): base(x, y, a, g)
        {
            imgName = "turret0.png";
            offX = 14;
            offY = 30;
            turnRate = 0.05;
            fireRate = 19;
            range = 450;
        }

        public override void fire()
        {
            if (count <= 0)
            {
                dirX = Math.Sin(angle);
                dirY = -1 * Math.Cos(angle);
                game.playerBullets.Add(new PlayerBullet(posX + 30 * dirX, posY + 30 * dirY, dirX, dirY, game));
                game.effects.Add(new Effect(posX + 30 * dirX, posY + 30 * dirY, angle, 1, 4));
                count = fireRate;
            }
        }
    }

    class SmallTurret : Turret
    {
        public SmallTurret(double x, double y, double a, Game g) : base(x, y, a, g)
        {
            imgName = "turret1.png";
            offX = 14;
            offY = 30;
            turnRate = 0.05;
            fireRate = 19;
            range = 450;
        }

        public override void fire()
        {
            if (count <= 0)
            {
                dirX = Math.Sin(angle);
                dirY = -1 * Math.Cos(angle);
                game.enemyBullets.Add(new SmallBullet(posX + 30 * dirX, posY + 30 * dirY, dirX, dirY, game));
                game.effects.Add(new Effect(posX + 30 * dirX, posY + 30 * dirY, angle, 1, 4));
                count = fireRate;
            }
        }
    }

    class BigTurret : Turret
    {
        public BigTurret(double x, double y, double a, Game g) : base(x, y, a, g)
        {
            imgName = "turret2.png";
            offX = 22;
            offY = 67;
            turnRate = 0.05;
            fireRate = 27;
            range = 880;
        }

        public override void fire()
        {
            if (count <= 0)
            {
                dirX = Math.Sin(angle);
                dirY = -1 * Math.Cos(angle);
                game.enemyBullets.Add(new BigBullet(posX + 70 * dirX, posY + 70 * dirY, dirX, dirY, game));
                game.effects.Add(new Effect(posX + 70 * dirX, posY + 70 * dirY, angle, 2, 6));
                count = fireRate;
            }
        }
    }
}
