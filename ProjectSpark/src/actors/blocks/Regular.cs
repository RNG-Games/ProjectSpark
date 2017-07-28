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
    class Regular : Block
    {
        public Regular(int x, int y) : base(x, y)
        {
            frame = "regular";
        }

        public override void Update(UltravioletTime time)
        {
            Vector2 playerPos = Player.getPlayer().position;

            /* if player is in y-range of block */
            if (playerPos.Y >= position.Y - scale && playerPos.Y <= position.Y + scale)
            {
                double newLeftBorder, newRightBorder;

                /* calculate maximum left and right position the player can go at current y-position, taking shape into account */
                if (position.Y - playerPos.Y > scale / 2 || playerPos.Y - position.Y > scale / 2)
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
                if (playerPos.X >= position.X && newLeftBorder > Player.getPlayer().leftBorder)
                {
                    Player.getPlayer().leftBorder = (int) newLeftBorder;
                }
                if (playerPos.X <= position.X && newRightBorder < Player.getPlayer().rightBorder)
                {
                    Player.getPlayer().rightBorder = (int) newRightBorder;
                }
            }
        }
    }
}
