using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.Scene;
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer.Shapes
{
	internal class Renderable 
	{
		public readonly Face[] Faces;
		public readonly Edge[] Edges;
		public readonly Vertex[] Vertices;

		public readonly Color Color;
	
		public Renderable(Face[] faces, Color color) 
		{
			this.Faces = faces;
			this.Edges = Faces.SelectMany(e => e.Edges).Distinct().ToArray();
			this.Vertices = Edges.SelectMany(e => e.Points).Distinct().ToArray();
			this.Color = color;
		}

		public void Render(Graphics g, Camera cam)
		{
			foreach(Edge edge in Edges)
			{
				edge.DrawEdge(g, cam, new Pen(Color));
			}
		}
	}
}
