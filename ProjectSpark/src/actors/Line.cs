using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _ProjectSpark.util;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace _ProjectSpark.actors
{

    abstract class Line : IActable
    {
        protected Vector2f borders;
        protected int scale = Resources.getScale();
        protected float position;
        protected bool enabled = false;
        protected bool done = false;
        protected bool onLine = false;
        public Line(Vector2f borders, float y)
        {
            this.borders = borders*scale;
            position = y*scale;
        }

        public virtual void Draw(RenderWindow _window)
        {
            Vertex[] line = { new Vertex(new Vector2f(borders.X, position), new Color(79,60,59)), new Vertex(new Vector2f(borders.Y, position), new Color(79, 60, 59)) };
            _window.Draw(line, 0, 2, PrimitiveType.Lines);
        }

        public bool IsExpired()
        {
            return false;
        }

        public float StartTime()
        {
            return 0f;
        }

        public virtual void Update(float _deltaTime)
        {
            if (done) return;
            Vector2f playerPos = Player.getPlayer().getPosition() + new Vector2f(scale/2, scale/2);
            Vector2f next;

            if (playerPos.X >= borders.X && playerPos.X <= borders.Y)
            {
                next = Player.getPlayer().getPosition() + _deltaTime * Player.getPlayer().getVelocity();
                if (next.Y > position - scale/2) Player.getPlayer().setCurrLine(position- scale/2, this);
            }

        }

        protected void resetLine()
        {
            Player.getPlayer().resetLine();
            done = true;
        }

        public void setLine()
        {
            onLine = true;
        }
    }
}
