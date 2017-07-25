using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using _ProjectSpark.util;

namespace _ProjectSpark.actors.textboxes
{
    [Serializable]
    class Speech : TextBox
    {
        protected float frameCounter;
        protected int i = 0;
        protected int j = 0;
        protected bool pressed = false;

        private int effect = 0;
        private bool affected = false;
        public Speech(Vector2f pos, string[] msgs)
        {
            position = pos;
            messages = msgs;
            msgLength = msgs.Length;
        }

        public virtual Memento<IActable> Save() => new Memento<IActable>(this);

        public override void Update(float _deltaTime)
        {
            base.Update(_deltaTime);
            frameCounter += _deltaTime;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && !pressed)
            {
                pressed = true;
                if (curr < msgLength - 1) ++curr;
                else expire = true;

                effect = 0;
                affected = false;
                i = 0;
                j = 0;
                letters.Clear();
            }

            string _curr = messages[curr];

                if (i < _curr.Length && frameCounter > 0.1 && _curr.Length > 0)
                {
                    Text t = new Text() { Font = new Font(Resources.GetFont("Anonymous.ttf")) };
                    t.Position = new Vector2f(position.X + j * width, position.Y);
                    t.FillColor = new Color(0, 0, 0);

                    if (_curr[i].Equals('$'))
                    {
                        i += 2;
                        effect = (int) Char.GetNumericValue(_curr[i - 1]);
                    }

                    if (_curr[i].Equals(' ')) effect = 0;
                    t.DisplayedString = (_curr[i].Equals('_')) ? " " : "" + _curr[i];
                    letters.Add(new Letter(t, effect));

                    ++i; ++j;
                    frameCounter = 0;
                }
                if (i == _curr.Length) pressed = false;

        }

    }
}
