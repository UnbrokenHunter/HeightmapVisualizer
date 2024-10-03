
using System.Numerics;
using System.Windows.Forms.VisualStyles;
using HeightmapVisualizer.Controls;
using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.Scene;
using HeightmapVisualizer.Shapes;
using HeightmapVisualizer.UI;

namespace HeightmapVisualizer
{
    internal class Window : Form
	{
		private Scene.Scene scene;

		public static Window Instance;



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
			Gameobject cube = Cuboid.CreateCorners(new Units.Vector3(-1, -1, -1), new Units.Vector3(1, 1, 1));

			return new Scene.Scene(camera, new Gameobject[] { cube, camera });
		}

		private void Update()
		{
			while (true)
			{
				Thread.Sleep(10);
				MouseHandler.Update();

				Invalidate(); // Calls the OnPaint Method
			}
		}

		// Override the OnPaint method to perform custom drawing
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			scene.Update(e.Graphics);
			MouseHandler.Debug(e.Graphics);

			Console.WriteLine(scene.camera.Transform);
		}

		[System.Runtime.InteropServices.DllImport("kernel32.dll")]
		private static extern bool AllocConsole();
	}
}
