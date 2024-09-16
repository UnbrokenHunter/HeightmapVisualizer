
using System.Numerics;
using HeightmapVisualizer.UI;

namespace HeightmapVisualizer
{
    internal class Window : Form
	{
		public static Window GetInstance()
		{
			if (Instance != null)
				return Instance;
			else
				return new Window();
		}
		private static Window Instance = GetInstance();

		private Window()
		{
			if (Instance == null)
				Instance = this;
			else
				throw new InvalidOperationException();

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

			new Player.Camera(position, screen, aspect, fov, nearClippingPlane, yaw, pitch);

			AllocConsole();

			// create a new thread
			new Menu();
			new Player.Controller();
			Thread t = new Thread(Player.Controller.Update);

			// start the thread
			t.Start();
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
			//DrawHeightmap.Draw(e, g, hm);

			DrawDebug.Draw(e, g, hm);

			UI.Button.Draw(g);
		}
	}
}
