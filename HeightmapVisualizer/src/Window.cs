
using System.Numerics;
using System.Diagnostics;
using HeightmapVisualizer.Components;
using HeightmapVisualizer.Controls;
using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.Scene;
using HeightmapVisualizer.src.Utilities;
using HeightmapVisualizer.src.Shapes;
using HeightmapVisualizer.src.UI;
using HeightmapVisualizer.src.Components;
using Plane = HeightmapVisualizer.src.Shapes.Plane;
using Button = HeightmapVisualizer.src.UI.Button;
using Timer = System.Windows.Forms.Timer;

namespace HeightmapVisualizer.src
{
    internal class Window : Form
    {
        public Scene.Scene Scene;

        public static Window Instance;

        private Timer timer;
        private Stopwatch stopwatch;
        private double previousTime;
        private int frameCount = 0;
        private double fpsTimer = 0.0;
        private double displayedFPS = 0.0;
        public const double FPS = 60;
        public double deltaTime = 0;

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

            timer = new Timer();
            timer.Interval = 16; // ~60 FPS
            timer.Tick += StartGameloop;

            stopwatch = new Stopwatch();
            stopwatch.Start();
            previousTime = stopwatch.Elapsed.TotalSeconds;

            timer.Start();


            Scene = CreateScene();
        }

        private Scene.Scene CreateScene()
        {
            Gameobject camera = new Gameobject();
            camera.AddComponent(new ControllerComponent());
            camera.AddComponent(new CameraComponent(Bounds));

            var values = new float[40, 4];
            //Random random = new Random();
            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    values[i, j] = i * j; // (float) random.NextDouble() * 255;
                }
            }

            Mesh[,] heightmap = Heightmap.CreateCorners(new Vector3(0, 0, 20), values, 1);
            Gameobject[] hm = MeshUtility.Convert2DArrayTo1DArray(heightmap);

            Gameobject cube = Cuboid.CreateCorners(new Vector3(-1, -1, -1), new Vector3(1, 1, 1)).SetColor(Color.Green);
            Gameobject cube2 = Cuboid.CreateCentered(new Vector3(-5, 2, 0), new Vector3(1, 2, 1), Color.HotPink, true);
            Gameobject floorPlane = Plane.CreateCentered(new Vector3(0, 5, 0), new Vector2(10, 10), Color.LightBlue, true);
            Gameobject wallPlane = Plane.CreateCentered(new Vector3(0, -5, 0),
                new Vector3((float)Math.PI / 2f, 0f, 0f).CreateQuaternionFromYawPitchRoll(),
                new Vector2(10, 10), drawWireframe: true);


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


			static void updatePos(UIElement g)
            {
                ((Button)g).SetText(Instance.Scene.Camera.Gameobject.Transform.Position.ToString());
            }

			static void updateAng(UIElement g)
			{
				((Button)g).SetText(Instance.Scene.Camera.Gameobject.Transform.Rotation.ToString());
			}


			// CREATE MENU
			UIElement[] ui = new List<UIElement>
			{
				new Button(new Vector2(0, 0), new Vector2(400, 60), "Position", id: "pos", update: updatePos),
				new Button(new Vector2(0, 60), new Vector2(400, 60), "Euler Angles", id: "ang", update: updateAng)
			}.ToArray();

			return new Scene.Scene(objects, ui);
        }

        private void StartGameloop(object sender, EventArgs e)
        {
            // Calculate delta time
            double currentTime = stopwatch.Elapsed.TotalSeconds;
            double deltaTime = currentTime - previousTime;
            previousTime = currentTime;

            // Update game logic
            UpdateGameLogic();

            // Calculate FPS over a second
            fpsTimer += deltaTime;
            frameCount++;
            if (fpsTimer >= 1.0)
            {
                displayedFPS = frameCount / fpsTimer;
                Console.WriteLine($"Actual Displayed FPS: {displayedFPS:F2}");

                // Reset counters for the next second
                fpsTimer -= 1.0;
                frameCount = 0;
            }

            // Render the current frame by invalidating the form to trigger OnPaint
            Render();

            void UpdateGameLogic()
            {
                Scene.Update();
                MouseHandler.Update();
            }


            void Render()
            {
                Invalidate(); // Calls the OnPaint Method
            }
        }

        // Override the OnPaint method to perform custom drawing
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Scene.Render(e.Graphics);
            MouseHandler.Debug(e.Graphics);
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
    }
}
