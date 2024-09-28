using HeightmapVisualizer.Scene;
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer.Primitives
{
	internal class Vertex
    {

        public Vector3 Position() => RelativePosition + Transform.Position; // Rotated Global Points
        private Vector3 RelativePosition => Transform.ToWorldSpace(LocalPosition); // Rotated points between -1 and 1
        
        private readonly Vector3 LocalPosition; // Points between -1 and 1
		public Transform Transform { get; set; }

        public List<Edge> Edges = new();

        public Vertex(Vector3 position)
        {
            this.LocalPosition = position;
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
