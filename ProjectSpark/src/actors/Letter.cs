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
        float upperBorder;
        float lowerBorder;
        const float range = 20;

        private const float _acc = 200;
        private const float _v = 100;
        private float acceleration = 200;
        private float velocity = _v;
        Sprite texture;

        public Letter(Text letter, int effect) {
            t = letter;
            c = effect;
            position = t.Position;
            lowerBorder = position.Y + range;
            upperBorder = position.Y - range;
            texture = new Sprite(Resources.GetTexture("testblock.png")) { Position = position };
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
            if (position.Y >= lowerBorder) acceleration = -_acc; 
            else if (position.Y <= upperBorder) acceleration = _acc;
            else acceleration = 0;

            position.Y += _deltaTime * velocity; // move
            velocity += _deltaTime * acceleration;

            if (velocity > _v) velocity = _v;
            if (velocity < -_v) velocity = -_v;

            t.Position = position;
        }

        public int getEffect()
        {
            return c;
        }
    }
}
