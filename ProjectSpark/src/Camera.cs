using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSpark.util;
using TwistedLogik.Ultraviolet;

namespace ProjectSpark
{
    public class Camera
    {
        public float Zoom { get; set; }
        public Vector2f Position { get; set; }
        public float Rotation { get; set; }
        public Vector2f Origin { get; set; }

        private Vector3 oldScreenScale = new Vector3(1,1,1);

        public Camera()
        {
            Zoom = 1;
            Position = new Vector2f(0, 0);
            Rotation = 0;
            Origin = new Vector2f(0, 0);
            Position = new Vector2f(0, 0);
        }

        public void Move(Vector2f direction)
        {
            Position += direction;
        }

        public Vector3 GetScreenScale(IUltravioletPlatform gfx, float width, float height)
        {
            if(gfx.Windows.GetPrimary() == null) return oldScreenScale;
            var scaleX = gfx.Windows.GetPrimary().DrawableSize.Width / width;
            var scaleY = gfx.Windows.GetPrimary().DrawableSize.Height / height;
            oldScreenScale = new Vector3(scaleX, scaleY, 1.0f);
            return oldScreenScale;
        }


        public Matrix GetTransform()
        {
            var translationMatrix = Matrix.CreateTranslation(new Vector3(Position.X, Position.Y, 0));
            var rotationMatrix = Matrix.CreateRotationZ(Rotation);
            var scaleMatrix = Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));
            var originMatrix = Matrix.CreateTranslation(new Vector3(Origin.X, Origin.Y, 0));

            return translationMatrix * rotationMatrix * scaleMatrix * originMatrix;
        }
    }
}
