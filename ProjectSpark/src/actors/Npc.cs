using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSpark.assets;
using ProjectSpark.input;
using ProjectSpark.util;
using TwistedLogik.Ultraviolet;
using TwistedLogik.Ultraviolet.Content;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D;
using TwistedLogik.Ultraviolet.Input;

namespace ProjectSpark.actors
{
    class Npc : IActable
    {
        string texture = "";
        string[] messages;
        Vector2f position;
        Sprite _texture;
        util.Circle hitbox;
        float scale = Resources.Scale;
        bool act = false;
        bool activated = false;
        Textbox _textbox;

        public Npc(float x, float y, string[] messages, string texture)
        {
            _texture = Resources.ContentManager.Load<Sprite>(GlobalSpriteID.npcs);
            this.position = new Vector2f(x*scale, y*scale);
            this.texture = texture;
            this.messages = messages;
            hitbox = new util.Circle(position + new Vector2f(scale / 2, scale / 2), scale / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawScaledSprite(_texture[texture].Controller, position, new Vector2(1, 1));
            if (act) spriteBatch.DrawScaledSprite(_texture["npcsign"].Controller, position + new Vector2f(0, -80), new Vector2(1, 1));
        }

        public bool IsExpired()
        {
            return false;
        }

        public float StartTime()
        {
            return 0f;
        }

        public void Update(UltravioletTime time)
        {
            if (Player.getPlayer().hitbox().intersectsWith(hitbox) && !activated) act = true;
            else act = false;

            if (act && Resources.Input.GetActions().UpKey.IsPressed(true))
            {
                _textbox = new Textbox(messages, position + new Vector2f(48, -64));
                activated = true;
                Resources.actorBuffer.Add(_textbox);
            }

            if (activated && _textbox.IsExpired()) activated = false;
        }
    }
}
