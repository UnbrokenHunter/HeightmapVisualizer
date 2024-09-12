using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeightmapVisualizer
{
	internal class CreateHeightmap
	{
		public Heightmap Map;

		public CreateHeightmap(int xSize, int ySize) 
		{
			Map = new Heightmap(xSize, ySize);

			CalculateValues(Map);
		}

		private void CalculateValues(Heightmap hm)
		{
			Random random = new Random();
			for (int i = 0; i < hm.Map.GetLength(0); i++)
			{
				for(int j = 0; j < hm.Map.GetLength(1); j++)
				{
					hm.Map[i, j] = 100;//(float) random.NextDouble() * 10;
				}
			}
		}
	}
}
