using System;
using TwistedLogik.Ultraviolet;

namespace ProjectSpark.util
{ 
    public struct Vector2f
    {

        public float X;
        public float Y;


        public Vector2f(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2f operator +(Vector2f v1, Vector2f v2) => new Vector2f(v1.X + v2.X, v1.Y + v2.Y);
        public static Vector2f operator -(Vector2f v1, Vector2f v2) => new Vector2f(v1.X - v2.X, v1.Y - v2.Y);
        public static Vector2f operator *(Vector2f v, float a) => new Vector2f(a * v.X, a * v.Y);
        public static Vector2f operator *(float a, Vector2f v) => v * a;
        public static float operator *(Vector2f v1, Vector2f v2) => v1.X * v2.X + v1.Y * v2.Y;

        public float Length() => (float)Math.Sqrt(LengthSquared());
        public float LengthSquared() => this * this;
        public void Normalize()
        {
            X = X / Length();
            Y = Y / Length();
        }

        public override string ToString() => $"Vector2({X},{Y})";
        public override int GetHashCode() => ((Vector2)this).GetHashCode();

        public override bool Equals(object obj)
        {
            const float tolerance = 0.0001f;
            if (obj is Vector2f o1)
            {
                return Math.Abs(o1.X - X) < tolerance && Math.Abs(o1.Y - Y) < tolerance;
            }
            if (obj is Vector2 o2)
            {
                return Math.Abs(o2.X - X) < tolerance && Math.Abs(o2.Y - Y) < tolerance;
            }
            return false;
        }

        public static implicit operator Vector2(Vector2f v) => new Vector2(v.X, v.Y);
        public static implicit operator Vector2f(Vector2 v) => new Vector2f(v.X, v.Y);
    }
}
