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
    class Horizontal : Enemy {
        // change these if you want to change the speed
        private const float _acc = 400;
        private const float _v = 200;

        private util.Circle hitbox;
        private Vector2f borders;
        private float acceleration = 0;
        private float velocity = _v;

        public Horizontal(int y, int leftBorder, int rightBorder) : base(leftBorder, y)
        {
            borders = new Vector2f(leftBorder, rightBorder) * scale;
            frame = "testenemy";
        }

        public override void Update(UltravioletTime time)
        {
            if (position.X >= borders.Y - scale) acceleration = -_acc;
            else if (position.X <= borders.X + scale) acceleration = _acc;
            else acceleration = 0;

            position.X += Resources.deltaTime * velocity; // move
            velocity += Resources.deltaTime * acceleration;

            if (velocity > _v) velocity = _v;
            if (velocity < -_v) velocity = -_v;

            hitbox = new util.Circle(position + new Vector2f(scale / 2, scale / 2), scale / 2);

            if (Player.getPlayer().hitbox().intersectsWith(hitbox))
            {
                Player.getPlayer().kill();
            }
        }
    }
}
