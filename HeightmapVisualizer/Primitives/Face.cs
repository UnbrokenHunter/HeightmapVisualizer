using HeightmapVisualizer.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeightmapVisualizer.Primitives
{
	internal class Face
	{
		public Edge[] Edges;

		public Face(Vertex p1, Vertex p2, Vertex p3)
		{
			Edges = new Edge[3];

			Edges[0] = new Edge(p1, p2);
			Edges[1] = new Edge(p2, p3);
			Edges[2] = new Edge(p3, p1);

			Edges[0].Faces = this;
		}

		public void DrawFace(Graphics g, Camera cam, Pen pen)
		{
			throw new NotImplementedException();
		}

		public override string ToString()
		{
			return $"Face: ({Edges[0]}, {Edges[1]}, {Edges[2]})";
		}

	}
}
