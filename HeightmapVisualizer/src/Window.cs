﻿
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
            Gameobject camera = new Gameobject()
                .AddComponent(new ControllerComponent())
                .AddComponent(new CameraComponent(Bounds));

            var values = new float[40, 4];
            //Random random = new Random();
            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    values[i, j] = i * j; // (float) random.NextDouble() * 255;
                }
            }

            //Gameobject[,] heightmap = Heightmap.CreateCorners(new Vector3(0, 0, 20), values, 1);
            //Gameobject[] hm = MeshUtility.Convert2DArrayTo1DArray(heightmap);

            Gameobject cube = new Gameobject(new Vector3(-1, -1, -1))
                .AddComponent(Cuboid.CreateCorners(new Vector3(1, 1, 1)).SetColor(Color.Green));
            Gameobject cube2 = new Gameobject(new Vector3(-5, 2, 0))
                .AddComponent(Cuboid.CreateCentered(new Vector3(1, 2, 1)));
            Gameobject floorPlane = new Gameobject(new Vector3(0, 5, 0))
                .AddComponent(Plane.CreateCentered(new Vector2(10, 10)));
            Gameobject wallPlane = new Gameobject(new Vector3(0, -5, 0), new Vector3((float)Math.PI / 2f, 0f, 0f).CreateQuaternionFromYawPitchRoll()) 
                .AddComponent(Plane.CreateCentered(new Vector2(10, 10))
                );

            var points = new Vector3[3] { new Vector3(1, 0, 1), new Vector3(0, 1, 0), new Vector3(-1, 0, 0) } ;

            Gameobject tri = new Gameobject()
                .AddComponent(
                    new MeshComponent(
                        new MeshComponent.Face[1] { new MeshComponent.Face(points) }, isWireframe: true
                    )
                );


   //         static void move(Gameobject g)
   //         {
   //             if (g.GetType() == typeof(Mesh))
   //             {
   //                 var names = ((Mesh)g).GetVertexsByName("Left");

   //                 foreach (Vertex t in names)
   //                 {
   //                     t.LocalPosition += Vector3.UnitZ / 1000 * -1;
   //                 }
   //             }
			//}

   //         hm.ToList().ForEach(h => h.AddComponent(new ScriptableComponent(null, move)));


            Gameobject line = new Gameobject()
                .AddComponent(Line.CreateCorners(Vector3.Zero, new Vector3(0, 10, 10)));

            var objects = new Gameobject[] { cube, camera };
            //objects = objects.Concat(hm).ToArray();


			static void updatePos(UIElement g)
            {
                ((Button)g).SetText(Instance.Scene.Camera.Gameobject.Transform.Position.ToString());
            }

			static void updateAng(UIElement g)
			{
				((Button)g).SetText(Instance.Scene.Camera.Gameobject.Transform.Rotation.ToString());
			}

            static void updateFPS(UIElement g)
            {
                ((Button)g).SetText("FPS: " + Gameloop.Instance.FPS);
            }

            // CREATE MENU
            UIElement[] ui = new List<UIElement>
			{
                new Button(new Vector2(0, 0), new Vector2(400, 60), "Position", id: "pos", update: updatePos),
                new Button(new Vector2(0, 60), new Vector2(400, 60), "Euler Angles", id: "ang", update: updateAng),
                new Button(new Vector2(0, 120), new Vector2(400, 60), "FPS", id: "fps", update: updateFPS),
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
