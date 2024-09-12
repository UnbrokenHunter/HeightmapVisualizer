using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HeightmapVisualizer
{
	internal class Vertex
	{
		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }

		public List<Edge> Edges = new();

		public Vertex(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}
	}
}
