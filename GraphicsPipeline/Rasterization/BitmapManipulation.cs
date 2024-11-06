
using System.Drawing.Imaging;
using System.Drawing;

namespace GraphicsPipeline.Rasterization
{
    internal class BitmapManipulation
    {
        internal static BitmapData LockBitmap(Bitmap bitmap)
        {
            // Define the area to lock (in this case, the entire bitmap).
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            // Lock the bitmap's bits for read/write access.
            return bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
        }

        internal static void UnlockBitmap(Bitmap bitmap, BitmapData bmpData)
        {
            // Unlock the bits to apply changes.
            bitmap.UnlockBits(bmpData);
        }

        internal static void FastSetPixel(BitmapData bmpData, int x, int y, Color color)
        {
            // Calculate the pixel's position in memory.
            const int bytesPerPixel = 4; // 32bppArgb = 4 bytes per pixel
            int stride = bmpData.Stride;
            IntPtr ptr = bmpData.Scan0;

            // Calculate the byte offset for the pixel at (x, y).
            int offset = (y * stride) + (x * bytesPerPixel);

            // Convert the color to ARGB format.
            int argb = color.ToArgb();

            // Write the color data to the pixel position in memory.
            unsafe
            {
                byte* pixel = (byte*)ptr + offset;
                pixel[0] = (byte)(argb);         // Blue
                pixel[1] = (byte)(argb >> 8);    // Green
                pixel[2] = (byte)(argb >> 16);   // Red
                pixel[3] = (byte)(argb >> 24);   // Alpha
            }
        }
    }
}
