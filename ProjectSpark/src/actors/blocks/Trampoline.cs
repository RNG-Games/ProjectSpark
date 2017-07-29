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

namespace ProjectSpark.actors.blocks
{
    class Trampoline : Block
    {
        private bool expire = false;
        public Trampoline(int x, int y) : base(x, y)
        {
            frame = "trampoline";
        }

        public override bool IsExpired()
        {
            return expire;
        }

        public override void Update(UltravioletTime time)
        {
            bool bounce = false;
            Vector2 playerPos = Player.getPlayer().position;
            /* if player is in y-range of the block */
            if (playerPos.Y >= position.Y - scale && playerPos.Y <= position.Y + scale)
            {
                double newLeftBorder, newRightBorder;

                /* check if the player has to bounce */
                if (position.Y - playerPos.Y >= scale / 2)
                {
                    bounce = true;
                }

                /* calculate maximum left and right position the player can go at current y-position, taking shape into account */
                if (playerPos.Y - position.Y > scale / 2)
                {
                    newLeftBorder = position.X + scale / 2 + Math.Sqrt(scale * scale / 4 - (Math.Abs(position.Y - playerPos.Y) - scale / 2) * (Math.Abs(position.Y - playerPos.Y) - scale / 2));
                    newRightBorder = position.X + scale / 2 - Math.Sqrt(scale * scale / 4 - (Math.Abs(position.Y - playerPos.Y) - scale / 2) * (Math.Abs(position.Y - playerPos.Y) - scale / 2));
                }
                else
                {
                    newLeftBorder = position.X + scale;
                    newRightBorder = position.X;
                }

                /* signals to player class */
                if (bounce && Player.getPlayer().hitbox().intersectsWithRectangle(position + new Vector2(2, 2), 44, 44))
                {
                    Player.getPlayer().velocity = new Vector2(0, -900);
                    expire = true;
                }
                if (!bounce && playerPos.X >= position.X && newLeftBorder > Player.getPlayer().leftBorder)
                {
                    Player.getPlayer().leftBorder = (int) newLeftBorder;
                }
                if (!bounce && playerPos.X <= position.X && newRightBorder < Player.getPlayer().rightBorder)
                {
                    Player.getPlayer().rightBorder = (int) newRightBorder;
                }
            }
        }
    }
}
