using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.Shapes;
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer.Scene
{
    public class Camera : Gameobject
    {
        public Rectangle Space { get; private set; } // Screen Space
        public float Aspect { get; private set; }
        public Vector2 Fov { get; private set; }
        public float NearClippingPlane { get; private set; }
        public float FarClippingPlane { get; private set; }

        public float FocalLength => (float)(Window.Instance.Width / (2 * Math.Tan(Fov.x / 2)));

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
        /// <returns>A Tuple with the vector, and whether or not it is on screen.</returns>
		public Tuple<Vector2, bool> ProjectPoint(Vector3 point)  
        {
            // Translate point relative to camera position
            Vector3 translatedPoint = point - Transform.Position;

            // Rotate point based on camera's orientation (yaw and pitch)
            Vector3 rotatedPoint = Quaternion.Rotate(translatedPoint, Transform.Rotation);


            Vector2 pointIn2D = new Vector2(rotatedPoint.x, rotatedPoint.y);

            float zClamped = Math.Max(rotatedPoint.z, NearClippingPlane); // Ensure depth is positive

            // Perform perspective projection
            Vector2 projected = (pointIn2D * FocalLength) / zClamped + Window.Instance.ScreenCenter;

            // Point Not On Screen
            if (projected.x > Window.Instance.ScreenSize.x || projected.x < 0 ||
                projected.y > Window.Instance.ScreenSize.y || projected.y < 0 ||
                zClamped > FarClippingPlane)
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
