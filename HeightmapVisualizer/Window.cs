using HeightmapVisualizer.Controls;
using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.Rendering;
using HeightmapVisualizer.Scene;
using HeightmapVisualizer.Shapes;
using HeightmapVisualizer.Units;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace HeightmapVisualizer
{
    internal class Window : GameWindow
    {
        public Scene.Scene Scene;

        public static Window Instance;

        public Vector2 ScreenSize => new Vector2(this.ClientSize.X, this.ClientSize.Y);
        public Vector2 ScreenCenter => ScreenSize / 2;

        private Menu menu = new();

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            if (Instance != null)
                throw new Exception("Two Windows have been created");
            else
                Instance = this;

            AllocConsole();

            this.Scene = CreateScene();
            Scene.Init();
        }

        #region Window Things

        protected override void OnLoad()
        {
            base.OnLoad();

            // Set up OpenGL state
            GL.ClearColor(0.1f, 0.2f, 0.3f, 1.0f);  // Set clear color
            GL.Enable(EnableCap.DepthTest);          // Enable depth testing for 3D rendering
        
            Scene.InitOpenGL();
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            // Free resources like buffers, shaders, etc.
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            // Update the OpenGL viewport to match the new window size
            GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
        }

        #endregion

        private Scene.Scene CreateScene()
        {
            Camera camera = new Camera(new Units.Transform());
            camera.Controller = new Controller();

            var values = new float[10, 10];
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
                Quaternion.CreateFromPitchYawRoll(new Vector3((float)Math.PI / 2, 0, 0)),
                new Vector2(10, 10));

            Gameobject line = Line.CreateCorners(Vector3.Zero, new Vector3(0, 10, 10));

            var objects = new Gameobject[] { line, cube, cube2, floorPlane, wallPlane, camera };
            objects = objects.Concat(hm).ToArray();

            return new Scene.Scene(camera, objects);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            Scene.Update();
            MouseHandler.Update();

            menu.Update(Scene.Camera);
        }

        // Override the OnRenderFrame method to perform custom drawing
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            // Clear the color and depth buffer before drawing
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Title = Scene.Camera.Transform.Position.ToString() + " \t " + Scene.Camera.Transform.Rotation.ToString();
            Scene.Draw(args);
            MouseHandler.Debug(args);

            UI.Button.Draw(args);

            // Check for any OpenGL errors
            CheckGLError();

            // Swap buffers to display the rendered content
            SwapBuffers();
        }

        // Helper method to check OpenGL errors
        private void CheckGLError()
        {
            ErrorCode error = GL.GetError();
            if (error != ErrorCode.NoError)
            {
                Console.WriteLine($"OpenGL Error: {error}");
            }
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
    }
}
