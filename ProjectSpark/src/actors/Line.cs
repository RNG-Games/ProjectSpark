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
    public abstract class Line : IActable
    {
        protected Vector2f borders;
        protected int scale = Resources.Scale;
        protected float position;
        protected bool enabled = false;
        protected bool done = false;
        protected bool onLine = false;

        private float scaleX;
        Sprite texture;

        public Line(Vector2f borders, float y)
        {
            scaleX = borders.Y - borders.X;
            this.borders = borders*scale;
            position = (y+0.5f)*scale;
            texture = Resources.ContentManager.Load<Sprite>(GlobalSpriteID.lines);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawScaledSprite(texture["testline"].Controller, new Vector2(borders.X, position), new Vector2(scaleX, 1));
        }

        public bool IsExpired()
        {
            return false;
        }

        public float StartTime()
        {
            return 0f;
        }

        public virtual void Update(UltravioletTime time)
        {

            if (done) return;
            Vector2f playerPos = Player.getPlayer().position + new Vector2f(scale/2, scale/2);
            Vector2f next;

            if (playerPos.X >= borders.X && playerPos.X <= borders.Y)
            {
                next = Player.getPlayer().position + Resources.deltaTime * Player.getPlayer().velocity;
                if (next.Y > position - scale/2 && next.Y <= position) Player.getPlayer().SetCurrLine(position- scale/2, this);
            }

        }

        protected void resetLine()
        {
            Player.getPlayer().resetLine();
            Player.getPlayer().resetVelocity();
            onLine = false;
            done = true;
        }

        public void setLine()
        {
            onLine = true;
        }
    }
}
