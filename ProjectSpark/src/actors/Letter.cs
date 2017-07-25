using System;
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
    [Serializable]
    class Letter : IActable
    {
        int c = 0;
        Text t;
        Random r = new Random();
        Vector2f position;

        private int s = 0;
        public Letter(Text letter, int effect) {
            t = letter;
            c = effect;
            //t.Rotation = -10;
            position = t.Position;
            position.Y += 5;
        }

        public void Draw(RenderWindow _window) {
            switch (c)
            {
                case 1:
                    Vector2f pos = t.Position;
                    t.Position = new Vector2f(t.Position.X + r.Next(-2, 2), t.Position.Y + r.Next(-2, 2));
                    _window.Draw(t);
                    t.Position = pos;
                    break;
                case 2:
                    _window.Draw(t);
                    break;
                case 3:
                    t.FillColor = new Color(255, 0, 0);
                    _window.Draw(t);
                    break;
                default:
                    _window.Draw(t);
                    break;
            }
        }

        public bool IsExpired()
        {
            return false;
        }

        public new virtual Memento<IActable> Save() => new Memento<IActable>(this);

        public float StartTime()
        {
            return 0f;
        }

        public void Update(float _deltaTime)
        {
            t.Position = position;
            s += 1;
            position.Y += (float) Math.Sin(s * 0.5 * Math.PI / 25);
        }

        public int getEffect()
        {
            return c;
        }
    }
}
