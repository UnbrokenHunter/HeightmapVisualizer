
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer.Rendering
{
    public sealed class Renderable
    {
        public DrawingMode DrawingMode { get; private set; }
        public Vector2[] Position { get; private set; }
        public Color Color { get; private set; }

        public Renderable(DrawingMode drawingMode, Vector2[] position, Color color)
        {
            DrawingMode = drawingMode;
            Position = position;
            Color = color;
        }
    }
}
