using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2Dtank_sim_game.models
{
    class Game
    {
        public bool dead;
        public Random rnd;
        public Tank player;
        public List<dynamic> removal = new List<dynamic>();
        public List<Tank> enemyTanks = new List<Tank>();
        public List<Bullet> playerBullets = new List<Bullet>();
        public List<Bullet> enemyBullets = new List<Bullet>();
        public List<Effect> effects = new List<Effect>();
        public List<House> houses = new List<House>();
        public List<Wreck> wrecks = new List<Wreck>();
        public List<Effect> smokes = new List<Effect>();

        public Game()
        {
            dead = false;
            rnd = new Random();
            player = new PlayerTank(511, 400, 45, this);
            houses.Add(new House(340, 320, 0));
            houses.Add(new House(480, 510, 90));
            houses.Add(new House(600, 300, -45));
            enemyTanks.Add(new SmallTank(-20, 400, 90, this));
            enemyTanks.Add(new BigTank(300, 100, 145, this));
            enemyTanks.Add(new SmallTank(650, 750, 0, this));
            enemyTanks.Add(new SmallTank(-20, 450, 90, this));
            enemyTanks.Add(new SmallTank(100, 650, 20, this));
        }

        public Boolean detect_radius(double x, double y, double X, double Y, double r)
        {
            return Math.Sqrt(Math.Pow(x - X, 2) + Math.Pow(y - Y, 2)) <= r;
        }

        public Boolean detect_square(double x, double y, double X, double Y, double kx, double ky, double W, double H)
        {
            double ux, uy, vx, vy, px, py, w, h;
            ux = kx * H;
            uy = ky * H;
            vx = -1 * uy * W;
            vy = ux * W;
            px = x - X;
            py = y - Y;
            w = Math.Abs(px * vx + py * vy) / Math.Sqrt(Math.Pow(vx, 2) + Math.Pow(vy, 2));
            h = Math.Abs(px * ux + py * uy) / Math.Sqrt(Math.Pow(ux, 2) + Math.Pow(uy, 2));
            if (w <= W && h <= H)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void update()
        {

            if (player.live <= 0 && !dead)
            {
                wrecks.Add(new Wreck(player.posX, player.posY, player.angle, 0));
                effects.Add(new Effect(player.posX, player.posY, 0, 3, 21));
                smokes.Add(new Effect(player.posX, player.posY, 0, 8, 16));
                removal.Add(player.img);
                removal.Add(player.turret.img);
                player.posX = 9999;
                player.posY = 9999;
                dead = true;
                if (player.smokeimg != null)
                {
                    removal.Add(player.smokeimg);
                }
            }
            else if(!dead)
            {
player.action();
            player.turret.cool();
            }

            player.smokecount += 1;
            if (player.smokecount > 15)
            {
                player.smokecount = 0;
            }

            for (int i = 0; i < effects.Count; i += 1)
            {
                effects[i].count += 1;

                if (effects[i].count == effects[i].duration)
                {
                    removal.Add(effects[i].img);
                    effects.RemoveAt(i);
                    i -= 1;
                }
            }

            for (int i = 0; i < smokes.Count; i += 1)
            {
                smokes[i].count += 1;

                if (smokes[i].count == smokes[i].duration)
                {
                    smokes[i].count = 0;
                }
            }

            for (int i = 0; i < enemyTanks.Count; i += 1)
            {
                
                if (enemyTanks[i].initi <= 0)
                {
                    enemyTanks[i].autodrive();
                    enemyTanks[i].rand += 1;
                }
                else
                {
                    enemyTanks[i].initi -= 1;
                    enemyTanks[i].act = Act.up;
                    enemyTanks[i].action();
                }
                enemyTanks[i].turret.detect();
                
                enemyTanks[i].turret.cool();
                enemyTanks[i].turret.randcount += 1;
                enemyTanks[i].smokecount += 1;
                if(enemyTanks[i].smokecount > 15)
                {
                    enemyTanks[i].smokecount = 0;
                }

                if (enemyTanks[i].live <= 0)
                {
                    if(enemyTanks[i].imgName == "tank1.png")
                    {
                        wrecks.Add(new Wreck(enemyTanks[i].posX, enemyTanks[i].posY, enemyTanks[i].angle, 1));
                        effects.Add(new Effect(enemyTanks[i].posX, enemyTanks[i].posY, 0, 3, 21));
                        smokes.Add(new Effect(enemyTanks[i].posX, enemyTanks[i].posY, 0, 8, 16));
                    }
                    else
                    {
                        smokes.Add(new Effect(enemyTanks[i].posX, enemyTanks[i].posY, 0, 8, 16));
                        smokes.Add(new Effect(enemyTanks[i].posX + 30 * enemyTanks[i].dirX, enemyTanks[i].posY + 30 * enemyTanks[i].dirY, 0, 8, 16));
                        smokes.Add(new Effect(enemyTanks[i].posX - 30 * enemyTanks[i].dirX - 20 * enemyTanks[i].dirY, enemyTanks[i].posY - 30 * enemyTanks[i].dirY + 20 * enemyTanks[i].dirX, 0, 8, 16));
                        wrecks.Add(new Wreck(enemyTanks[i].posX, enemyTanks[i].posY, enemyTanks[i].angle, 2));
                        effects.Add(new Effect(enemyTanks[i].posX, enemyTanks[i].posY, 0, 4, 37));
                    }
                    removal.Add(enemyTanks[i].img);
                    removal.Add(enemyTanks[i].turret.img);
                    if (enemyTanks[i].smokeimg != null)
                    {
                        removal.Add(enemyTanks[i].smokeimg);
                    }
                    enemyTanks.RemoveAt(i);
                    i -= 1;
                }
            }

            for (int i = 0; i < playerBullets.Count; i += 1)
            {                       
                playerBullets[i].count += 1;
                playerBullets[i].detect_enemy();
                playerBullets[i].posY += playerBullets[i].speed * playerBullets[i].dirY;
                playerBullets[i].posX += playerBullets[i].speed * playerBullets[i].dirX;

                if (playerBullets[i].count == playerBullets[i].duration)
                {
                    removal.Add(playerBullets[i].img);
                    playerBullets.RemoveAt(i);
                    i -= 1;
                }
            }

            for (int i = 0; i < enemyBullets.Count; i += 1)
            {
                enemyBullets[i].count += 1;
                enemyBullets[i].detect_player();
                enemyBullets[i].posY += enemyBullets[i].speed * enemyBullets[i].dirY;
                enemyBullets[i].posX += enemyBullets[i].speed * enemyBullets[i].dirX;

                if (enemyBullets[i].count == enemyBullets[i].duration)
                {
                    removal.Add(enemyBullets[i].img);
                    enemyBullets.RemoveAt(i);
                    i -= 1;
                }
            }
        }
    }
}
