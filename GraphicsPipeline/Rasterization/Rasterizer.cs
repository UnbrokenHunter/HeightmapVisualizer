
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

            foreach (var part in mesh)
            {
                DrawTriangle();

                void DrawTriangle()
                {
                    Bresenham(bmpData, (int)part.P1.X, (int)part.P1.Y, (int)part.P2.X, (int)part.P2.Y, part.Color);
                    Bresenham(bmpData, (int)part.P2.X, (int)part.P2.Y, (int)part.P3.X, (int)part.P3.Y, part.Color);
                    Bresenham(bmpData, (int)part.P3.X, (int)part.P3.Y, (int)part.P1.X, (int)part.P1.Y, part.Color);
                }
            }

            BitmapManipulation.UnlockBitmap(bitmap, bmpData);
        }

        }

        private static void Bresenham(BitmapData bmpData, int x1, int y1, int x2, int y2, Color color)
        {
            // Point 1 is outside of bitmap
            if (!(x1 >= 0 && x1 < bmpData.Width
                && y1 >= 0 && y1 < bmpData.Height))
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
                if (bmpData.Width > x1 && bmpData.Height > y1 && 0 <= x1 && 0 <= y1)
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
    }
}
