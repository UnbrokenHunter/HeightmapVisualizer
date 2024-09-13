
using System.Numerics;

namespace HeightmapVisualizer
{
	internal class Window : Form
	{
		public Window()
		{
			// Set up form properties
			this.Text = "Projections";
			this.Width = 16 * 100;
			this.Height = 9 * 100;

			// Yaw (rotation around y-axis) and pitch (rotation around x-axis)
			Vector3 position = new Vector3(-50, -400, -50);
			Vector2 screen = new Vector2(Width, Height);
			float aspect = 16f / 9f;
			float fov = 90f;
			float nearClippingPlane = 0.001f;
			float yaw = 0f;
			float pitch = 10f;

			new Camera(position, screen, aspect, fov, nearClippingPlane, yaw, pitch);

			AllocConsole();

			// create a new thread
			Thread t = new Thread(Worker);

			// start the thread
			t.Start();
		}

		static void Worker()
		{
			while (true)
			{
				Console.WriteLine(Cursor.Position.ToString()); // Make Menu and change parameters, and add a redraw button for now
				Thread.Sleep(100);
			}
		}


		[System.Runtime.InteropServices.DllImport("kernel32.dll")]
		private static extern bool AllocConsole();

		// Override the OnPaint method to perform custom drawing
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			// Get the Graphics object to draw on the form
			Graphics g = e.Graphics;

			Heightmap hm = new CreateHeightmap(30, 30).Map;
			DrawHeightmap.Draw(e, g, hm);

			DrawDebug.Draw(e, g, hm);		
		}
	}
}
