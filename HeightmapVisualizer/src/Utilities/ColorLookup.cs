
using System.Drawing.Drawing2D;

namespace HeightmapVisualizer.src.Utilities
{
    public static class ColorLookup
    {
        private static Dictionary<Color, Brush> brushes = new Dictionary<Color, Brush>();
        public static Brush FindOrGetBrush(Color color)
        {
            brushes.TryGetValue(color, out Brush? value);


            if (value == null)
            {
                value = new LinearGradientBrush(Point.Empty, new Point(10, 10), color, Color.BlueViolet);
                brushes.Add(color, value);

            }

            return value;
        }

        private static Dictionary<Color, Pen> pens = new Dictionary<Color, Pen>();
        public static Pen FindOrGetPen(Color color)
        {
            pens.TryGetValue(color, out Pen? value);

            if (value == null)
            {
                value = new Pen(color);
                pens.Add(color, value);
            }

            return value;
        }
    }
}
