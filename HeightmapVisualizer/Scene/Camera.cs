using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.Shapes;
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer.Scene
{
    public class Camera : Gameobject
    {
        public float AspectRatio => (float)Window.Instance.ClientSize.X / Window.Instance.ClientSize.Y;

        public float FocalLength => (float)(1 / (2 * Math.Tan(MathF.PI / 180 * (Fov.x / 2))));
        public Vector2 Fov { get; private set; }
        public float NearClippingPlane { get; private set; }
        public float FarClippingPlane { get; private set; }

        public Camera(Transform transform,
            float fov = 90f,
            float nearClippingPlane = 0.0001f,
            float farClippingPlane = 100000f) : base(transform)
        {
            this.Fov = new Vector2(fov, fov / AspectRatio);
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
            // Translate point relative to the camera position
            Vector3 translatedPoint = point - Transform.Position;

            // Rotate point based on camera's orientation (yaw and pitch)
            Vector3 rotatedPoint = Quaternion.Rotate(translatedPoint, Transform.Rotation);

            // Clamping z to the near clipping plane (ensure depth is positive)
            float zClamped = Math.Max(rotatedPoint.z, NearClippingPlane);

            // Perform perspective projection
            Vector2 projected = new Vector2(
                (rotatedPoint.x * FocalLength) / zClamped,  // Project the x-coordinate
                (rotatedPoint.y * FocalLength) / zClamped * -1 * AspectRatio   // Project the y-coordinate
            );

            // Check if point is outside the view frustum in NDC space (-1 to 1 range)
            if (projected.x > 1 || projected.x < -1 ||
                projected.y > 1 || projected.y < -1 ||
                zClamped > FarClippingPlane)
            {
                return new Tuple<Vector2, bool>(projected, false);
            }

            return new Tuple<Vector2, bool>(projected, true);  // Point is inside the frustum
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
