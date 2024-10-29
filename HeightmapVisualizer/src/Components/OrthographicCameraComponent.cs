﻿using HeightmapVisualizer.src.Scene;
using HeightmapVisualizer.src.Utilities;
using System.Numerics;

namespace HeightmapVisualizer.src.Components
{
    public class OrthographicCameraComponent : CameraBase
	{
		public OrthographicCameraComponent(
			float aspect = 16f / 9f, 
			float fov = 90f,
			float nearClippingPlane = 0.0001f,
			float farClippingPlane = 100000f,
			int priority = 10) : 
			base(aspect, fov, nearClippingPlane, farClippingPlane, priority) { }

		public override Tuple<PointF, bool>? ProjectPoint(Vector3 point, Rectangle bounds)
		{
			if (Gameobject == null) return null;

			// Translate point relative to camera position
			Vector3 translatedPoint = point - Gameobject.Transform.Position;

			// Rotate point based on camera's orientation (yaw and pitch)
			Vector3 rotatedPoint = Transform.Rotate(translatedPoint, Gameobject.Transform.Rotation);


			Vector2 pointIn2D = new Vector2(rotatedPoint.X, rotatedPoint.Y);

			// Perform perspective projection
			Vector2 projected = (pointIn2D * FocalLength) / 3f + new Vector2(bounds.Width / 2, bounds.Height / 2);

			// Convert to PointF
			PointF pointF = new PointF(projected.X, projected.Y);


			// Point Not On Screen
			if (projected.X > bounds.Width || projected.X < 0 ||
				projected.Y > bounds.Height || projected.Y < 0)
			{
				return new Tuple<PointF, bool>(pointF, false);
			}

			return new Tuple<PointF, bool>(pointF, true);
		}
	}
}
