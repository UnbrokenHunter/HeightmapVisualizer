using HeightmapVisualizer.Units;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeightmapVisualizer.Utilities
{
    public class GLUtilities
    {
        public static void DrawLineStrip(Vector2[] vertices, Color color)
        {
            // Convert Color to normalized floats
            float red = color.R / 255f;
            float green = color.G / 255f;
            float blue = color.B / 255f;
            float alpha = color.A / 255f;

            // Generate and bind the VAO and VBO
            int vao = GL.GenVertexArray();
            int vbo = GL.GenBuffer();

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * Vector2.SizeInBytes, vertices, BufferUsageHint.DynamicDraw);

            // Enable vertex attribute
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // Use the shader program
            GL.UseProgram(ColorShader.GetColorShader());  // Assuming shaderProgram is already compiled and linked

            // Set color uniform
            int colorLocation = GL.GetUniformLocation(ColorShader.GetColorShader(), "uColor");
            GL.Uniform4(colorLocation, red, green, blue, alpha);

            // Draw the line strip
            GL.DrawArrays(PrimitiveType.LineStrip, 0, vertices.Length);

            // Unbind and cleanup
            GL.DisableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.DeleteBuffer(vbo);
            GL.DeleteVertexArray(vao);
        }

        public static void DrawLine(Vector2[] vertices, Color color)
        {
            // Same logic as in DrawLineStrip but using PrimitiveType.Lines
            int vao = GL.GenVertexArray();
            int vbo = GL.GenBuffer();

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * Vector2.SizeInBytes, vertices, BufferUsageHint.DynamicDraw);

            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.UseProgram(ColorShader.GetColorShader());
            int colorLocation = GL.GetUniformLocation(ColorShader.GetColorShader(), "uColor");
            GL.Uniform4(colorLocation, color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);

            GL.DrawArrays(PrimitiveType.Lines, 0, vertices.Length);

            GL.DisableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.DeleteBuffer(vbo);
            GL.DeleteVertexArray(vao);
        }

        public static void DrawRect(Vector2 pos1, Vector2 pos2, Color color)
        {
            // Calculate the other two corners of the rectangle
            Vector2 topRight = new Vector2(pos2.x, pos1.y);  // Top right corner
            Vector2 bottomLeft = new Vector2(pos1.x, pos2.y);  // Bottom left corner

            // Define the rectangle vertices (starting from pos1 and going clockwise)
            Vector2[] vertices = new Vector2[]
            {
                pos1,        // Bottom-left (starting point)
                topRight,    // Bottom-right
                pos2,        // Top-right
                bottomLeft   // Top-left
            };

            // Generate and bind VAO and VBO
            int vao = GL.GenVertexArray();
            int vbo = GL.GenBuffer();

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * Vector2.SizeInBytes, vertices, BufferUsageHint.DynamicDraw);

            // Set up the vertex attribute pointer
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // Use the color shader and set the color uniform
            GL.UseProgram(ColorShader.GetColorShader());
            int colorLocation = GL.GetUniformLocation(ColorShader.GetColorShader(), "uColor");
            GL.Uniform4(colorLocation, color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);

            // Draw the rectangle as a line loop to create a connected outline
            GL.DrawArrays(PrimitiveType.LineLoop, 0, vertices.Length);

            // Unbind and clean up
            GL.DisableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.DeleteBuffer(vbo);
            GL.DeleteVertexArray(vao);
        }

        public static void DrawCircle(Vector2 center, float radius, Color color)
        {
            const int numSegments = 100;  // Number of segments for the circle

            // Prepare vertices for the circle
            List<Vector2> circleVertices = new List<Vector2>();
            circleVertices.Add(center);  // Center of the circle

            for (int i = 0; i <= numSegments; i++)
            {
                float angle = 2.0f * MathF.PI * i / numSegments;
                float x = center.x + MathF.Cos(angle) * radius;
                float y = center.y + MathF.Sin(angle) * radius;
                circleVertices.Add(new Vector2(x, y));
            }

            // Generate and bind VAO and VBO
            int vao = GL.GenVertexArray();
            int vbo = GL.GenBuffer();

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, circleVertices.Count * Vector2.SizeInBytes, circleVertices.ToArray(), BufferUsageHint.DynamicDraw);

            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.UseProgram(ColorShader.GetColorShader());
            int colorLocation = GL.GetUniformLocation(ColorShader.GetColorShader(), "uColor");
            GL.Uniform4(colorLocation, color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);

            GL.DrawArrays(PrimitiveType.TriangleFan, 0, circleVertices.Count);

            // Unbind and cleanup
            GL.DisableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.DeleteBuffer(vbo);
            GL.DeleteVertexArray(vao);
        }


    }
}
