using HeightmapVisualizer.src.Scene;
using HeightmapVisualizer.src.Utilities;
using System.Numerics;

namespace HeightmapVisualizer.src.Components
{
    public class OrthographicCameraComponent : CameraBase, IComponent
	{
		public Gameobject? Gameobject { get; set; }

		public OrthographicCameraComponent(Rectangle space,
			float aspect = 16f / 9f, 
			float fov = 90f,
			float nearClippingPlane = 0.0001f,
			float farClippingPlane = 100000f) : 
			base(space, aspect, fov, nearClippingPlane, farClippingPlane) { }

		public override Tuple<Vector2, bool> ProjectPoint(Vector3 point)
		{
			if (Gameobject == null) return null;

			// Translate point relative to camera position
			Vector3 translatedPoint = point - Gameobject.Transform.Position;

			// Rotate point based on camera's orientation (yaw and pitch)
			Vector3 rotatedPoint = Transform.Rotate(translatedPoint, Gameobject.Transform.Rotation);


			Vector2 pointIn2D = new Vector2(rotatedPoint.X, rotatedPoint.Y);

			float zClamped = Math.Max(rotatedPoint.Z, NearClippingPlane); // Ensure depth is positive

			// Perform perspective projection
			Vector2 projected = (pointIn2D * FocalLength) / 3f + Window.Instance.ScreenCenter;

			// Point Not On Screen
			if (projected.X > Window.Instance.ScreenSize.X || projected.X < 0 ||
				projected.Y > Window.Instance.ScreenSize.Y || projected.Y < 0)
			{
				return new Tuple<Vector2, bool>(projected, false);
			}

			return new Tuple<Vector2, bool>(projected, true);
		}

		public void Init(Gameobject gameobject)
		{
			this.Gameobject = gameobject;
		}

		public void Update()
		{
		}
	}
}
