using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using _ProjectSpark.util;

namespace _ProjectSpark.actors.enemies
{
    class Vertical : Enemy
    {
        // change these if you want to change the speed
        private const float _acc = 400;
        private const float _v = 200;


        private Circle hitbox;
        private Vector2f borders;
        private float acceleration = 0;
        private float velocity = _v;


        public Vertical(int x, int upperBorder, int lowerBorder) : base(x, upperBorder)
            {
            borders = new Vector2f(upperBorder, lowerBorder) * scale;
            texture = new Sprite(Resources.GetTexture("evil.png")) { Position = position };
        }

        public override void Update(float _deltaTime)
        {
            if (position.Y >= borders.Y - scale) acceleration = -_acc;
            else if (position.Y <= borders.X + 2 * scale) acceleration = _acc;
            else acceleration = 0;

            position.Y += _deltaTime * velocity; // move
            velocity += _deltaTime * acceleration;

            if (velocity > _v) velocity = _v;
            if (velocity < -_v) velocity = -_v;

            hitbox = new Circle(position + new Vector2f(scale / 2, scale / 2), scale / 2);
            texture.Position = position;

            if (Player.getPlayer().hitbox().intersectsWith(hitbox))
            {
                Player.getPlayer().kill();
            }
        }
    }
}
