using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer.Shapes
{
    internal class Cuboid : Shape
    {
        public Vector3 corner, size;

        public Cuboid(Transform transform, Vector3 cornerOffset, Vector3 size) : base(transform)
        {
            this.corner = cornerOffset;
            this.size = size;

            transform.Vertices = new Vertex[8];

            // Bottom vertices (y = 0)
            transform.Vertices[0] = new Vertex(new Vector3(corner.x, corner.y, corner.z));
            transform.Vertices[1] = new Vertex(new Vector3(corner.x + size.x, corner.y, corner.z));
            transform.Vertices[2] = new Vertex(new Vector3(corner.x, corner.y, corner.z + size.z));
            transform.Vertices[3] = new Vertex(new Vector3(corner.x + size.x, corner.y, corner.z + size.z));

            // Top vertices (y = size.y)
            transform.Vertices[4] = new Vertex(new Vector3(corner.x, corner.y - size.y, corner.z));
            transform.Vertices[5] = new Vertex(new Vector3(corner.x + size.x, corner.y - size.y, corner.z));
            transform.Vertices[6] = new Vertex(new Vector3(corner.x, corner.y - size.y, corner.z + size.z));
            transform.Vertices[7] = new Vertex(new Vector3(corner.x + size.x, corner.y - size.y, corner.z + size.z));

            transform.Faces = new Face[12]
            {
				new Face(transform.Vertices[6], transform.Vertices[7], transform.Vertices[4]),
				new Face(transform.Vertices[5], transform.Vertices[7], transform.Vertices[4]),
				new Face(transform.Vertices[2], transform.Vertices[3], transform.Vertices[0]),
				new Face(transform.Vertices[1], transform.Vertices[3], transform.Vertices[0]),

				new Face(transform.Vertices[6], transform.Vertices[4], transform.Vertices[0]),
				new Face(transform.Vertices[6], transform.Vertices[0], transform.Vertices[0]),
				new Face(transform.Vertices[7], transform.Vertices[5], transform.Vertices[1]),
				new Face(transform.Vertices[7], transform.Vertices[1], transform.Vertices[1]),

				new Face(transform.Vertices[5], transform.Vertices[4], transform.Vertices[0]),
				new Face(transform.Vertices[1], transform.Vertices[0], transform.Vertices[5]),
				new Face(transform.Vertices[7], transform.Vertices[6], transform.Vertices[2]),
				new Face(transform.Vertices[3], transform.Vertices[2], transform.Vertices[7])
			};
        }

		public override void Init()
		{
			foreach (Face face in Transform.Faces)
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

		public override void Update()
		{
			//throw new NotImplementedException();
		}
	}
}
