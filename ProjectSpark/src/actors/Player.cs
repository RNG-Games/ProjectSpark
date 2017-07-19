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
    class Player : IActable
    {
        Vector2f position;
        Sprite texture;
        float speed;
        private bool leftlock = false;
        private bool rightlock = false;
        private bool dead = false;
        private static bool spawned = false;
        private static Player instance;
        private Vector2f borders;
        private bool onLine = false;

        Vector2f gravity = new Vector2f(0, 500);
        Vector2f velocity = new Vector2f(0, 500);

        private Player()
        {
            position = new Vector2f(1000, 0);
            speed = 800f;
            texture = new Sprite(Resources.GetTexture("player.png")) { Position = position };
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
            if (dead) return;
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
            if (dead) return;

            if (!onLine) { 
            position += _deltaTime * velocity;
            velocity += _deltaTime * gravity;
            }

            var move = new Vector2f(0, 0);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left) && !leftlock)
                move.X -= speed * _deltaTime;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right) && !rightlock)
                move.X += speed * _deltaTime;

            position += move;
            texture.Position = position;

            //just for test purposes:
            if (position.Y > 1000)
            {
                position.Y = 0;
                velocity = new Vector2f(0, 500);
            }

            leftlock = false;
            rightlock = false;
        }

        public Circle hitbox()
        {
            return new Circle(position + new Vector2f(24, 24), 24);
        }

        public void setLeftlock()
        {
            leftlock = true;
        }

        public void setRightlock()
        {
            rightlock = true;
        }

        public void kill()
        {
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

        public void setLine()
        {
            onLine = true;
        }

        public void resetLine()
        {
            onLine = false;
        }
    }
}
