using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _ProjectSpark.util;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace _ProjectSpark.actors.lines
{

    class Checkpoint : Line
    {
        bool start = false;
        public Checkpoint(bool start, Vector2f borders, float y) : base(borders, y)
        {
            this.start = start;
            this.borders = borders * scale;
            position = y * scale;
        }

        public override void Update(float _deltaTime)
        {
            base.Update(_deltaTime);
            if (onLine)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space)) resetLine();
                Vector2f plPos = Player.getPlayer().getPosition();
                if (Player.getPlayer().getLeftBorder() < borders.X - scale / 2) Player.getPlayer().setLeftBorder((int)borders.X - scale / 2);
                if (Player.getPlayer().getRightBorder() > borders.Y + scale / 2) Player.getPlayer().setRightBorder((int)borders.Y + scale / 2);
            }
        }
    }
}
