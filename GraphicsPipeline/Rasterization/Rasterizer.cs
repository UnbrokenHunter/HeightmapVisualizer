
using System.Drawing.Imaging;
using System.Drawing;
using System.Numerics;

namespace GraphicsPipeline.Rasterization
{
    internal class Rasterizer
    {
        //[MethodTimer.Time]
        internal static void RenderTriangle(Bitmap bitmap, RenderData[] mesh)
        {
            BitmapData bmpData = BitmapManipulation.LockBitmap(bitmap);

            Parallel.For(0, mesh.Length, i =>
            {
                var part = mesh[i];

                DrawTriangle(bmpData, (int)part.P1.X, (int)part.P1.Y, (int)part.P2.X, (int)part.P2.Y, (int)part.P3.X, (int)part.P3.Y, part.Color);

                    FillTriangle(bmpData, part);
            });

            BitmapManipulation.UnlockBitmap(bitmap, bmpData);
        }

        //[MethodTimer.Time]
        private static void FillTriangle(BitmapData bmpData, RenderData part)
        {
            if (!(IsPointOnScreen(bmpData, (int)part.P1.X, (int)part.P1.Y) ||
                IsPointOnScreen(bmpData, (int)part.P2.X, (int)part.P2.Y) ||
                IsPointOnScreen(bmpData, (int)part.P3.X, (int)part.P3.Y)))
                return;

            // Clamping beforehand saves ~50ms per cycle
            // Calculate bounding box with edge equations
            int startX = Math.Max(0, (int)Math.Floor(Math.Min(part.P1.X, Math.Min(part.P2.X, part.P3.X))));
            int startY = Math.Max(0, (int)Math.Floor(Math.Min(part.P1.Y, Math.Min(part.P2.Y, part.P3.Y))));
            int endX = Math.Min(bmpData.Width - 1, (int)Math.Ceiling(Math.Max(part.P1.X, Math.Max(part.P2.X, part.P3.X))));
            int endY = Math.Min(bmpData.Height - 1, (int)Math.Ceiling(Math.Max(part.P1.Y, Math.Max(part.P2.Y, part.P3.Y))));

            // Doing in parralel saves ~200ms per cycle
            Parallel.For(startX, endX, i =>
            {
                for (int j = startY; j < endY; j++)
                {
                    if (IsPointInTriangle(i, j, part.P1, part.P2, part.P3))
                    {
                        BitmapManipulation.FastSetPixel(bmpData, i, j, part.Color);
                    }
                }
            });
        }

        private static void DrawTriangle(BitmapData bmpData, int x1, int y1, int x2, int y2, int x3, int y3, Color color)
        {
            Bresenham(bmpData, x1, y1, x2, y2, color);
            Bresenham(bmpData, x2, y2, x3, y3, color);
            Bresenham(bmpData, x3, y3, x1, y1, color);
        }

        private static void Bresenham(BitmapData bmpData, int x1, int y1, int x2, int y2, Color color)
        {
            // Point 1 is outside of bitmap
            if (!IsPointOnScreen(bmpData, x1, y1))
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
            // If neither are on screen then stop
            else if (!IsPointOnScreen(bmpData, x2, y2))
                return;

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
                if (IsPointOnScreen(bmpData, x1, y1))
                    BitmapManipulation.FastSetPixel(bmpData, x1, y1, color);
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

        private static bool IsPointOnScreen(BitmapData bmpData, int x1, int y1)
        {
            return x1 >= 0 && x1 < bmpData.Width && y1 >= 0 && y1 < bmpData.Height;
        }

        private static bool IsPointInTriangle(int x, int y, Vector2 a, Vector2 b, Vector2 c)
        {
            // Calculate the cross-products for each edge
            float cross1 = (b.X - a.X) * (y - a.Y) - (b.Y - a.Y) * (x - a.X);
            float cross2 = (c.X - b.X) * (y - b.Y) - (c.Y - b.Y) * (x - b.X);
            float cross3 = (a.X - c.X) * (y - c.Y) - (a.Y - c.Y) * (x - c.X);

            // Early-out if any two cross-products have opposite signs (not in triangle)
            return (cross1 >= 0 && cross2 >= 0 && cross3 >= 0) || (cross1 <= 0 && cross2 <= 0 && cross3 <= 0);
        }
    }
}
