using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace HeightmapVisualizer
{
    public static class Program
    {
        public static void Main()
        {
            var gameWindowSettings = GameWindowSettings.Default;

            var nativeWindowSettings = new NativeWindowSettings
            {
                ClientSize = new Vector2i(16 * 200, 9 * 200),  // New property to set the window size
                Title = "OpenTK Game Window",  // Window title
                APIVersion = new Version(3, 3),  // Request OpenGL 3.3 context
                Profile = ContextProfile.Core,  // Core profile (modern OpenGL)
                Flags = ContextFlags.ForwardCompatible  // Forward-compatible OpenGL context
            };

            using (var window = new Window(gameWindowSettings, nativeWindowSettings))
            {
                window.Run();  // Start the game loop
            }
        }
    }
}