using HeightmapVisualizer.Components;
using HeightmapVisualizer.Scene;
using HeightmapVisualizer.src.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HeightmapVisualizer.src.Components
{
	public class CameraComponent : IComponent
	{
		public Gameobject? Gameobject { get; set; }
		public Rectangle Space { get; private set; } // Screen Space
		public float Aspect { get; private set; }
		public Vector2 Fov { get; private set; }
		public float NearClippingPlane { get; private set; }
		public float FarClippingPlane { get; private set; }
		public float FocalLength => (float)(Window.Instance.Width / (2 * Math.Tan(Fov.X / 2)));

		public CameraComponent(Rectangle space,
			float aspect = 16f / 9f, 
			float fov = 90f,
			float nearClippingPlane = 0.0001f,
			float farClippingPlane = 100000f)
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
			if (Gameobject == null) return null;

			// Translate point relative to camera position
			Vector3 translatedPoint = point - Gameobject.Transform.Position;

			// Rotate point based on camera's orientation (yaw and pitch)
			Vector3 rotatedPoint = Transform.Rotate(translatedPoint, Gameobject.Transform.Rotation);


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

		public void Init(Gameobject gameobject)
		{
			this.Gameobject = gameobject;
		}

		public void Update()
		{
		}
	}
}
