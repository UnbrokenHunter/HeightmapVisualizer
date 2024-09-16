using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeightmapVisualizer.Controls
{
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
