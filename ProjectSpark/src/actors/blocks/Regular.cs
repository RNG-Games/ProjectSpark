using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _ProjectSpark.util;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace _ProjectSpark.actors.blocks
{
    class Regular : Block
    {
        public Regular(int x, int y) : base(x, y)
        {
            texture = new Sprite(Resources.GetTexture("testblock.png")) { Position = position };
        }

        public override void Update(float _deltaTime)
        {
            Vector2f playerPos = Player.getPlayer().getPosition();

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
                if (playerPos.X >= position.X && newLeftBorder > Player.getPlayer().getLeftBorder())
                {
                    Player.getPlayer().setLeftBorder((int) newLeftBorder);
                }
                if (playerPos.X <= position.X && newRightBorder < Player.getPlayer().getRightBorder())
                {
                    Player.getPlayer().setRightBorder((int) newRightBorder);
                }
            }
        }
    }
}
