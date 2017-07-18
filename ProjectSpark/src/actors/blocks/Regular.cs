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

        }
    }
}
