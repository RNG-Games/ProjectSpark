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
    [Serializable]
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

        public virtual Memento<IActable> Save() => new Memento<IActable>(this);
    }
}
