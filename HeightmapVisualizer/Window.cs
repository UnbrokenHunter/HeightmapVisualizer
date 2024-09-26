
using System.Numerics;
using System.Windows.Forms.VisualStyles;
using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.Scene;
using HeightmapVisualizer.Shapes;
using HeightmapVisualizer.UI;

namespace HeightmapVisualizer
{
    internal class Window : Form
	{
		/*

		private Heightmap hm;
		private Cuboid[,] hm3d;

		*/

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
			AllocConsole();

			Camera camera = new Camera(new Units.Transform(), this.Bounds);
			camera.Controller = new Controller();
			Gameobject cube = new Cuboid(new Units.Transform(), new Units.Vector3(-1, -1, -1), new Units.Vector3(1, 1, 1));

			new Scene.Scene(camera, new Gameobject[] { cube });

			// PUT THIS IN LOOP
			Window.GetInstance().Invalidate();
			Thread.Sleep(1);

		}



		/*
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

			new Player.Camera(position, screen, aspect, fov, nearClippingPlane, new Quaternion(0, 0, 0, 1));

			hm = new CreateHeightmap(30, 30).Map;
			hm3d = hm.Map3D();

			AllocConsole();

			// create a new thread
			Menu menu = new Menu();
			new Player.Controller();
			Thread t = new Thread(Controller.Update);
			Thread t2 = new Thread(menu.Update);

			// start the thread
			t.Start();
			t2.Start();
		}

		*/

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

		[System.Runtime.InteropServices.DllImport("kernel32.dll")]
		private static extern bool AllocConsole();
	}
}
