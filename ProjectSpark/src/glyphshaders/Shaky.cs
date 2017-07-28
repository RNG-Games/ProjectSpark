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

namespace ProjectSpark.glyphshaders
{
    class Shaky : GlyphShader
    {
        Random r = new Random();
        public override void Execute(ref GlyphShaderContext context, ref GlyphData data, int index)
        {
            data.X += r.Next(-2, 2);
            data.Y += r.Next(-2, 2);
        }
    }
}
