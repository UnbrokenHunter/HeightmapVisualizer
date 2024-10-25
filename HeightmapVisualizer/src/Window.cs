
using HeightmapVisualizer.Components;
using HeightmapVisualizer.Controls;
using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.Scene;
using System.Numerics;
using Plane = HeightmapVisualizer.src.Shapes.Plane;
using HeightmapVisualizer.src.Utilities;
using HeightmapVisualizer.src.Shapes;
using HeightmapVisualizer.src.UI;
using HeightmapVisualizer.src.Components;
using Button = HeightmapVisualizer.src.UI.Button;

namespace HeightmapVisualizer.src
{
    internal class Window : Form
    {
        public Scene.Scene Scene;

        public static Window Instance;

        public Vector2 ScreenSize => new Vector2(Width, Height);
        public Vector2 ScreenCenter => ScreenSize / 2;

        public Window()
        {
            if (Instance != null)
                throw new Exception("Two Windows have been created");
            else
                Instance = this;

            AllocConsole();

            // Set up form properties
            Text = "Projections";
            Width = 16 * 200;
            Height = 9 * 200;

            DoubleBuffered = true;

            Scene = CreateScene();

            Thread t1 = new Thread(Gameloop);
            t1.Start();
        }

        private Scene.Scene CreateScene()
        {
            Gameobject camera = new Gameobject();
            camera.AddComponent(new ControllerComponent());
            camera.AddComponent(new CameraComponent(Bounds));

            var values = new float[4, 4];
            //Random random = new Random();
            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    values[i, j] = i * j; // (float) random.NextDouble() * 255;
                }
            }

            Mesh[,] heightmap = Heightmap.CreateCorners(new Vector3(0, 0, 20), values, 1, mode: DrawingMode.Faces);
            Gameobject[] hm = MeshUtility.Convert2DArrayTo1DArray(heightmap);

            Gameobject cube = Cuboid.CreateCorners(new Vector3(-1, -1, -1), new Vector3(1, 1, 1)).SetColor(Color.Green);
            Gameobject cube2 = Cuboid.CreateCentered(new Vector3(-5, 2, 0), new Vector3(1, 2, 1), Color.HotPink, DrawingMode.Points);
            Gameobject floorPlane = Plane.CreateCentered(new Vector3(0, 5, 0), new Vector2(10, 10), Color.LightBlue, DrawingMode.Faces);
            Gameobject wallPlane = Plane.CreateCentered(new Vector3(0, -5, 0),
                new Vector3((float)Math.PI / 2f, 0f, 0f).CreateQuaternionFromYawPitchRoll(),
                new Vector2(10, 10));


            static void move(Gameobject g)
            {
                if (g.GetType() == typeof(Mesh))
                {
                    var names = ((Mesh)g).GetVertexsByName("Left");

                    foreach (Vertex t in names)
                    {
                        t.LocalPosition += Vector3.UnitZ / 1000 * -1;
                    }
                }
			}

            hm.ToList().ForEach(h => h.AddComponent(new ScriptableComponent(null, move)));


            Gameobject line = Line.CreateCorners(Vector3.Zero, new Vector3(0, 10, 10));

            var objects = new Gameobject[] { line, cube, cube2, wallPlane, camera };
            objects = objects.Concat(hm).ToArray();


			// CREATE MENU
			UIElement[] ui = new List<UIElement>
			{
				new Button(new Vector2(0, 0), new Vector2(400, 60), "Position", id: "pos"),
				new Button(new Vector2(0, 60), new Vector2(400, 60), "Euler Angles", id: "ang")
			}.ToArray();

			return new Scene.Scene(objects, ui);
        }

        private void Gameloop()
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

            Scene.Update(e.Graphics);
            MouseHandler.Debug(e.Graphics);
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
    }
}
