using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSpark.assets;
using TwistedLogik.Ultraviolet;
using TwistedLogik.Ultraviolet.Content;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D;
using TwistedLogik.Ultraviolet.Input;

namespace ProjectSpark.actors
{
    abstract class Block : IActable
    {
        protected Vector2 position;
        protected int scale;
        protected string frame;
        protected Sprite texture;

        public Block(int x, int y)
        {
            texture = Resources.ContentManager.Load<Sprite>(GlobalSpriteID.blocks);
            scale = Resources.Scale;
            position = new Vector2(scale * x, scale * y);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawScaledSprite(texture[frame].Controller, position, new Vector2(1, 1), Color.White, 0, SpriteEffects.None, 0);
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
