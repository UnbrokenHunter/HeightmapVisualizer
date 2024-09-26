using HeightmapVisualizer.Scene;
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer.Primitives
{
    internal class Vertex
    {
        public Vector3 RelativePosition { get; set; }

        public Vector3 LocalPosition() => RelativePosition + Transform.Position;
		public Transform Transform { get; set; }

        public List<Edge> Edges = new();

        public Vertex(Vector3 position)
        {
            this.RelativePosition = position;
        }

        public void DrawPoint(Graphics g, Camera cam)
        {
            throw new NotImplementedException();
        }

		public bool Equals(Vertex p1)
        {
            return RelativePosition.Equals(p1.RelativePosition);
        }

        public override string ToString()
        {
            return $"Vertex: ({RelativePosition})";
        }
    }
}
