using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSpark.assets;
using ProjectSpark.util;
using TwistedLogik.Ultraviolet;
using TwistedLogik.Ultraviolet.Content;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D;
using TwistedLogik.Ultraviolet.Input;

namespace ProjectSpark.actors.enemies
{
    class Stationary : Enemy
    {
        private util.Circle hitbox;

        public Stationary(int x, int y) : base(x, y)
        {
            frame = "testenemy";
            hitbox = new util.Circle(position + new Vector2f(scale/2, scale/2), scale/2); 
        }

        public override void Update(UltravioletTime time)
        {
            if (Player.getPlayer().hitbox().intersectsWith(hitbox))
            {
                Player.getPlayer().kill();
            }
        }
    }
}
