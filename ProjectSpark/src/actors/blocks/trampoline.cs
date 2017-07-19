using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _ProjectSpark.util;
using _ProjectSpark.actors;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace _ProjectSpark.actors.blocks
{
    class Trampoline : Block
    {
        public Trampoline(int x, int y) : base(x, y)
        {
            texture = new Sprite(Resources.GetTexture("trampoline.png")) { Position = position };
        }

        public override void Update(float _deltaTime)
        {
            if (Player.getPlayer().hitbox().intersectsWithRectangle(position + new Vector2f(2, 2), 44, 44))
            {

                    Player.getPlayer().setVelocity(-700);            
            }
        }


    }
}
