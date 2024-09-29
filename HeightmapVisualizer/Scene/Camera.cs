
using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer.Scene
{
    internal class Camera : Gameobject
    { 
        public Rectangle Space; // Screen Space
        public float Aspect;
        public float Fov;
        public float NearClippingPlane;

        public Camera(Transform transform,
			Rectangle space, 
            float aspect = 16f/9f, 
            float fov = 90f, 
            float nearClippingPlane = 0.1f) : base(transform)
        {
            this.Space = space;
            this.Aspect = aspect;
            this.Fov = fov;
            this.NearClippingPlane = nearClippingPlane;
        } 


		// Project a 3D point to 2D screen space with perspective
		public Vector2 ProjectVertex(Vertex vertex)
        {
            // Translate point relative to camera position
            Vector3 translatedPoint = vertex.Position() - Transform.Position;

            // Rotate point based on camera's orientation (yaw and pitch)
            Vector3 rotatedPoint = Quaternion.Rotate(translatedPoint, Transform.Rotation);

            // Perform perspective projection
            float zClamped = Math.Max(rotatedPoint.z, NearClippingPlane); // Ensure depth is positive

            // Convert to 2D screen coordinates
            float xScreen = Fov * rotatedPoint.x / zClamped + Space.Width / 2;
            float yScreen = Fov * rotatedPoint.y / zClamped + Space.Height / 2;

            return new Vector2(xScreen, yScreen);
        }
	}
}
