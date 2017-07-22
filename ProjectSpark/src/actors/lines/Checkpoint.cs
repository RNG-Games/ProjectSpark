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
        public Action setCheckpoint;
        bool start = false;
        public Checkpoint(bool start, Vector2f borders, float y) : base(borders, y)
        {
            this.start = start;
            if (start)
            {
                Player.getPlayer().setCurrLine(position - scale / 2, this);
                Player.getPlayer().setPosition(new Vector2f(this.borders.X + Math.Abs(this.borders.Y - this.borders.X)/2, y));
            }
        }

        public override void Update(float _deltaTime)
        {

            base.Update(_deltaTime);
            if (onLine)
            {
                if (!start) setCheckpoint?.Invoke();
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space)) resetLine();
                Vector2f plPos = Player.getPlayer().getPosition();
                if (Player.getPlayer().getLeftBorder() < borders.X - scale / 2) Player.getPlayer().setLeftBorder((int)borders.X - scale / 2);
                if (Player.getPlayer().getRightBorder() > borders.Y + scale / 2) Player.getPlayer().setRightBorder((int)borders.Y + scale / 2);
            }
        }

        
    }
}
