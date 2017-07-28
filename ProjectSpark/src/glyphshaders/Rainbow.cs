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
    class Rainbow : GlyphShader
    {
        private static readonly Color[] colors = new[] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet };    
        public override void Execute(ref GlyphShaderContext context, ref GlyphData data, int index)
            {
                var offset = (Int32) (DateTime.UtcNow.TimeOfDay.TotalMilliseconds / 50);
                data.Color = colors[(index + offset) % colors.Length];
            }
    }
}
