using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using _ProjectSpark.util;

namespace _ProjectSpark.actors
{
    abstract class TextBox : IActable
    {
        protected List<Letter> letters = new List<Letter>();
        protected Vector2f position;
        protected string[] messages;
        protected int curr = 0;
        protected int cutoff = 0;
        protected int msgLength = 0;
        Text text = new Text() { Font = new Font(Resources.GetFont("trebuc.ttf")) };
        protected bool expire = false;

        protected int width = 18;
        protected bool done = false;

        private float velocity = 1;
        private float acceleration = 5;
        private float speed = 0;
        private float counter = 0;

        public void Draw(RenderWindow _window)
        {
            foreach (Letter t in letters)
            {
                t.Draw(_window);
            }
        }



        public bool IsExpired()
        {
            return expire;
        }

        public float StartTime()
        {
            return 0f;
        }

        public virtual Memento<IActable> Save() => new Memento<IActable>(this);

        public virtual void Update(float _deltaTime) {
            foreach (Letter t in letters)
            {
                if (t.getEffect() == 2) t.Update(_deltaTime);
            }
        }
    }
}