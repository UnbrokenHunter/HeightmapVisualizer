using System.Drawing;
using System.Numerics;

namespace GraphicsPipeline
{
    public class Renderer
    {
        public static void RenderTriangle(Bitmap bitmap, ((Vector2, bool), (Vector2, bool), (Vector2, bool), Color, bool)[] mesh)
        {
            foreach (var part in mesh)
            {
                if (part.Item1.Item2 || part.Item2.Item2 || part.Item3.Item2)
                {
                    DrawTriangle();
                }

                void DrawTriangle()
                {
                    Bresenham(bitmap, (int)part.Item1.Item1.X, (int)part.Item1.Item1.Y, (int)part.Item2.Item1.X, (int)part.Item2.Item1.Y, part.Item4);
                    Bresenham(bitmap, (int)part.Item2.Item1.X, (int)part.Item2.Item1.Y, (int)part.Item3.Item1.X, (int)part.Item3.Item1.Y, part.Item4);
                    Bresenham(bitmap, (int)part.Item3.Item1.X, (int)part.Item3.Item1.Y, (int)part.Item1.Item1.X, (int)part.Item1.Item1.Y, part.Item4);
                }
            }
        }

        private static void Bresenham(Bitmap bitmap, int x1, int y1, int x2, int y2, Color color)
        {
            // Point 1 is outside of bitmap
            if (!(x1 >= 0 && x1 < bitmap.Width
                && y1 >= 0 && y1 < bitmap.Height))
            {
                // Swaps starting point
                // Will draw line starting with point on screen, until no longer on screen
                var tempX = x1;
                var tempY = y1;
                x1 = x2;
                y1 = y2;
                x2 = tempX;
                y2 = tempY;
            }

            int w = x2 - x1;
            int h = y2 - y1;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                // Only draw it to the screen if it is on the screen
                if (bitmap.Width > x1 && bitmap.Height > y1 && 0 <= x1 && 0 <= y1)
                    bitmap.SetPixel(x1, y1, color);
                else
                    break;

                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x1 += dx1;
                    y1 += dy1;
                }
                else
                {
                    x1 += dx2;
                    y1 += dy2;
                }
            }
        }
    }
}
