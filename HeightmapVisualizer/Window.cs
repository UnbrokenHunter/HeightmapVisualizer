
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
		private Scene.Scene scene;

		public static Window GetInstance()
		{
			if (Instance == null)
				Instance = new Window();
			return Instance;
		}
		private static Window Instance;

		private Window()
		{
			AllocConsole();

			// Set up form properties
			this.Text = "Projections";
			this.Width = 16 * 100;
			this.Height = 9 * 100;

			this.DoubleBuffered = true;

			this.scene = CreateScene();
			scene.Init();
		}

		private Scene.Scene CreateScene()
		{
			Camera camera = new Camera(new Units.Transform(), this.Bounds);
			camera.Controller = new Controller();
			Gameobject cube = new Cuboid(new Units.Transform(), new Units.Vector3(-1, -1, -1), new Units.Vector3(1, 1, 1));

			return new Scene.Scene(camera, new Gameobject[] { cube });
		}



		/*
		private Window()
		{
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

			scene.Update(e.Graphics);

			Thread.Sleep(1);
			Window.GetInstance().Invalidate();
		}

		[System.Runtime.InteropServices.DllImport("kernel32.dll")]
		private static extern bool AllocConsole();
	}
}
