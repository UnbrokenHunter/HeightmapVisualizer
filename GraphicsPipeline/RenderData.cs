using System.Drawing;
using System.Numerics;

namespace GraphicsPipeline
{
    public readonly struct RenderData
    {
        public Vector2 P1 { get; }
        public Vector2 P2 { get; }
        public Vector2 P3 { get; }
        public Color Color { get; }
        public bool IsWireframe { get; }

        public RenderData(Vector2 p1, Vector2 p2, Vector2 p3, Color color, bool isWireframe)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
            Color = color;
            IsWireframe = isWireframe;
        }
    }
}
