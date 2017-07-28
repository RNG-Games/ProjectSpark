using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSpark.assets;
using TwistedLogik.Ultraviolet;
using TwistedLogik.Ultraviolet.Content;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D;
using TwistedLogik.Ultraviolet.Input;

namespace ProjectSpark.actors
{
    public class Player : IActable
    {
        private int scale = Resources.Scale;
        public Vector2 position { get; set; }
        private Sprite texture;
        private bool dead = false;
        private static bool spawned = false;
        private static Player instance;
        private Vector2 borders;
        public bool onLine { get; private set; } = false;
        private bool fixLine = false;
        //private Line currLine = null;
        private float currLineY = 0;
        private Vector2 direction = new Vector2(0,0);
        public int leftBorder { get; set; } = int.MinValue;
        public int rightBorder { get; set; } = int.MaxValue;

        //Particles maybe

        private const float _spd = 500f;
        private const float _onLineSpd = 700f;
        private float speed = _spd;

        Vector2 gravity = new Vector2(0,800);
        Vector2 upwardGravity = new Vector2(0, 1200);
        private const float _vel = 500;
        public Vector2 velocity { get; set; }= new Vector2(0, _vel);

        public Action Respawn;

        private Player()
        {
            //onLine = true;
            position = new Vector2(1000,24);
            texture = Resources.ContentManager.Load<Sprite>(GlobalSpriteID.player);
        }

        public static Player getPlayer()
        {
            if(spawned) return instance;
            instance = new Player();
            spawned = true;
            return instance;
        }

        public void Update(UltravioletTime time)
        {
            if (dead)
            {
                return;
            }

            if (!onLine)
            {
                position += (float) time.ElapsedTime.TotalSeconds * velocity;

                if (velocity.Y < 0)
                {
                    velocity += (float) time.ElapsedTime.TotalSeconds * upwardGravity;
                }
                else
                {
                    velocity += (float) time.ElapsedTime.TotalSeconds * gravity;
                }
            }

            var move = new Vector2(0,0);
            speed = onLine ? _onLineSpd : _spd;
            //TODO: Input
            /*
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                    move.X -= speed * _deltaTime;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                    move.X += speed * _deltaTime;
            }
            */
            position += move;
            if (position.X < leftBorder) position = new Vector2(leftBorder, position.Y);
            if (position.X > rightBorder - scale) position = new Vector2(rightBorder - scale, position.Y);

            //just for test purposes:
            if (position.Y > 2000)
            {
                resetVelocity();
                position = new Vector2(position.X, 0);
            }

            if (fixLine)
            {
                position = new Vector2(position.X, currLineY);
                //currLine.setLine();
                onLine = true;
            }

            leftBorder = int.MinValue;
            rightBorder = int.MaxValue;
            fixLine = false;
            //currLine = null
            direction += new Vector2(move.X, velocity.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (dead)
            {
                return;
            }
            spriteBatch.DrawScaledSprite(texture["player"].Controller, position, new Vector2(1,1));
        }

        public bool IsExpired()
        {
            return false;
        }

        public util.Circle hitbox()
        {
            return new util.Circle(position + new Vector2(scale/2f, scale/2f), scale/2f);
        }

        public void kill()
        {
            dead = true;
            Respawn?.Invoke();
        }

        public void unkill()
        {
            dead = false;
        }

        public void resetLine()
        {
            onLine = false;
        }

        public void setCurrLine(float y/*, Line currLine*/)
        {
            //this.currLine = currLine;
            currLineY = y;
            fixLine = true;
        }

        public void resetVelocity()
        {
            velocity = new Vector2(velocity.X, _vel);
        }

        public float StartTime()
        {
            return 0f;
        }
    }
}
