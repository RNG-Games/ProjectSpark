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
        protected List<Tuple<Text, int>> letters = new List<Tuple<Text, int>>();
        protected Vector2f position;
        protected string[] messages;
        protected int curr = 0;
        protected int cutoff = 0;
        protected int msgLength = 0;
        Text text = new Text() { Font = new Font(Resources.GetFont("trebuc.ttf")) };
        protected bool expire = false;

        private Random r = new Random();
        protected int width = 18;
        protected bool done = false;

        private float velocity = 1;
        private float acceleration = 5;
        private float speed = 0;
        private float counter = 0;

        public void Draw(RenderWindow _window)
        {
            foreach (Tuple<Text, int> _t in letters)
            {
                Text t = _t.Item1;
                int c = _t.Item2;

                switch (c)
                {
                    case 1:
                        Vector2f pos = t.Position; 
                        t.Position = new Vector2f(t.Position.X + r.Next(-2, 2), t.Position.Y + r.Next(-2, 2));
                        _window.Draw(t);
                        t.Position = pos;
                        break;
                    case 2:
                        t.Position = t.Position + new Vector2f(0, 1);
                        _window.Draw(t);
                        break;
                    default:
                        _window.Draw(t);
                        break;
                }
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
        }
    }
}