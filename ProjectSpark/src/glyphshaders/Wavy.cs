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
    class Wavy : GlyphShader
    {
        public override void Execute(ref GlyphShaderContext context, ref GlyphData data, int index)
        {
            var timeoffset = (Int32) (DateTime.UtcNow.TimeOfDay.TotalMilliseconds / 25); // number here is speed
            var angle = ((timeoffset + index) / 60.0) * Math.PI * 2.0; // change for x scale
            data.Y += (Single) Math.Sin(angle) * 10f; // number there is y scale
        }
    }
}
