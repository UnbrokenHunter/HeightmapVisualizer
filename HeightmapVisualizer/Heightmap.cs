using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeightmapVisualizer.Shapes;

namespace HeightmapVisualizer
{
    internal class Heightmap
	{
		/*
		public int Width, Height;
		public float[,] Map;
		public static float SquareSize = 4f;

		public Heightmap(int xSize, int ySize)
		{
			Width = xSize;
			Height = ySize;

			Map = new float[Width, Height];
		}

		public Cuboid[,] Map3D()
		{
			Cuboid[,] map3D = new Cuboid[Map.GetLength(0), Map.GetLength(1)];

			for (int i = 0; i < Map.GetLength(0); i++)
			{
				for (int j = 0; j < Map.GetLength(1); j++)
				{
					map3D[i, j] = new Cuboid(
						i * SquareSize * SquareSize, 
						0, 
						j * SquareSize * SquareSize,
						Map[i, j], 
						SquareSize);
				}
			}

			return map3D;
		}
		*/
	}
}
