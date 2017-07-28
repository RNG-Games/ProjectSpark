using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSpark.assets;
using ProjectSpark.util;
using TwistedLogik.Ultraviolet;
using TwistedLogik.Ultraviolet.Content;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D;
using TwistedLogik.Ultraviolet.Input;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D.Text;
using ProjectSpark.glyphshaders;

namespace ProjectSpark.actors
{
    class Textbox : IActable
    {
        private Vector2f position;
        private string[] msgs;
        private string curr;
        private string _curr;
        private const int capacityX = 30;
        private const int capacityY = 2;
        private float frameCounter = 0;
        private int i = 0;
        private TextRenderer tr;
        private SpriteFont Trebuchet;

        private int skips = 0;
        private int sub = 0;

        public Textbox(string[] messages, Vector2f position)
        {
            this.position = position;
            msgs = messages;
            curr = msgs[0];
            _curr = " ";
            tr = new TextRenderer();
            tr.RegisterGlyphShader("shaky", new Shaky());
            tr.RegisterGlyphShader("rainbow", new Rainbow());
            tr.RegisterGlyphShader("wavy", new Wavy());
            Trebuchet = Resources.ContentManager.Load<SpriteFont>(GlobalFontID.TrebuchetMS16);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var settings = new TextLayoutSettings(Trebuchet, null, null, TextFlags.Standard);
            tr.Draw(spriteBatch, _curr, position, Color.Black, settings);
        }

        public void Update(UltravioletTime time)
        {
            frameCounter += Resources.deltaTime;
            if (i < curr.Length && frameCounter > 0.1 && curr.Length > 0)
            {
                if (curr[i].Equals(' ')) wordwrap(i, curr);
                if (curr[i].Equals('|'))
                {
                    ++skips;
                    _curr += curr[i++];
                    do
                    {
                        _curr += curr[i++];
                        ++skips;
                    } while (!curr[i].Equals('|'));
                    ++skips;
                }
                _curr += curr[i++];
            }
        }

        private void wordwrap(int i, string s)
        {
            int j = 0;
            ++i;
            int start = (i - skips) - sub; //- line * capacityX;
            if (i < s.Length && s[i].Equals('|')) j = push(i, s);
            else
            {
                while (i < s.Length)
                {
                    if (s[i].Equals(' ') || s[i].Equals('|')) break;
                    ++i; ++j;
                }
            }

            if (start + j > capacityX) { _curr += System.Environment.NewLine; sub += start; }
        }

        private int push(int i, string s)
        {
            string _s = "";
            string[] subs;
            string _r = "";

            while (i+1 < s.Length && !s[i+1].Equals(' '))
            {
                _s += s[i];
                ++i;
            }
            _s += "|";
            subs = _s.Split('|');
            for (int j = 1; j < subs.Length; ++j)
            {
                if (!subs[j].Contains("shader") && (!(subs[j].Length == 1) && !subs[j].Contains("c"))) { _r += subs[j]; }
            }

            return _r.Length;
        }

        public bool IsExpired()
        {
            return false;
        }

        public float StartTime()
        {
            return 0f;
        }
    }
}
