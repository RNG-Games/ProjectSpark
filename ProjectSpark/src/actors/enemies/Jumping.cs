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
    class Jumping : Enemy
    {
        private int state = 0;
        private float frameCounter = 0;
        private float _v;
        private float _u;
        private util.Circle hitbox;
        private const float gravity = 800;
        private const float upwardGravity = 1200;
        private float ground;

        public Jumping(int x, int y, float velocity) : base(x, y)
        {
            ground = y * scale;
            frame = "testenemy";
            _u = velocity;
            _v = _u;
        }

        public override void Update(UltravioletTime time)
        {
            frameCounter += Resources.deltaTime;
            switch (state)
            {
                case 0:
                    wait(); break;
                case 1:
                    move(); break;
                default:
                    break;
            }

            hitbox = new util.Circle(position + new Vector2f(scale / 2, scale / 2), scale / 2);
            if (Player.getPlayer().hitbox().intersectsWith(hitbox))
            {
                Player.getPlayer().kill();
            }
        }

        private void wait()
        {
            if (frameCounter >= 1) { _v = _u; ; state = 1; }
        }

        private void move()
        {
            frameCounter = 0;
            position.Y += Resources.deltaTime * _v;

            if (_v < 0)
            {
                _v += Resources.deltaTime * upwardGravity;
            }
            else
            {
                _v += Resources.deltaTime * gravity;
            }

            if (position.Y > ground) { position.Y = ground; state = 0; }
        }

    }
}
