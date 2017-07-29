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

namespace ProjectSpark.actors.enemies
{
    class Blockwise : Enemy
    {
        private Queue<Vector2f> blocks = new Queue<Vector2f>();
        private int state = 0;
        private float frameCounter = 0;
        private Vector2f direction;
        private const float _v = 100;

        public Blockwise(Vector2f[] blocks) : base((int) blocks[0].X, (int) blocks[0].Y)
        {
            frame = "testenemy";
            for (int i = 0; i < blocks.Length; ++i)
            {
                this.blocks.Enqueue(blocks[i] * scale + new Vector2f(scale/2, scale/2));
            }
            position = this.blocks.Peek();
        }

        public override void Update(UltravioletTime time)
        {
            frameCounter += Resources.deltaTime;
            switch (state)
            {
                case 0:
                    wait(); break;
                case 1:
                    move(); break;
                default:
                    break;
            }
        }

        private void wait()
        {
            direction = calcDirection();
            state = 1;
        }

        private void move()
        {
            frameCounter = 0;
            position += direction * 0.01f; // * direction * Resources.deltaTime;

            float length = (blocks.Peek() - position).Length();
            //if (length < 10f) state = 0;          
        }

        private Vector2f calcDirection()
        {
            Vector2f _curr = blocks.Dequeue();
            blocks.Enqueue(_curr);
            Console.WriteLine(_curr);
            Vector2f _next = blocks.Dequeue();
            blocks.Enqueue(_next);
            Console.WriteLine(_next);
            Vector2f _dir = _next - _curr;
            _dir.Normalize();
            Console.WriteLine(_dir.Length());
            return _dir;
        }
    }
}
