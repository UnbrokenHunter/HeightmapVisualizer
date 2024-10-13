
using HeightmapVisualizer.Rendering;
using HeightmapVisualizer.Units;
using HeightmapVisualizer.Utilities;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;

namespace HeightmapVisualizer.Scene
{
    public class Scene
    {
        public Camera Camera { get; set; }
        public Gameobject[] Gameobjects { get; set; }

        public Scene(Camera camera, Gameobject[] gameobjects)
        {
            this.Camera = camera;
            this.Gameobjects = gameobjects;
        }

        public void Init()
        {
            foreach (var gameobject in Gameobjects)
            {
                gameobject.Init();
            }
        }

        public void Update()
        {
            UpdateGameobjects();

        }

        public void Draw(FrameEventArgs args)
        {
            RenderCamera(args);
        }

        private void UpdateGameobjects()
        {
            foreach (var gameobject in Gameobjects)
            {
                gameobject.Update();
            }
        }

        private void RenderCamera(FrameEventArgs args)
        {
            List<Renderable> renderOrder = new List<Renderable>();

            foreach (var gameobject in Gameobjects)
            {
                var mesh = gameobject.GetRenderable();
                if (mesh != null)
                {
                    var renderables = mesh.PointsToRender();

                    foreach (var renderable in renderables)
                    {
                        // Cull Objects off screen
                        if (renderable.GetOrCalculateScreenPosition(Camera).All(e => e.Item2 == false))
                            continue;


                        renderOrder.Add(renderable);
                    }
                }
            }

            // Draw the furthest first, and draw nearer ones on tops
            Renderable[] toRender = renderOrder.OrderBy(e => -e.GetOrCalculateDistance(Camera).Min(e => e)).ToArray();

            UpdateAndDrawTriangles(toRender);
        }

        private int vbo = -1;
        private int vao = -1;

        public void InitOpenGL()
        {
            // Create VAO and VBO
            vao = GL.GenVertexArray();
            vbo = GL.GenBuffer();

            // Bind VAO
            GL.BindVertexArray(vao);

            // Bind VBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            // Vertex attribute pointer (for 2D positions)
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, Vector2.SizeInBytes, 0);
            GL.EnableVertexAttribArray(0);

            // Unbind VBO and VAO
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        public void UpdateAndDrawTriangles(Renderable[] toRender)
        {
            // Calculate the number of vertices required for this frame
            int numVertices = toRender.Length * 3;  // Each Renderable is a triangle (3 vertices)
            Vector2[] vertices = new Vector2[numVertices];

            // Fill the vertex array with current positions
            for (int i = 0; i < toRender.Length; i++)
            {
                Renderable renderable = toRender[i];
                var screenPositions = renderable.GetOrCalculateScreenPosition(Camera);

                vertices[i * 3] = new Vector2(screenPositions[0].Item1.x, screenPositions[0].Item1.y);
                vertices[i * 3 + 1] = new Vector2(screenPositions[1].Item1.x, screenPositions[1].Item1.y);
                vertices[i * 3 + 2] = new Vector2(screenPositions[2].Item1.x, screenPositions[2].Item1.y);
            }

            // Bind VAO
            GL.BindVertexArray(vao);

            // Update VBO data each frame
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, numVertices * Vector2.SizeInBytes, vertices, BufferUsageHint.DynamicDraw);

            // Use the shader program
            GL.UseProgram(ColorShader.GetColorShader());

            // Render each triangle
            for (int i = 0; i < toRender.Length; i++)
            {
                Renderable renderable = toRender[i];

                // Set the color using a uniform
                int colorLocation = GL.GetUniformLocation(ColorShader.GetColorShader(), "uColor");
                var color = renderable.Tri.GetColor();
                GL.Uniform4(colorLocation, color.R / 255f, color.G / 255f, color.B / 255f, 1.0f);

                if (renderable.DrawingMode == DrawingMode.Lines)
                {
                    GL.DrawArrays(PrimitiveType.LineLoop, i * 3, 3);
                }
                else if (renderable.DrawingMode == DrawingMode.Faces)
                {
                    GL.DrawArrays(PrimitiveType.Triangles, i * 3, 3);
                }
            }

            // Unbind VAO
            GL.BindVertexArray(0);
        }

    }
}
