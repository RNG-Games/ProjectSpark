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
    class Decoration : Block
    {
        public Decoration(int x, int y, string texture) : base(x, y)
        {
            this.texture = new Sprite(Resources.GetTexture(texture)) { Position = position };
        }

        public new virtual Memento<IActable> Save() => new Memento<IActable>(this);
    }
}
