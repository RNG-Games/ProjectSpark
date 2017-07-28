using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistedLogik.Ultraviolet;

namespace ProjectSpark.util
{
    public class Circle
    {
        public Vector2 middle { get; protected set; }
        public float radius { get; protected set; }

        public Circle() { }

        public Circle(Vector2 middle, float radius)
        {
            this.middle = middle;
            this.radius = radius;
        }

        public Circle(float x, float y, float radius)
        {
            middle = new Vector2(x,y);
            this.radius = radius;
        }

        public void setPosition(float x, float y)
        {
            this.middle = new Vector2(x,y);
        }

        public void setPosition(Vector2 middle)
        {
            this.middle = middle;
        }

        public void addToPosition(float x, float y)
        {
            this.middle += new Vector2(x,y);
        }

        public void addToPostion(Vector2 move)
        {
            this.middle += move;
        }
        public bool intersectsWith(Circle c)
        {
            var distancePow2 = (c.middle.X - this.middle.X) * (c.middle.X - this.middle.X) + (c.middle.Y - this.middle.Y) * (c.middle.Y - this.middle.Y);
            return distancePow2 < (c.radius + radius) * (c.radius + radius);                        /* since distance ^2 is used, the 
                                                                                                       (radius + radius) is squared too*/
        }

        public bool intersectsWithRectangle(Vector2 topleft, int width, int height)
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
