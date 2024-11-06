using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;

namespace GraphicsPipeline
{
    public class GraphicsPipeline
    {
        public static Bitmap Render(Bitmap bitmap, List<(float, RenderData[])> objects)
        {
            // Render each triangle in sorted order
            foreach (var e in objects)
            {
                Rasterization.Rasterizer.RenderTriangle(bitmap, e.Item2);
            }
            return bitmap;
        }
    }
}
