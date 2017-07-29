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
        private const float _v = 300;
        private util.Circle hitbox;
        private float waitTime;
        public Blockwise(Vector2f[] blocks, float wait) : base((int) blocks[0].X, (int) blocks[0].Y)
        {
            frame = "testenemy";
            for (int i = 0; i < blocks.Length; ++i)
            {
                this.blocks.Enqueue(blocks[i] * scale);
            }
            position = this.blocks.Peek();

            waitTime = wait;
            hitbox = new util.Circle(position + new Vector2f(scale / 2, scale / 2), scale / 2);
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
            if (Player.getPlayer().hitbox().intersectsWith(hitbox))
            {
                Player.getPlayer().kill();
            }
        }

        private void wait()
        {
            direction = calcDirection();
            if (frameCounter >= waitTime) state = 1;
        }

        private void move()
        {
            frameCounter = 0;
            position += _v * direction * Resources.deltaTime;

            float length = (blocks.Peek() - position).Length();
            if (length < 5) state = 0;          
        }

        private Vector2f calcDirection()
        {
            Vector2f _curr = blocks.Dequeue();
            blocks.Enqueue(_curr);
            Vector2f _next = blocks.Peek();
            Vector2f _dir = _next - _curr;
            _dir.Normalize();
            return _dir;
        }
    }
}
