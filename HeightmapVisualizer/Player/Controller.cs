using HeightmapVisualizer.Controls;
using System.Numerics;

namespace HeightmapVisualizer.Player
{
	internal class Controller
	{

		private static Vector3 movement = new Vector3();

		public Controller() 
		{
			// Enable key preview so the form receives key events
			Window.GetInstance().KeyPreview = true;

			// Subscribe to the KeyDown and KeyUp events
			Window.GetInstance().KeyDown += OnKeyDown;
			Window.GetInstance().KeyUp += OnKeyUp;
		}

		public static void Update()
		{
			while (true)
			{
				Pan();
				Move();

				Window.GetInstance().Invalidate();
				Thread.Sleep(1);
			}
		}

		private static void Move()
		{
			Camera.Instance.position += movement * 2;
		}

		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.W:
					movement.Z = 1;
					break;
				case Keys.A:
					movement.X = 1;
					break;
				case Keys.S:
					movement.Z = -1;
					break;
				case Keys.D:
					movement.X = -1;
					break;
				case Keys.Q:
					movement.Y = -1;
					break;
				case Keys.E:
					movement.Y = 1;
					break;
				case Keys.Escape:
					Console.WriteLine("Escape key pressed! Exiting...");
					Application.Exit();
					break;
			}
		}

		// Handle key up events (optional)
		private void OnKeyUp(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.W:
					movement.Z = 0;
					break;
				case Keys.A:
					movement.X = 0;
					break;
				case Keys.S:
					movement.Z = 0;
					break;
				case Keys.D:
					movement.X = 0;
					break;
				case Keys.Q:
					movement.Y = 0;
					break;
				case Keys.E:
					movement.Y = 0;
					break;
			}
		}


		private static void Pan()
		{
			DragHandler.UpdateDrag();
			if (DragHandler.IsDragging)
			{
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

		}
	}
}
