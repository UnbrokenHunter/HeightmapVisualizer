
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer.Primitives
{
	public class Face
	{
		private readonly Vector3[] points;

		public Face(Vector3[] points) 
		{
			this.points = points;
		}

		internal Tri[] Triangulate(Mesh mesh)
		{
			// should pass the mesh into each primitive tri it creates

			List<Tri> tris = new();

			int n = points.Length;
			for (int i = 1; i < n - 1; i++)
			{
				tris.Add(new Tri(mesh, points[0], points[i], points[i + 1]));
			}

			return tris.ToArray();
		}
	}
}
