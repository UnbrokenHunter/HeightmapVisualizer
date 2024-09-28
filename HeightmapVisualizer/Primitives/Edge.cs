using HeightmapVisualizer.Scene;

namespace HeightmapVisualizer.Primitives
{
	internal class Edge
    {
        public Vertex[] Points;
        public Face Faces;

        public Edge(Vertex p1, Vertex p2)
        {
            Points = new Vertex[2];

			Points[0] = p1;
            Points[1] = p2;
        }

        public void DrawEdge(Graphics g, Camera cam, Pen pen)
        {
            if (Points[0] == null || Points[1] == null)
                return;

            var p1 = cam.ProjectVertex(Points[0]);
            var p2 = cam.ProjectVertex(Points[1]);

            g.DrawLine(pen, p1.x, p1.y, p2.x, p2.y);
        }

		public override string ToString()
		{
            return $"\nEdge: (\n\t{Points[0]}, \n\t{Points[1]})";
		}
	}
}
