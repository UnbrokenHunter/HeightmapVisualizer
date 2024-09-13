using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HeightmapVisualizer
{
	internal class DrawHeightmap
	{
		public static void Draw(PaintEventArgs e, Graphics g, Heightmap hm)
		{
			if (e == null || g == null || hm == null) return;

			Cuboid[,] hm3D = hm.Map3D();

			for (int i = 0; i < hm.Map.GetLength(0); i++)
			{
				for (int j = 0; j < hm.Map.GetLength(1); j++)
				{
					Cuboid value = hm3D[i, j];
					Pen pen = new (Color.FromArgb(255, Math.Min((int) value.height / 12, 255), Math.Min((int) value.height / 6, 255), Math.Min((int) value.height, 255)));

					foreach (Edge edge in value.edges)
					{
						edge.DrawEdge(g, pen);
					}

					pen.Dispose();
				}
			}
		}
	}
}

