using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeightmapVisualizer
{
	internal class Heightmap
	{
		public int Width, Height;
		public float[,] Map;

		public Heightmap(int xSize, int ySize)
		{
			Width = xSize;
			Height = ySize;

			Map = new float[Width, Height];
		}
	}
}
