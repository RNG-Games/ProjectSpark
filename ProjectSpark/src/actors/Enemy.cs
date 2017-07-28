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

namespace ProjectSpark.actors
{
    abstract class Enemy : IActable
    {
        protected Vector2f position;
        protected Sprite texture;
        protected int scale;
        protected string frame;

        public Enemy(int x, int y)
        {
            texture = Resources.ContentManager.Load<Sprite>(GlobalSpriteID.enemies);
            scale = Resources.Scale;
            position = new Vector2f(scale * x, scale * y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawScaledSprite(texture[frame].Controller, position, new Vector2(1, 1));
        }

        public bool IsExpired()
        {
            return false;
        }

        public float StartTime()
        {
            return 0f;
        }

        public virtual void Update(UltravioletTime time) {}

    }
}
