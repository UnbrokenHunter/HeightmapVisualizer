
using HeightmapVisualizer.Controls;
using HeightmapVisualizer.Scene;
using HeightmapVisualizer.Shapes;
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer
{
    internal class Window : Form
    {
        public Scene.Scene Scene;

        public static Window Instance;

        public Vector2 ScreenSize => new Vector2(Width, Height);
        public Vector2 ScreenCenter => ScreenSize / 2;

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

            this.Scene = CreateScene();
            Scene.Init();

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
                Quaternion.ToQuaternion(new Vector3((float)Math.PI / 2, 0, 0)),
                new Vector2(10, 10));

            Gameobject line = Line.CreateCorners(Vector3.Zero, new Vector3(0, 10, 10));

            return new Scene.Scene(camera, new Gameobject[] { line, cube, floorPlane, wallPlane, camera });
        }

        private void Update()
        {
            while (true)
            {
                Thread.Sleep(10);
                MouseHandler.Update();

                menu.Update(Scene.Camera);

                Invalidate(); // Calls the OnPaint Method
            }
        }

        // Override the OnPaint method to perform custom drawing
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Scene.Update(e.Graphics);
            MouseHandler.Debug(e.Graphics);

            UI.Button.Draw(e.Graphics);

        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
    }
}
