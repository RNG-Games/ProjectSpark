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
using ProjectSpark.Input;

namespace ProjectSpark.actors.lines
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
                Player.getPlayer().SetCurrLine(position - scale / 2, this);
                Player.getPlayer().position = new Vector2f(this.borders.X + Math.Abs(this.borders.Y - this.borders.X)/2, y);
            }
        }

        public override void Update(UltravioletTime time)
        {

            base.Update(time);
            if (onLine)
            {
                if (!start) setCheckpoint?.Invoke();
                if (Resources.Input.GetActions().ActionKey.IsDown()) resetLine();
                Vector2f plPos = Player.getPlayer().position;
                if (Player.getPlayer().leftBorder < borders.X - scale / 2) Player.getPlayer().leftBorder = (int) borders.X - scale / 2;
                if (Player.getPlayer().rightBorder > borders.Y + scale / 2) Player.getPlayer().rightBorder = (int) borders.Y + scale / 2;
            }
        }

        
    }
}
