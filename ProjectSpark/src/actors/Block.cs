﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _ProjectSpark.util;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace _ProjectSpark.actors
{
    abstract class Block : IActable
    {
        protected Vector2f position;
        protected Sprite texture;
        protected int scale;

        public Block(int x, int y)
        {
            scale = Resources.getScale();
            position = new Vector2f(scale *x, scale *y);
        }

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
