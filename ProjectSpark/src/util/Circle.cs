using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace _ProjectSpark.util
{
    class Circle
    {
        public Vector2f middle { get; protected set; }
        public float radius { get; protected set; }

        public Circle() { }

        public Circle(Vector2f middle, float radius)
        {
            this.middle = middle;
            this.radius = radius;
        }

        public Circle(float x, float y, float radius)
        {
            middle = new Vector2f(x,y);
            this.radius = radius;
        }

        public void setPosition(float x, float y)
        {
            this.middle = new Vector2f(x,y);
        }

        public void setPosition(Vector2f middle)
        {
            this.middle = middle;
        }

        public void addToPosition(float x, float y)
        {
            this.middle += new Vector2f(x,y);
        }

        public void addToPostion(Vector2f move)
        {
            this.middle += move;
        }
        public bool intersectsWith(Circle c)
        {
            var distancePow2 = (c.middle.X - this.middle.X) * (c.middle.X - this.middle.X) + (c.middle.Y - this.middle.Y) * (c.middle.Y - this.middle.Y);
            return distancePow2 < (c.radius + radius) * (c.radius + radius);                        /* since distance ^2 is used, the 
                                                                                                       (radius + radius) is squared too*/
        }

        public bool intersectsWithRectangle(Vector2f topleft, int width, int height)
        {
            for (float i = topleft.X; i <= topleft.X + width; i++)
            {
                for (float j = topleft.Y; j <= topleft.Y + height; j++)
                {
                    var distance = (middle.X - i) * (middle.X - i) + (middle.Y - j) * (middle.Y - j);
                    if (distance <= radius * radius) return true;
                }
            }
            return false;
        }
    }



}
