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
using ProjectSpark.math;

namespace ProjectSpark.actors.enemies
{
    class Circular : Enemy
    {
        private float radius;
        private float angle;
        private Vector2f middle;
        private float _v = 100;
        private util.Circle hitbox;

        public Circular(int x, int y, float r, float a, float speed) : base(x, y)
        {
            frame = "testenemy";
            middle = position + new Vector2f(scale / 2, scale / 2);
            radius = r * 48;
            angle = a;
            _v *= speed;
            position = Maths.ToCartesian(middle, angle, radius);
            hitbox = new util.Circle(position + new Vector2f(scale / 2, scale / 2), scale / 2);
        }

        public override void Update(UltravioletTime time)
        {
            base.Update(time);
            position = Maths.ToCartesian(middle, angle, radius);
            angle += _v * Resources.deltaTime;

            if (Player.getPlayer().hitbox().intersectsWith(hitbox))
            {
                Player.getPlayer().kill();
            }
        }
    }
}
