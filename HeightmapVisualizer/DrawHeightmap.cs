using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeightmapVisualizer
{
	internal class DrawHeightmap
	{

		private static float _boxSize = 40f;

		public static void Draw(PaintEventArgs e, Graphics g, Heightmap hm)
		{
			if (e == null || g == null) return;

			for (int i = 0; i < hm.Map.GetLength(0); i++)
			{
				for (int j = 0; j < hm.Map.GetLength(1); j++)
				{
					float value = hm.Map[i, j];
					Brush pen = new SolidBrush(Color.FromArgb(255, (int) value, 255, 255));

					g.FillRectangle(pen, 
						i * _boxSize, 
						j * _boxSize, 
						_boxSize, 
						_boxSize);

					pen.Dispose();
				}
			}
		}
	}
}
