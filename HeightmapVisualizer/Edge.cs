using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

		public void DrawEdge(Graphics g, Pen pen)
		{
			var vec1 = new Vector3(P1.X, P1.Y, P1.Z);
			var p1 = Camera.Instance.ProjectVertex(vec1);

			var vec2 = new Vector3(P2.X, P2.Y, P2.Z);
			var p2 = Camera.Instance.ProjectVertex(vec2);

			g.DrawLine(pen, p1.X, p1.Y, p2.X, p2.Y);
		}
	}
}
