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
    public class TextBox : IActable
    {
        protected Text text; 
        protected string toDisplay;
        protected Sprite texture = new Sprite(Resources.GetTexture("TestTB.png")); 
        protected Sprite portrait;
        protected Vector2f position;
        protected bool expiration;
        protected float frameCounter;
        protected float starttime = 0f;

        public TextBox(Vector2f _position, string _text, string _font, string _portrait)
        {
            text = new Text("", Resources.GetFont(_font)) {FillColor = new Color(0, 0, 0)};
            toDisplay = _text;
            position = _position;
            expiration = false;
            portrait = new Sprite(Resources.GetTexture(_portrait));
        }

        public TextBox(Vector2f _position, string _text, string _font, string _portrait, float _starttime) :this(_position,_text,_font,_portrait)
        {
            starttime = _starttime;
        }

        public void Draw(RenderWindow _window)
        {
            _window.Draw(texture);
            _window.Draw(portrait);
            _window.Draw(text);
        }

        public bool IsExpired()
        {
            return expiration;
        }

        public float StartTime()
        {
            return starttime;
        }

        public virtual Memento<IActable> Save() => new Memento<IActable>(this);

        public virtual void Update(float _deltaTime)
        {
            frameCounter += _deltaTime;
            if (frameCounter > 0.1 && toDisplay.Length > 0)  //slowly display the text
            {
                text.DisplayedString += toDisplay.ElementAt(0);
                if (text.DisplayedString.Length % 20 == 0) { text.DisplayedString += '\n'; }
                toDisplay = toDisplay.Substring(1);  //adjust toDisplay
                frameCounter = 0;
            }

            texture.Position = position; 
            text.Position = new Vector2f (position.X + 100, position.Y + 20);  
            portrait.Position = new Vector2f(position.X + -25, position.Y -25); //portraits der Charaktere hängen jetzt vorerst in der oberen linken ecke

            if (frameCounter > 1.5) { position = new Vector2f(position.X, position.Y + (float)0.2); } //Despawn animation
            if (frameCounter > 3) { expiration = true; } //After the full Text is displayed and 2 seconds(?) passed, textbox closes
        }
    }
}