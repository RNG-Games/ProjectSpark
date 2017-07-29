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

namespace ProjectSpark.actors
{
    class Collectible : IActable
    {
        Vector2f position;
        util.Circle hitbox;
        float scale = Resources.Scale;
        bool collected = false;
        Sprite texture;
        int index = 0;

        public Collectible(float x, float y, int index)
        {
            this.index = index;
            position = new Vector2f(x, y) * scale;
            texture = Resources.ContentManager.Load<Sprite>(GlobalSpriteID.collectables);
            hitbox = new util.Circle(position + new Vector2f(scale / 2, scale / 2), scale / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawScaledSprite(texture["placeholder"].Controller, position, new Vector2(1, 1));
        }

        public bool IsExpired()
        {
            return collected;
        }

        public float StartTime()
        {
            return 0f;
        }

        public void Update(UltravioletTime time)
        {
            if (Player.getPlayer().hitbox().intersectsWith(hitbox)) { collected = true; ++Resources.collectables; }
        }
    }
}
