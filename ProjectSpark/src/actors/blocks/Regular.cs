﻿using System;
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
            if (playerPos.Y >= position.Y && playerPos.Y <= position.Y + scale)
            {
                if (playerPos.X >= position.X && position.X + scale > Player.getPlayer().getLeftBorder())
                {
                    Player.getPlayer().setLeftBorder((int) position.X + scale);
                }
                if (playerPos.X <= position.X && position.X < Player.getPlayer().getRightBorder())
                {
                    Player.getPlayer().setRightBorder((int) position.X);
                }
            }
        }
    }
}
