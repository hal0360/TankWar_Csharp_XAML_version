using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2Dtank_sim_game.models
{
    class Bullet
    {
        public Game game;
        public double speed;
        public double posX;
        public double posY;
        public double dirX;
        public double dirY;
        public int duration;
        public int count;
        public string imgName;
        public dynamic img;
        public int dmg;

        public Bullet(double x, double y, double xx, double yy, Game g)
        {
            game = g;
            posX = x;
            posY = y;
            dirX = xx;
            dirY = yy;
            count = 0;
        }

        public Boolean detect_player()
        {
            if (this.detect_house())
            {
                return true;
            }
            if (game.detect_radius(this.posX, this.posY, game.player.posX, game.player.posY, game.player.radius))
            {
                if (game.detect_square(this.posX, this.posY, game.player.posX, game.player.posY, game.player.dirX, game.player.dirY, game.player.w, game.player.h))
                {
                    this.count = this.duration;
                    if (imgName == "bullet0.png")
                    {
                        game.effects.Add(new Effect(this.posX, this.posY, 0, 5, 10));
                    }
                    else
                    {
                        game.effects.Add(new Effect(this.posX, this.posY, 0, 6, 12));
                    }
                    game.player.live = game.player.live - this.dmg;
                }
            }
            return false;
        }

        public Boolean detect_enemy()
        {
            if (this.detect_house())
            {
                return true;
            }
            for (int i = 0; i < game.enemyTanks.Count; i += 1)
            {
                if (game.detect_radius(this.posX, this.posY, game.enemyTanks[i].posX, game.enemyTanks[i].posY, game.enemyTanks[i].radius))
                {
                    if (game.detect_square(this.posX, this.posY, game.enemyTanks[i].posX, game.enemyTanks[i].posY, game.enemyTanks[i].dirX, game.enemyTanks[i].dirY, game.enemyTanks[i].w, game.enemyTanks[i].h))
                    {
                        this.count = this.duration;
                        if(imgName == "bullet0.png")
                        {
                            game.effects.Add(new Effect(this.posX, this.posY, 0, 5, 10));
                        }
                        else
                        {
                            game.effects.Add(new Effect(this.posX, this.posY, 0, 6, 12));
                        }
                        
                        game.enemyTanks[i].live = game.enemyTanks[i].live - this.dmg;
                        return true;
                    }
                }
            }
            return false;
        }

        public Boolean detect_house()
        {
            for (int i = 0; i < game.houses.Count; i += 1)
            {
                if (game.detect_radius(this.posX, this.posY, game.houses[i].posX, game.houses[i].posY, game.houses[i].radius))
                {
                    if (game.detect_square(this.posX, this.posY, game.houses[i].posX, game.houses[i].posY, game.houses[i].dirX, game.houses[i].dirY, game.houses[i].w, game.houses[i].h))
                    {
                        this.count = this.duration;
                        if (imgName == "bullet0.png")
                        {
                            game.effects.Add(new Effect(this.posX, this.posY, 0, 5, 10));
                        }
                        else
                        {
                            game.effects.Add(new Effect(this.posX, this.posY, 0, 6, 12));
                        }
                        return true;
                    }
                }
            }
            return false;
        }
    }

    class PlayerBullet : Bullet
    {
        public PlayerBullet(double x, double y, double xx, double yy, Game g) : base(x, y, xx, yy, g)
        {
            imgName = "bullet0.png";
            speed = 6;
            duration = 67;
            dmg = 1;
        }
    }

    class SmallBullet: Bullet
    {
        public SmallBullet(double x, double y, double xx, double yy, Game g) : base(x, y, xx, yy, g)
        {
            imgName = "bullet0.png";
            speed = 6;
            duration = 67;
            dmg = 1;
        }
    }

    class BigBullet : Bullet
    {
        public BigBullet(double x, double y, double xx, double yy, Game g) : base(x, y, xx, yy, g)
        {
            imgName = "bullet1.png";
            speed = 8;
            duration = 100;
            dmg = 2;
        }
    }
}
