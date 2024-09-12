using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HeightmapVisualizer
{
	internal class Cuboid
	{

		public float x, z, height, rectSize;

		// Upper Front Left & Right
		public Vertex ufl;
		public Vertex ufr;

		// Upper Back Left & Right
		public Vertex ubl;
		public Vertex ubr;

		// Down Front Left & Right
		public Vertex dfr;
		public Vertex dfl;

		// Down Back Left & Right
		public Vertex dbl;
		public Vertex dbr;

		public Edge[] edges = new Edge[12];

		public Cuboid(float x, float y, float z, float height, float rectSize) 
		{
			this.x = x; 
			this.z = z; 
			this.height = height; 
			this.rectSize = rectSize;

			// Bottom vertices (y = 0)
			dbl = new Vertex(x,				y,				z);
			dbr = new Vertex(x + rectSize,	y,				z);
			dfl = new Vertex(x,				y,				z + rectSize);
			dfr = new Vertex(x + rectSize,	y,				z + rectSize);

			// Top vertices (y = height)
			ubl = new Vertex(x,				y - height,		z);
			ubr = new Vertex(x + rectSize,	y - height,		z);
			ufl = new Vertex(x,				y - height,		z + rectSize);
			ufr = new Vertex(x + rectSize,	y - height,		z + rectSize);


			// Define the edges
			edges[0] = new Edge(dbl, dbr);
			edges[1] = new Edge(dfl, dbl);
			edges[2] = new Edge(dfl, dfr);
			edges[3] = new Edge(dfr, dbr);

			edges[4] = new Edge(ubl, ubr);
			edges[5] = new Edge(ufl, ubl);
			edges[6] = new Edge(ufl, ufr);
			edges[7] = new Edge(ufr, ubr);

			edges[8] = new Edge(dbl, ubl);
			edges[9] = new Edge(dfl, ufl);
			edges[10] = new Edge(dfr, ufr);
			edges[11] = new Edge(dbr, ubr);

		}
	}
}
