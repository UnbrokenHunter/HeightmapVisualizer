using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HeightmapVisualizer
{
	public class Camera
	{
		public static Camera Instance { get; private set; }

		public Vector3 position;  // Camera position (x_cam, y_cam, z_cam)
		public Vector2 space; // Screen Space
		public float aspect;
		public float fov;
		public float nearClippingPlane;
		public float yaw;         // Rotation around y-axis (left-right)
		public float pitch;       // Rotation around x-axis (up-down)

		public Camera(Vector3 position, Vector2 space, float aspect, float fov, float nearClippingPlane, float yaw, float pitch)
		{
			if (this == null)
				throw new NullReferenceException();
			else
				Instance = this;

			this.position = position;
			this.fov = fov;
			this.space = space;
			this.nearClippingPlane = nearClippingPlane;
			this.yaw = yaw * (float)Math.PI / 180;
			this.pitch = pitch * (float)Math.PI / 180;
		}

		// Rotate the point by yaw and pitch (camera rotation)
		public Vector3 Rotate(Vector3 point)
		{
			// Yaw (rotate around y-axis)
			float cosYaw = (float)Math.Cos(yaw);
			float sinYaw = (float)Math.Sin(yaw);
			float xRotated = cosYaw * point.X + sinYaw * point.Z;
			float zRotated = -sinYaw * point.X + cosYaw * point.Z;

			// Pitch (rotate around x-axis)
			float cosPitch = (float)Math.Cos(pitch);
			float sinPitch = (float)Math.Sin(pitch);
			float yRotated = cosPitch * point.Y - sinPitch * zRotated;
			zRotated = sinPitch * point.Y + cosPitch * zRotated;

			return new Vector3(xRotated, yRotated, zRotated);
		}

		// Project a 3D point to 2D screen space with perspective
		public Vector2 ProjectVertex(Vector3 point)
		{
			// Translate point relative to camera position
			Vector3 translatedPoint = point - position;

			// Rotate point based on camera's orientation (yaw and pitch)
			Vector3 rotatedPoint = Rotate(translatedPoint);

			// Perform perspective projection
			float zClamped = Math.Max(rotatedPoint.Z, nearClippingPlane); // Ensure depth is positive

			// Convert to 2D screen coordinates
			float xScreen = ((fov * rotatedPoint.X) / zClamped) + space.X / 2;
			float yScreen = ((fov * rotatedPoint.Y) / zClamped) + space.Y / 2;

			return new Vector2(xScreen, yScreen);
		}

		public Vector3 MovePointInCameraDirection(Vector3 point, float distance)
		{
			// Calculate the camera's forward direction based on yaw and pitch
			float forwardX = (float)(Math.Cos(pitch) * Math.Sin(yaw));
			float forwardY = (float)Math.Sin(pitch);
			float forwardZ = (float)(Math.Cos(pitch) * Math.Cos(yaw));

			// Create the forward direction vector
			Vector3 forward = new Vector3(forwardX, forwardY, forwardZ);

			// Scale the forward vector by the distance you want to move (10 units)
			Vector3 moveVector = forward * distance;

			// Move the point in the direction of the camera
			Vector3 newPoint = point + moveVector;

			return newPoint;
		}
	}
}
