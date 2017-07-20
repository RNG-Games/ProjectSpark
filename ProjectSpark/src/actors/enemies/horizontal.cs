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
        private Vector2f _acceleration = new Vector2f(20, 0); 
        private Vector2f _velocity = new Vector2f(80, 0); // start speed
        private Vector2f acceleration;
        private Vector2f velocity;

        public Horizontal(int x, int y, int leftBorder, int rightBorder) : base(x, y)
            {
            acceleration = _acceleration;
            velocity = _velocity;
            borders = new Vector2f(leftBorder, rightBorder) * scale;
            texture = new Sprite(Resources.GetTexture("evil.png")) { Position = position };
            hitbox = new Circle(position + new Vector2f(scale/2, scale/2), scale/2); 
        }

        public override void Update(float _deltaTime)
        {
            position += _deltaTime * velocity;
            velocity += _deltaTime * acceleration;
            if ((position.X >= borders.Y && velocity.X >= 0) || (position.X <= borders.X && velocity.X <= 0)) {
                velocity *= -1;
                acceleration *= -1;
            }

            texture.Position = position;

            if (Player.getPlayer().hitbox().intersectsWith(hitbox))
            {
                Player.getPlayer().kill();
            }
        }
}
}
