using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetEXT.Animation;
using _ProjectSpark.util;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using NetEXT.Particles;
using NetEXT.MathFunctions;

namespace _ProjectSpark.actors
{
    class Player : IActable
    {
        int scale = Resources.getScale();
        Vector2f position;
        Sprite texture;
        private bool dead = false;
        private static bool spawned = false;
        private static Player instance;
        private Vector2f borders;
        private bool onLine = false;
        private bool fixLine = false;
        private Line currLine = null;
        private float currLineY = 0;
        private Vector2f direction = new Vector2f(0,0);
        private int leftBorder = int.MinValue;
        private int rightBorder = int.MaxValue;

        private ParticleSystem system;
        private UniversalEmitter emitter;

        const float _spd = 500f;
        const float _onLineSpd = 700f;
        float speed = _spd;

        Vector2f gravity = new Vector2f(0, 800);
        Vector2f upwardGravity = new Vector2f(0, 1200);
        private const float _vel = 500;
        Vector2f velocity = new Vector2f(0, _vel);

        private Player()
        {
            onLine = true;
            position = new Vector2f(1000, 24);
            texture = new Sprite(Resources.GetTexture("player.png")) { Position = position };
            system = new ParticleSystem(Resources.GetTexture("player.png"));
            emitter = new UniversalEmitter();
            emitter.EmissionRate = 1000f;
            emitter.ParticleLifetime =
                NetEXT.MathFunctions.Distributions.Uniform(Time.FromSeconds(0.001f), Time.FromSeconds(0.7f));
            system.AddEmitter(emitter, Time.FromSeconds(0.3f));
        }

        public static Player getPlayer()
        {
            if (spawned) return instance;
            instance = new actors.Player();
            spawned = true;
            return instance; 
            
        }

        public Vector2f getPosition()
        {
            return position;
        }

        public void Draw(RenderWindow _window)
        {
            if (dead)
            {
                system.Draw(_window,RenderStates.Default);
                return;
            }
            _window.Draw(texture);
        }

        public bool IsExpired()
        {
            return false;
        }

        public virtual Memento<IActable> Save() => new Memento<IActable>(this);

        public float StartTime()
        {
            return 0f;
        }

        public void Update(float _deltaTime)
        {
            
            if (dead)
            {
                system.Update(Time.FromSeconds(_deltaTime));
                return;
            }

            if (!onLine) { 
            position += _deltaTime * velocity;
            if (velocity.Y < 0)
                {
                    velocity += _deltaTime * upwardGravity;
                }
            else
                {
                    velocity += _deltaTime * gravity;
                }
            }

            var move = new Vector2f(0, 0);
            speed = onLine ? _onLineSpd : _spd;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                    move.X -= speed * _deltaTime;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                    move.X += speed * _deltaTime;
            }

            direction.X = move.X;
            position += move;
            if (position.X < leftBorder) position.X = leftBorder;
            if (position.X > rightBorder - scale) position.X = rightBorder - scale;
            texture.Position = position;

            //just for test purposes:

            if (position.Y > 2000)
            {
                resetVelocity();
                position.Y = 0;
            }

            if (fixLine)
            {
                position.Y = currLineY;
                currLine.setLine();
                onLine = true;
            }

            leftBorder = int.MinValue;
            rightBorder = int.MaxValue;
            fixLine = false;
            currLine = null;
            direction.Y = velocity.Y;
        }

        public Circle hitbox()
        {
            return new Circle(position + new Vector2f(scale/2, scale/2), scale/2);
        }

        public void kill()
        {
            /*system.Position = position + new Vector2f(12,12);
            emitter.ParticleVelocity = Distributions.Deflect(direction, 15f);
            //emitter.ParticleRotation = Distributions.Uniform(0f, 0f);
            emitter.ParticleScale = new Vector2f(0.5f, 0.5f);*/
            dead = true;
        }

        public void setVelocity(float v)
        {
            velocity = new Vector2f(velocity.X, v);
        }

        public void setVelocity(Vector2f v)
        {
            velocity = v;
        }

        public void setBorders(float x, float y)
        {
            borders = new Vector2f(x, y);
        }

        public bool getOnLine()
        {
            return onLine;
        }

        public void resetLine()
        {
            onLine = false;
        }

        public int getLeftBorder()
        {
            return leftBorder;
        }

        public int getRightBorder()
        {
            return rightBorder;
        }

        public void setLeftBorder(int value)
        {
            leftBorder = value;
        }

        public void setRightBorder(int value)
        {
            rightBorder = value;
        }

        public Vector2f getVelocity()
        {
            return velocity;
        }

        public void setCurrLine(float y, Line currLine)
        {
            this.currLine = currLine;
            currLineY = y;
            fixLine = true;
        }

        public void resetVelocity()
        {
            velocity.Y = _vel;
        }

        public void setPosition(Vector2f pos)
        {
            position = pos;
        }
    }
}
