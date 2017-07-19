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

    class Line : IActable
    {
        Vector2f borders;
        float position;
        bool enabled = false;

        public Line(Vector2f borders, float y)
        {
            this.borders = borders;
            position = y;
        }

        public void Draw(RenderWindow _window)
        {
            Vertex[] line = { new Vertex(new Vector2f(borders.X, position)), new Vertex(new Vector2f(borders.Y, position)) };
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
            float length = Math.Abs(borders.Y - borders.X);
            if (Player.getPlayer().hitbox().intersectsWithRectangle(new Vector2f(borders.X, position + 18),  (int) length, 1))
            {
                Player.getPlayer().setLine();
                if (!enabled) Program.MoveCameraDown(position - 48, 3);
                enabled = true;
            }
            
        }
    }
}
