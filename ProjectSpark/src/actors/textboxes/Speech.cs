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
        private int line = 0;
        private const int capacityX = 30;
        private const int capacityY = 2;

        private int effect = 0;
        public Speech(Vector2f pos, string[] msgs)
        {
            position = pos;
            messages = msgs;
            msgLength = msgs.Length;
            overlay = new Sprite(Resources.GetTexture("testbox.png")) { Position = position};
            button = new Sprite(Resources.GetTexture("testclick.png")) { Position = position + new Vector2f(650, 80) };
            button.Texture.Smooth = true;
            overlay.Texture.Smooth = true;
            overlay.Scale = new Vector2f(0.5f, 0.5f);

            position += new Vector2f(20, 20);
        }

        public virtual Memento<IActable> Save() => new Memento<IActable>(this);

        public override void Update(float _deltaTime)
        {
            base.Update(_deltaTime);
            frameCounter += _deltaTime;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && !pressed)
            {
                letters.Clear();
                pressed = true;
                if (curr < msgLength - 1) ++curr;
                else expire = true;

                effect = 0;
                i = 0;
                j = 0;
                line = 0;
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
                Console.WriteLine(t.GetGlobalBounds().Width);
                ++i; ++j;
                frameCounter = 0;
            }
            if (i == _curr.Length) pressed = false;

        }

        private void wordwrap(int i, string s)
        {
            int sub = 0;
            if (i == 0 || s[i-1].Equals(' ') || s[i - 1].Equals('_'))
            {
                while (i+1 < s.Length)
                {
                    if (s[i].Equals('$')) sub += 2;

                    if (s[i + 1].Equals(' ')) break;
                    ++i;
                }

                i -= sub;

                if ((i - line * capacityX) > capacityX) {
                    ++line;
                    j = 0;

                    
                    if (line % capacityY == 0) letters.Clear();
                }
            }
        }


    }
}
