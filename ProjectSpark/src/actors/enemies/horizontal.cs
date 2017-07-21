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
    class Horizontal : Enemy { 

        private Circle hitbox;
        private Vector2f borders;
        private float acceleration = 0;
        private float velocity = 200;
        private const float _acc = 400;
        private const float _v = 200;
        private int state = 0;

        public Horizontal(int y, int leftBorder, int rightBorder) : base(leftBorder, y)
            {
            borders = new Vector2f(leftBorder, rightBorder) * scale;
            texture = new Sprite(Resources.GetTexture("evil.png")) { Position = position }; 
        }

        public override void Update(float _deltaTime)
        {
            Console.WriteLine(velocity);
            if (position.X >= borders.Y - scale) acceleration = -400;
            else if (position.X <= borders.X + 2*scale) acceleration = 400;
            else acceleration = 0;

            position.X += _deltaTime * velocity; // move
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
