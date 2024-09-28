using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.Scene;
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer.Shapes
{
    internal class Cuboid : Gameobject
    {
		public Face[] Faces;
        public Vector3 corner, size;

        public Cuboid(Transform transform, Vector3 cornerOffset, Vector3 size) : base(transform)
        {
            this.corner = cornerOffset;
            this.size = size;

            Vertex[] Vertices = new Vertex[8];

            // Bottom vertices (y = 0)
            Vertices[0] = new Vertex(new Vector3(corner.x, corner.y, corner.z));
            Vertices[1] = new Vertex(new Vector3(corner.x + size.x, corner.y, corner.z));
            Vertices[2] = new Vertex(new Vector3(corner.x, corner.y, corner.z + size.z));
            Vertices[3] = new Vertex(new Vector3(corner.x + size.x, corner.y, corner.z + size.z));

            // Top vertices (y = size.y)
            Vertices[4] = new Vertex(new Vector3(corner.x, corner.y - size.y, corner.z));
            Vertices[5] = new Vertex(new Vector3(corner.x + size.x, corner.y - size.y, corner.z));
            Vertices[6] = new Vertex(new Vector3(corner.x, corner.y - size.y, corner.z + size.z));
            Vertices[7] = new Vertex(new Vector3(corner.x + size.x, corner.y - size.y, corner.z + size.z));

            Faces = new Face[12]
            {
				new Face(Vertices[6], Vertices[7], Vertices[4]),
				new Face(Vertices[5], Vertices[7], Vertices[4]),
				new Face(Vertices[2], Vertices[3], Vertices[0]),
				new Face(Vertices[1], Vertices[3], Vertices[0]),

				new Face(Vertices[6], Vertices[4], Vertices[0]),
				new Face(Vertices[6], Vertices[0], Vertices[0]),
				new Face(Vertices[7], Vertices[5], Vertices[1]),
				new Face(Vertices[7], Vertices[1], Vertices[1]),

				new Face(Vertices[5], Vertices[4], Vertices[0]),
				new Face(Vertices[1], Vertices[0], Vertices[5]),
				new Face(Vertices[7], Vertices[6], Vertices[2]),
				new Face(Vertices[3], Vertices[2], Vertices[7])
			};
        }

		public override void Init()
		{
			foreach (Face face in Faces)
			{
				foreach (Edge edge in face.Edges)
				{
					foreach (Vertex vertex in edge.Points)
					{
						vertex.Transform = Transform;
					}
				}
			}
		}

		public override Renderable? GetRenderable()
		{
			return new Renderable(Faces, Color.AntiqueWhite);
		}
	}
}
