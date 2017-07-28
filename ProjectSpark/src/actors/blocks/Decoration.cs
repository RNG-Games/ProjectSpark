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
    [Serializable]
    class Decoration : Block
    {
        public Decoration(int x, int y, string texture) : base(x, y)
        {
            frame = texture;
        }
    }
}
