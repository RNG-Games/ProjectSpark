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
        float speed;
        private bool dead = false;
        private static bool spawned = false;
        private static Player instance;
        private Vector2f borders;
        private bool onLine = false;
        private bool fixLine = false;
        private float currLine = 0;

        private int leftBorder = int.MinValue;
        private int rightBorder = int.MaxValue;

        private ParticleSystem system;
        private UniversalEmitter emitter;

        Vector2f gravity = new Vector2f(0, 30);
        Vector2f velocity = new Vector2f(0, 30);

        private Player()
        {
            position = new Vector2f(1000, 0);
            speed = 800f;
            texture = new Sprite(Resources.GetTexture("player.png")) { Position = position };
            system = new ParticleSystem(Resources.GetTexture("pixel.png"));
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
            velocity += _deltaTime * gravity;
            }

            var move = new Vector2f(0, 0);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                move.X -= speed * _deltaTime;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                move.X += speed * _deltaTime;

            position += move;
            if (position.X < leftBorder) position.X = leftBorder;
            if (position.X > rightBorder - scale) position.X = rightBorder - scale;
            texture.Position = position;

            //just for test purposes:

            if (position.Y > 2000)
            {
                position.Y = 0;
                velocity = new Vector2f(0, 500);
            }

            if (fixLine)
            {
                position.Y = currLine;
                onLine = true;
            }

            leftBorder = int.MinValue;
            rightBorder = int.MaxValue;
            fixLine = false;
            currLine = 0;
        }

        public Circle hitbox()
        {
            return new Circle(position + new Vector2f(scale/2, scale/2), scale/2);
        }

        public void kill()
        {
            system.Position = position + new Vector2f(12,12);
            emitter.ParticleVelocity = Distributions.Deflect(new Vector2f(500, 500), 360f);
            emitter.ParticleRotation = Distributions.Uniform(0f, 0f);
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

        public void setCurrLine(float y)
        {
            fixLine = true;
            currLine = y;
        }
    }
}
