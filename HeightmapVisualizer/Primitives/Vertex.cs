using HeightmapVisualizer.Scene;
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer.Primitives
{
    /// <summary>
    /// Represents a vertex in 3D space as part of a mesh.
    /// Stores a local position and provides a method to compute its global position.
    /// Also tracks the edges this vertex is part of.
    /// </summary>
    internal sealed class Vertex : Primitive, IEquatable<Vertex>
    {
        /// <summary>
        /// A list of edges this vertex is a part of.
        /// </summary>
        internal List<Edge> Edges { get; } = new();

        /// <summary>
        /// The local position of the vertex in the mesh's local coordinate space.
        /// </summary>
        public readonly Vector3 LocalPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex"/> class with a given mesh and local position.
        /// </summary>
        /// <param name="mesh">The mesh to which this vertex belongs.</param>
        /// <param name="position">The local position of the vertex in 3D space.</param>
        public Vertex(Mesh mesh, Color color, Vector3 position) : base(mesh, color)
        {
            this.LocalPosition = position;
        }

        /// <summary>
        /// Gets the global position of the vertex by applying the mesh's transform.
        /// </summary>
        /// <returns>The global position of the vertex in 3D space.</returns>
        public Vector3 Position => mesh.Transform.ToWorldSpace(LocalPosition) + mesh.Transform.Position;

        #region Overriding Equality

        /// <summary>
        /// Determines whether the specified <see cref="Vertex"/> is equal to the current <see cref="Vertex"/>.
        /// </summary>
        /// <param name="other">The vertex to compare with the current vertex.</param>
        /// <returns>true if the specified vertex is equal to the current vertex; otherwise, false.</returns>
        public bool Equals(Vertex? other)
        {
            if (other == null)
                return false;
            return LocalPosition.Equals(other.LocalPosition);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current vertex.
        /// </summary>
        /// <param name="obj">The object to compare with the current vertex.</param>
        /// <returns>true if the object is equal to the current vertex; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as Vertex);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return LocalPosition.GetHashCode();
        }

        #endregion

        /// <summary>
        /// Draws the vertex using the provided graphics context and camera.
        /// </summary>
        /// <param name="g">The graphics context used for drawing.</param>
        /// <param name="cam">The camera used for projection.</param>
        public override void Draw(Graphics g, Camera cam)
        {
			if (LocalPosition == null)
				return;

			var p = cam.ProjectPoint(Position);

            int size = 6;
            var rect = new Rectangle((int)(p.x - size / 2), (int)(p.y - size / 2), size, size);

			g.FillPie(new SolidBrush(color), rect, 0f, 360f);
		}

		/// <summary>
		/// Returns a string that represents the current vertex.
		/// </summary>
		/// <returns>A string that represents the current vertex's position.</returns>
		public override string ToString()
        {
            return $"Vertex: {Position}";
        }
    }
}
