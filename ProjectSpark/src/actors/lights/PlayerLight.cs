using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace _ProjectSpark.actors.lights
{
    class PlayerLight : Light
    {
        
        public PlayerLight() : base() {
            texture = new Sprite(Resources.GetTexture("light.png")) { Position = position };
        }

        public override void Update(float _deltaTime)
        {
            position = Player.getPlayer().getPosition();
            texture.Position = position;
        }
    }
}
