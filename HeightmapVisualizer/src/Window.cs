
using System.Numerics;
using HeightmapVisualizer.src.Utilities;
using HeightmapVisualizer.src.UI;
using HeightmapVisualizer.src.Components;
using Plane = HeightmapVisualizer.src.Factories.Plane;
using Button = HeightmapVisualizer.src.UI.Button;
using HeightmapVisualizer.src.Primitives;
using HeightmapVisualizer.src.Scene;
using HeightmapVisualizer.src.Factories;
using HeightmapVisualizer.src.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using HeightmapVisualizer.src.Components.Collision;
using HeightmapVisualizer.src.Components.Physics;

namespace HeightmapVisualizer.src
{
    internal class Window : Form
    {
        public Scene.Scene Scene;

        public static Window? Instance;

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

            new Gameloop(Render, UpdateScene, 60d).Start();
        }

        private Scene.Scene CreateScene()
        {
            Gameobject camera = new Gameobject(new Vector3(0, -10, -30))
                .AddComponent(new ControllerComponent().SetSpeed(0.5f))
                .AddComponent(new PerspectiveCameraComponent(priority: 2));

			Gameobject camera2 = new Gameobject(new Vector3(-0.5f, -0.5f, 1.5f))
                .AddComponent(new PerspectiveCameraComponent(priority: 1));

            static void sine(Gameobject g)
            {
                if (g.Components.Find(c => c.GetType() == typeof(MeshComponent)) is MeshComponent m)
                {
                    var names = m.GetFacesByName("Top");

                    var points = names[0].Points;
                    for (int i = 0; i < points.Length; i++)
                    {
                        if (Gameloop.Instance != null)
                        {
                            var time = (float)Gameloop.Instance.Time;

                            var posX = g.Transform.Position.X;
                            var posZ = g.Transform.Position.Z;

                            var pointX = points[i].X;
                            var pointZ = points[i].Z;

                            var size1 = 4f;
                            var size2 = 4f;

                            var x = posX + pointX; // X axis
                            var z = posZ + pointZ;

                            var input = time + x + z;

                            var f1 = MathF.Sin((input) / size1);
                            var f2 = MathF.Sin((input) / size2);

                            var height = 0.5f;
                            var output = (f1) / height;

                            points[i] = new Vector3(pointX, output, pointZ);

                        }
                    }

                    names[0].SetPoints(m, points);
                }

            }

            var values = new float[1, 1];
            //Random random = new Random();
            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    values[i, j] = 0; // i * j; // (float) random.NextDouble() * 255;
                }
            }

            Gameobject[,] heightmap = Heightmap.CreateCorners(values, 1, new Vector3(-values.GetLength(0) / 2, 1, -values.GetLength(1) / 2));
            Gameobject[] hm = Heightmap.Convert2DArrayTo1DArray(heightmap);
            hm.ToList().ForEach(g =>
            {
                g.TryGetComponents(out MeshComponent[] m);
                (m[0]).SetWireframe(true).SetColor(Color.Blue);
                g.AddComponent(new ScriptableComponent(update: sine));
                g.AddComponent(new MeshAABBCollisionComponent().SetDebug(true));
            });

            Gameobject cube = new Gameobject(new Vector3(-1, -1, 2))
                .AddComponent(new MeshComponent(Cuboid.CreateCorners(new Vector3(1, 1, 1))).SetColor(Color.Green).SetWireframe(true))
				.AddComponent(new BoxAABBCollisionComponent().SetDebug(true))
			    .AddComponent(new PhysicsComponent().SetVelocity(new Vector3(0.01f, 0, 0)));
			Gameobject cube2 = new Gameobject(new Vector3(1, -1, 2))
                .AddComponent(new MeshComponent(Cuboid.CreateCentered(new Vector3(1, 2, 1))).SetColor(Color.Red).SetWireframe(true))
                .AddComponent(new BoxAABBCollisionComponent().SetDebug(true))
                .AddComponent(new PhysicsComponent());
			Gameobject floorPlane = new Gameobject(new Vector3(0, 5, 0))
                .AddComponent(new MeshComponent(Plane.CreateCentered(new Vector2(10, 10))).SetWireframe(true));
            Gameobject wallPlane = new Gameobject(new Vector3(0, -5, 0), new Vector3((float)Math.PI / 2f, 0f, 0f).CreateQuaternionFromYawPitchRoll()) 
                .AddComponent(new MeshComponent(Plane.CreateCentered(new Vector2(10, 10))).SetWireframe(true)
                );

            var points = new Vector3[3] { new Vector3(1, 0, 1), new Vector3(0, 1, 0), new Vector3(-1, 0, 0) } ;

            Gameobject tri = new Gameobject()
                .AddComponent(
                    new MeshComponent(
                        new MeshComponent.Face[1] { new MeshComponent.Face(points) }, isWireframe: true
                    )
                );


            static void move(Gameobject g)
            {
                // g.Transform.Position += new Vector3(0.01f, 0, 0);
                //if (g.Components.Find(c => c.GetType() == typeof(MeshComponent)) is MeshComponent m)
                //{
                //    var names = m.GetFacesByName("Left");

                //    var points = names[0].Points;
                //    for (int i = 0; i < points.Length; i++)
                //    {
                //        points[i] += new Vector3(-0.001f, 0, 0);
                //    }

                //    names[0].SetPoints(points);
                //}
            }

            cube.AddComponent(new ScriptableComponent(null, move));

                Gameobject line = new Gameobject()
                .AddComponent(new MeshComponent(Line.CreateCorners(Vector3.Zero, new Vector3(0, 10, 10))));

            var objects = new Gameobject[] { cube, cube2, camera, camera2 };
            objects = objects.Concat(hm).ToArray();

            static void cam(Button button)
            {
				foreach (Gameobject game in Instance.Scene.Gameobjects)
                {
                    if (game.TryGetComponents(out PerspectiveCameraComponent[] res) != 0) 
                    {
                        foreach(PerspectiveCameraComponent perspectiveCamera in res)
                        {
                            Console.WriteLine(perspectiveCamera + " prio " + perspectiveCamera.Priority);
                            perspectiveCamera.SetPriority((perspectiveCamera.Priority + 1) % 2);
                        }
                    }
                }

			}

            // CREATE MENU
            UIElement[] ui = new List<UIElement>
			{
                new Button(new Vector2(0, 0), new Vector2(800, 60), "Position", id: "pos", update: (UIElement g) => ((Button)g).SetText(Instance.Scene.Camera.Gameobject.Transform.Position.ToString())),
                new Button(new Vector2(0, 60), new Vector2(800, 60), "Euler Angles", id: "ang", update: (UIElement g) => ((Button)g).SetText(Instance.Scene.Camera.Gameobject.Transform.Rotation.ToString())),
				new Button(new Vector2(0, 120), new Vector2(400, 60), "FPS", id: "fps", update: (UIElement g) => ((Button)g).SetText("FPS: " + Gameloop.Instance.FPS)),
				new Button(new Vector2(0, 180), new Vector2(400, 60), "Camera", id: "cam", onClick: cam),
				new Button(new Vector2(0, 240), new Vector2(400, 60), "Gameobjs", id: "objs", update: (UIElement g) => ((Button)g).SetText("Object Count: " + Instance.Scene.Gameobjects.Length)),
			}.ToArray();

			return new src.Scene.Scene(objects, ui);
        }

        void UpdateScene()
        {
            Scene.Update();
            MouseHandler.Update();
        }


        void Render()
        {
            Invalidate(); // Calls the OnPaint Method
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
