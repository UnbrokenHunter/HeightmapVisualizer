
using System.Numerics;
using HeightmapVisualizer.Shapes;
using HeightmapVisualizer.UI;

namespace HeightmapVisualizer
{
    internal class Window : Form
	{

		private Heightmap hm;
		private Cuboid[,] hm3d;

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

			this.DoubleBuffered = true;

			// Yaw (rotation around y-axis) and pitch (rotation around x-axis)
			Vector3 position = new Vector3(-50, -400, -50);
			Vector2 screen = new Vector2(Width, Height);
			float aspect = 16f / 9f;
			float fov = 90f;
			float nearClippingPlane = 0.1f;
			float yaw = 0f;
			float pitch = 10f;

			new Player.Camera(position, screen, aspect, fov, nearClippingPlane, yaw, pitch);

			hm = new CreateHeightmap(30, 30).Map;
			hm3d = hm.Map3D();

			AllocConsole();

			// create a new thread
			Menu menu = new Menu();
			new Player.Controller();
			Thread t = new Thread(Player.Controller.Update);
			Thread t2 = new Thread(menu.Update);

			// start the thread
			t.Start();
			t2.Start();
		}

		[System.Runtime.InteropServices.DllImport("kernel32.dll")]
		private static extern bool AllocConsole();

		// Override the OnPaint method to perform custom drawing
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			// Get the Graphics object to draw on the form
			Graphics g = e.Graphics;

			DrawHeightmap.Draw(g, hm3d);

			DrawDebug.Draw(e, g, hm);

			UI.Button.Draw(g);
		}
	}
}
