using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeightmapVisualizer
{
	internal class Edge
	{
		public Vertex P1 { get; set; }
		public Vertex P2 { get; set; }

		public Edge(Vertex p1, Vertex p2)
		{
			P1 = p1;
			P2 = p2;

			p1.Edges.Add(this);
			p2.Edges.Add(this);
		}
	}
}
