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
        private float frameCounter;
        private int i = 0;
        private int j = 0;
        private bool pressed = false;
        private int line = 0;
        private const int capacityX = 40;
        private const int capacityY = 4;

        private int effect = 0;
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
                i = 0;
                j = 0;
                line = 0;
                letters.Clear();
            }

            string _curr = messages[curr];

            if (i < _curr.Length && frameCounter > 0.025 && _curr.Length > 0)
            {
                wordwrap(i, _curr);
                Text t = new Text() { Font = new Font(Resources.GetFont("Anonymous.ttf")) };
                t.Position = new Vector2f(position.X + j * width, position.Y + line%capacityY * 40);
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

        private void wordwrap(int i, string s)
        {
            if (i == 0 || s[i-1].Equals(' ') || s[i - 1].Equals('_'))
            {
                while (i+1 < s.Length)
                {
                    if (s[i + 1].Equals(' ')) break;
                    ++i;
                }
                if ((i - line * capacityX) > capacityX) {
                    ++line;
                    j = 0;

                    if (line % capacityY == 0) letters.Clear();
                }
            }
        }


    }
}
