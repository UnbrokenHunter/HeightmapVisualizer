
using OpenTK.Graphics.OpenGL;
using System.Drawing.Drawing2D;

namespace HeightmapVisualizer.Utilities
{
	public static class ColorShader
	{

        public static int GetColorShader()
        {
            if (shaderProgram == int.MinValue)
                CreateShaders();
            return shaderProgram;
        }

        private static int shaderProgram = int.MinValue;

        private static void CreateShaders()
        {
            // Vertex Shader
            var vertexShaderSource = @"
    #version 330 core
    layout (location = 0) in vec2 aPosition;
    void main() { gl_Position = vec4(aPosition, 0.0, 1.0); }";
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.CompileShader(vertexShader);

            // Fragment Shader
            var fragmentShaderSource = @"
    #version 330 core
    out vec4 FragColor;
    uniform vec4 uColor;
    void main() { FragColor = uColor; }";
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);
            GL.CompileShader(fragmentShader);

            // Create Shader Program
            shaderProgram = GL.CreateProgram();
            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);
            GL.LinkProgram(shaderProgram);

            // Clean up shaders after linking
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }


    }
}
