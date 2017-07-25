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
    [Serializable]
    abstract class TextBox : IActable
    {
        protected bool pressed = true;
        protected List<Letter> letters = new List<Letter>();
        protected Vector2f position;
        protected string[] messages;
        protected int curr = 0;
        protected int cutoff = 0;
        protected int msgLength = 0;
        Text text = new Text() { Font = new Font(Resources.GetFont("trebuc.ttf")) };
        protected bool expire = false;
        protected Sprite overlay;
        protected Sprite button;
        protected int width = 18;
        protected bool done = false;

        public void Draw(RenderWindow _window)
        {
            _window.Draw(overlay);
            if (!pressed) _window.Draw(button);
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