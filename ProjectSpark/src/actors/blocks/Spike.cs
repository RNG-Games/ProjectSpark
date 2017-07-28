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
    class Spike : Block
    {
        public Spike(int x, int y) : base(x, y)
        {
            frame = "spike";
        }

        public override void Update(UltravioletTime time)
        {
            if (Player.getPlayer().hitbox().intersectsWithRectangle(position + new Vector2(2,2), 44, 44))
            {
                Player.getPlayer().kill();
            }
        }
    }
}
