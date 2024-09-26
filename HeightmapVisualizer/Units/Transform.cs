
using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.Units.Quaternion;

namespace HeightmapVisualizer.Units
{
	internal class Transform
	{
		public Vector3 Position { get; set; }
		public Quaternion Rotation { get; set; }
		public Vertex[] Vertices { get; set; }
		public Face[] Faces { get; set; }

		public Transform() : this(new Vector3(), new Quaternion()) { }

		public Transform(Vector3 position, Quaternion rotation)
		{
			Position = position;
			Rotation = rotation;
			Position.To
			
		}
	}
}
