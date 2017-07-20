﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using _ProjectSpark.util;

namespace _ProjectSpark.actors.enemies
{
    class Stationary : Enemy
    {
        private Circle hitbox;

        public Stationary(int x, int y) : base(x, y)
        {
            texture = new Sprite(Resources.GetTexture("evil.png")) { Position = position };
            hitbox = new Circle(position + new Vector2f(24, 24), 24); ;
        }

        public override void Update(float _deltaTime)
        {
            if (Player.getPlayer().hitbox().intersectsWith(hitbox))
            {
                Player.getPlayer().kill();
            }
        }
    }
}