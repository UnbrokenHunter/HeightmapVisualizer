using HeightmapVisualizer.Scene;
using HeightmapVisualizer.src.Components;
using HeightmapVisualizer.src.Utilities;
using System.Numerics;

namespace HeightmapVisualizer.Primitives
{
    /// <summary>
    /// Represents a triangle (Tri) in 3D space as part of a mesh.
    /// Defined by three edges that connect three vertices.
    /// </summary>
    internal sealed class Tri : Primitive, IEquatable<Tri>
    {
        /// <summary>
        /// The edges that define the triangle. A triangle has exactly three edges.
        /// </summary>
        internal Edge[] Edges { get; }

        /// <summary>
        /// The three original points used to create the object
        /// </summary>
        internal Vertex[] Points { get; }

        internal Vector3 Normal { get; }
        private Vector3 CalculateNormal()
        {
			var edge1 = Points[1].Position - Points[0].Position;
			var edge2 = Points[2].Position - Points[0].Position;

            return Vector3.Normalize(Vector3.Cross(edge1, edge2));
		}

		internal string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tri"/> class with the given mesh and three vertex positions.
        /// Automatically constructs the three edges that form the triangle. This constructor ensures that any identical
        /// edges already in the mesh are reused by checking the mesh's edge dictionary. If an edge between the same two
        /// vertices exists, it will be reused; otherwise, a new edge will be created.
        /// </summary>
        /// <param name="mesh">The mesh to which this triangle belongs.</param>
        /// <param name="color">The color to draw this Tri with</param>
        /// <param name="p1">The first vertex position of the triangle.</param>
        /// <param name="p2">The second vertex position of the triangle.</param>
        /// <param name="p3">The third vertex position of the triangle.</param>
        public Tri(Mesh mesh, Color color, Vector3 p1, Vector3 p2, Vector3 p3, string name) : base(mesh, color)
        {

            Edges = new Edge[3];

            // Nested function to handle the GetOrCreate logic for edges
            Edge GetOrCreateEdge(Vector3 v1, Vector3 v2)
            {
                //Console.WriteLine($"---------------------\nPoint 1: {v1} \n\n Point 2: {v2} \n\n {string.Join("\n", mesh.edgeDict.Values)} \n------------------- ");

                if (mesh.edgeDict.TryGetValue((v1, v2), out var existingEdge1))
                {
                    existingEdge1.Tris.Add(this);
                    return existingEdge1;
                }
                else if (mesh.edgeDict.TryGetValue((v2, v1), out var existingEdge2))
                {
                    existingEdge2.Tris.Add(this);
                    return existingEdge2;
                }
                else
                {
                    var newEdge = new Edge(mesh, color, v1, v2);
                    newEdge.Tris.Add(this);
                    mesh.edgeDict[(v1, v2)] = newEdge;
                    //Console.WriteLine("A New Value was created: " + newEdge);
                    return newEdge;
                }
            }

            // Assign edges using the nested GetOrCreateEdge method
            Edges[0] = GetOrCreateEdge(p1, p2);
            Edges[1] = GetOrCreateEdge(p2, p3);
            Edges[2] = GetOrCreateEdge(p3, p1);

			List<Vertex> vector3s = new List<Vertex>
			{
				Edges[0].Vertices[0],
				Edges[0].Vertices[1]
			};

			if (vector3s.Contains(Edges[1].Vertices[0]))
                vector3s.Add(Edges[1].Vertices[1]);
            else
				vector3s.Add(Edges[1].Vertices[0]);

			Points = vector3s.ToArray();

            this.Name = name;
            this.Normal = CalculateNormal();
		}

		/// <summary>
		/// Draws the triangle by rendering each of its edges.
		/// </summary>
		/// <param name="g">The graphics context used for drawing.</param>
		/// <param name="cam">The camera used for projection.</param>
		public override void Draw(Graphics g, CameraComponent cam)
        {
			if (Points[0] == null || Points[1] == null || Points[2] == null)
				return;

			var p1 = cam.ProjectPoint(Points[0].Position);
			var p2 = cam.ProjectPoint(Points[1].Position);
			var p3 = cam.ProjectPoint(Points[2].Position);

            PointF pF(Vector2 v) => new PointF(v.X, v.Y);

            
            // Atleast one point on screen
            if (p1.Item2 || p2.Item2 || p3.Item2)
            {
				var p = new PointF[] { pF(p1.Item1), pF(p2.Item1), pF(p3.Item1) };
				g.FillPolygon(ColorLookup.FindOrGetBrush(color), p);

				Edges[0].Draw(g, cam);
				Edges[1].Draw(g, cam);
				Edges[2].Draw(g, cam);

			}

		}

		#region Overriding Equality

		/// <summary>
		/// Determines whether the specified <see cref="Tri"/> is equal to the current <see cref="Tri"/>.
		/// Two triangles are considered equal if they have the same edges, in the same order.
		/// </summary>
		/// <param name="other">The triangle to compare with the current triangle.</param>
		/// <returns>true if the specified triangle is equal to the current triangle; otherwise, false.</returns>
		public bool Equals(Tri? other)
        {
            if (other == null)
                return false;

            return Edges[0].Equals(other.Edges[0]) &&
                   Edges[1].Equals(other.Edges[1]) &&
                   Edges[2].Equals(other.Edges[2]);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current triangle.
        /// </summary>
        /// <param name="obj">The object to compare with the current triangle.</param>
        /// <returns>true if the object is equal to the current triangle; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as Tri);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current triangle, based on its edges.</returns>
        public override int GetHashCode()
        {
            // Combine hash codes of the edges. Order matters since it's a triangle.
            int hash1 = Edges[0].GetHashCode();
            int hash2 = Edges[1].GetHashCode();
            int hash3 = Edges[2].GetHashCode();

            return hash1 ^ hash2 ^ hash3; // XOR to combine the hashes
        }

        #endregion

        /// <summary>
        /// Returns a string that represents the current triangle's edges.
        /// </summary>
        /// <returns>A string that represents the triangle.</returns>
        public override string ToString()
        {
            return $"Triangle: ({Edges[0]}, {Edges[1]}, {Edges[2]})";
        }
    }
}
