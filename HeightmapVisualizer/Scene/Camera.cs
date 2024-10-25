using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.Shapes;
using HeightmapVisualizer.Units;
using System.Numerics;

namespace HeightmapVisualizer.Scene
{
    public class Camera : Gameobject
    {
        public Rectangle Space { get; private set; } // Screen Space
        public float Aspect { get; private set; }
        public Vector2 Fov { get; private set; }
        public float NearClippingPlane { get; private set; }
        public float FarClippingPlane { get; private set; }

        public float FocalLength => (float)(Window.Instance.Width / (2 * Math.Tan(Fov.X / 2)));

        public Camera(Transform transform,
            Rectangle space,
            float aspect = 16f / 9f, // This is currently not tied to anything
            float fov = 90f,
            float nearClippingPlane = 0.0001f,
            float farClippingPlane = 100000f) : base(transform)
        {
            this.Space = space;
            this.Aspect = aspect;
            this.Fov = new Vector2(fov, fov / aspect);
            this.NearClippingPlane = nearClippingPlane;
            this.FarClippingPlane = farClippingPlane;
        }


		// Project a 3D point to 2D screen space with perspective
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns>A Tuple with the vector, and whether or not it is on screen.</returns> // LOOK INTO HOW VERECTOR3 IS IMPLEMENTED. IS IT DEFAWULT TO NULL? 
		public Tuple<Vector2, bool> ProjectPoint(Vector3 point)  
        {
            // Translate point relative to camera position
            Vector3 translatedPoint = point - Transform.Position;

            // Rotate point based on camera's orientation (yaw and pitch)
            Vector3 rotatedPoint = Transform.Rotate(translatedPoint, Transform.Rotation);


            Vector2 pointIn2D = new Vector2(rotatedPoint.X, rotatedPoint.Y);

            float zClamped = Math.Max(rotatedPoint.Z, NearClippingPlane); // Ensure depth is positive

            // Perform perspective projection
            Vector2 projected = (pointIn2D * FocalLength) / zClamped + Window.Instance.ScreenCenter;

            // Point Not On Screen
            if (projected.X > Window.Instance.ScreenSize.X || projected.X < 0 ||
                projected.Y > Window.Instance.ScreenSize.Y || projected.Y < 0)
            {
                return new Tuple<Vector2, bool>(projected, false);
            }

			return new Tuple<Vector2, bool>(projected, true);
		}

		public override Mesh? GetRenderable()
        {
            var debug = false;
            if (debug)
            {
                var combined = MeshUtility.CombineGeometry(
                    MeshUtility.CombineGeometry(
                        Line.CreateRay(Transform.Position, Transform.Forward, 5, Color.CornflowerBlue),
                        Line.CreateRay(Transform.Position, Transform.Up, 5, Color.Green)
                    ),
                    Line.CreateRay(Transform.Position, Transform.Right, 5, Color.Red)
                );
                return combined;
            }
            return null;
        }
    }
}
