
using HeightmapVisualizer.Controls;
using HeightmapVisualizer.Scene;
using HeightmapVisualizer.Shapes;
using HeightmapVisualizer.UI;
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer
{
    internal class Window : Form
	{
		private Scene.Scene scene;

		public static Window Instance;

		private Menu menu = new();

		public Window()
		{
			if (Instance != null)
				throw new Exception("Two Windows have been created");
			else
				Instance = this;

			AllocConsole();

			// Set up form properties
			this.Text = "Projections";
			this.Width = 16 * 200;
			this.Height = 9 * 200;

			this.DoubleBuffered = true;

			this.scene = CreateScene();
			scene.Init();

			Thread t1 = new Thread(Update);
			t1.Start();
		}

		private Scene.Scene CreateScene()
		{
			Camera camera = new Camera(new Units.Transform(), this.Bounds);
			camera.Controller = new Controller();
			Gameobject cube = Cuboid.CreateCorners(new Vector3(-1, -1, -1), new Vector3(1, 1, 1));
            Gameobject floorPlane = Plane.CreateCentered(new Vector3(0, 5, 0), new Vector2(10, 10));
            Gameobject wallPlane = Plane.CreateCentered(new Vector3(0, -5, 0), 
				Quaternion.ToQuaternion(new Vector3((float) Math.PI / 2,0, 0)), 
				new Vector2(10, 10));

            return new Scene.Scene(camera, new Gameobject[] { cube, floorPlane, wallPlane, camera });
		}

		private void Update()
		{
			while (true)
			{
				Thread.Sleep(10);
				MouseHandler.Update();

				menu.Update(scene.camera);

				Invalidate(); // Calls the OnPaint Method
			}
		}

		// Override the OnPaint method to perform custom drawing
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			scene.Update(e.Graphics);
			MouseHandler.Debug(e.Graphics);

			UI.Button.Draw(e.Graphics);

		}

		[System.Runtime.InteropServices.DllImport("kernel32.dll")]
		private static extern bool AllocConsole();
	}
}
