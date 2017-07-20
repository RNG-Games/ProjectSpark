using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using _ProjectSpark.util;

namespace _ProjectSpark.actors
{
    abstract class Enemy : IActable
    {
        private const int size = 48;
        protected Vector2f position;
        protected Sprite texture;

        public Enemy(int x, int y)
        {
            position = new Vector2f(size * x, size * y);
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
