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

namespace ProjectSpark.actors.lines
{
    class Transition : Line
    {        

        public Transition(Vector2f borders, float y) : base(borders, y) {}

        public override void Update(UltravioletTime time)
        {
            base.Update(time);
            if (onLine)
            {

                if (!enabled){
                    //Program.MoveCameraDown(position - scale, 3, resetLine);
                }
                enabled = true;
                Vector2f plPos = Player.getPlayer().position;
                if (Player.getPlayer().leftBorder < borders.X - scale / 2) Player.getPlayer().leftBorder = (int)borders.X - scale / 2;
                if (Player.getPlayer().rightBorder > borders.Y + scale / 2) Player.getPlayer().rightBorder = (int)borders.Y + scale / 2;
            }
        }
    }
}
