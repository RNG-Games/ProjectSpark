﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace _ProjectSpark.actors
{
    abstract class Light : IActable
    {
        protected Vector2f position;
        protected Sprite texture;

        public void Draw(RenderWindow _window)
        {
            _window.Draw(texture);
        }

        public bool IsExpired()
        {
            return false;
        }

        public float StartTime()
        {
            return 0f;
        }

        public virtual void Update(float _deltaTime)
        {
            return;
        }
    }
}