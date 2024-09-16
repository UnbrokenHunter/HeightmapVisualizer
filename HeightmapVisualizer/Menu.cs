using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HeightmapVisualizer
{
	internal class Menu
	{
		public Menu() 
		{
			new Button("Text", new Vector2(0, 0), new Vector2(60, 60), (button) =>
			{
				Window.GetInstance().Invalidate();
			});
		}

		public static void Update()
		{
			while (true)
			{
				DragHandler.UpdateDrag();
				if (DragHandler.IsDragging)
				{
					Window.GetInstance().Invalidate();
					Console.WriteLine("Dragging" + DragHandler.GetCurrentPosition() + " " + DragHandler.GetLastPosition());
					Vector2 direction = new Vector2(DragHandler.GetCurrentPosition().X - DragHandler.GetLastPosition().X, DragHandler.GetCurrentPosition().Y - DragHandler.GetLastPosition().Y);


					// Sensitivity
					float sensitivity = 0.005f;

					// Apply sensitivity scaling to direction
					direction *= sensitivity;

					// Calculate pitch and yaw directly based on direction (no atan here)
					float yaw = direction.X;
					float pitch = direction.Y;

					// Apply pitch and yaw adjustments
					Camera.Instance.pitch += pitch;
					Camera.Instance.yaw += yaw;

				}
				Console.WriteLine($"Pitch: {Camera.Instance.pitch} Yaw: {Camera.Instance.yaw}");
				Thread.Sleep(1);
			}
		}

		public static class DragHandler
		{
			private static Point originalPosition;  // The starting mouse position when dragging begins
			private static Point lastPosition;   // The current mouse position during dragging
			private static Point currentPosition;   // The current mouse position during dragging
			private static bool isDragging = false; // Flag to check if dragging is active

			public static bool IsDragging => isDragging; // Property to check if dragging is active

			// Method to check if dragging has started, called whenever
			public static void UpdateDrag()
			{
				// Check if the left mouse button is pressed
				if (Control.MouseButtons == MouseButtons.Left)
				{
					// If dragging has not started, record the initial position
					if (!isDragging)
					{
						originalPosition = Cursor.Position; // Store the original position (screen coordinates)
						isDragging = true;
						Console.WriteLine($"Dragging started at: {originalPosition}");
					}

					// Update the last position to the old current position
					lastPosition = currentPosition;

					// Update the current mouse position as the drag continues
					currentPosition = Cursor.Position;
				}
				else if (isDragging) // If the left mouse button is released, stop dragging
				{
					isDragging = false;
					Console.WriteLine($"Dragging ended at: {Cursor.Position}");
				}
			}

			// Method to get the original mouse position when dragging started
			public static Point GetOriginalPosition()
			{
				return originalPosition;
			}

			// Method to get the current mouse position during dragging
			public static Point GetLastPosition()
			{
				return lastPosition;
			}

			// Method to get the current mouse position during dragging
			public static Point GetCurrentPosition()
			{
				return currentPosition;
			}
		}
	}
}
