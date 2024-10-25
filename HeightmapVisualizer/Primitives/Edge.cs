using HeightmapVisualizer.Scene;
using HeightmapVisualizer.Units;
using HeightmapVisualizer.Utilities;
using System.Numerics;

namespace HeightmapVisualizer.Primitives
{
    /// <summary>
    /// Represents an edge in 3D space, defined by two vertices.
    /// An edge can be part of multiple triangles (tris).
    /// </summary>
    internal sealed class Edge : Primitive, IEquatable<Edge>
    {
        /// <summary>
        /// An array of two vertices that define the edge.
        /// </summary>
        internal Vertex[] Vertices { get; }

        /// <summary>
        /// A list of triangles (tris) this edge is a part of.
        /// </summary>
        internal List<Tri> Tris { get; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Edge"/> class with the given mesh and two vertex positions.
        /// This constructor ensures that any identical vertices already in the mesh are reused by checking the mesh's vertex dictionary.
        /// If a vertex with the same position exists, it will be reused; otherwise, a new vertex will be created.
        /// </summary>
        /// <param name="mesh">The mesh to which this edge belongs.</param>
        /// <param name="p1">The first vertex position of the edge.</param>
        /// <param name="p2">The second vertex position of the edge.</param>
        public Edge(Mesh mesh, Color color, Vector3 p1, Vector3 p2) : base(mesh, color)
        {
            Vertices = new Vertex[2];

            // Nested function to handle the GetOrCreate logic for vertices
            Vertex GetOrCreateVertex(Vector3 position)
            {
                if (mesh.vertexDict.TryGetValue(position, out var existingVertex))
                {
                    existingVertex.Edges.Add(this);
                    return existingVertex;
                }
                else
                {
                    var newVertex = new Vertex(mesh, color, position);
                    newVertex.Edges.Add(this);
                    mesh.vertexDict[position] = newVertex;
                    return newVertex;
                }
            }

            // Assign vertices using the nested GetOrCreateVertex method
            Vertices[0] = GetOrCreateVertex(p1);
            Vertices[1] = GetOrCreateVertex(p2);
        }

        /// <summary>
        /// Draws the edge using the provided graphics context and camera.
        /// Projects the vertices into 2D space and renders a line between them.
        /// </summary>
        /// <param name="g">The graphics context used for drawing.</param>
        /// <param name="cam">The camera used for projection.</param>
        public override void Draw(Graphics g, Camera cam)
        {
            if (Vertices[0] == null || Vertices[1] == null)
                return;

            var p1 = cam.ProjectPoint(Vertices[0].Position);
            var p2 = cam.ProjectPoint(Vertices[1].Position);

            // One point is on screen
            if (p1.Item2 || p2.Item2)
            {
                g.DrawLine(ColorLookup.FindOrGetPen(color), p1.Item1.X, p1.Item1.Y, p2.Item1.X, p2.Item1.Y);
            }

        }

        #region Overriding Equality

        /// <summary>
        /// Determines whether the specified <see cref="Edge"/> is equal to the current <see cref="Edge"/>.
        /// Two edges are considered equal if they have the same vertices, regardless of the order.
        /// </summary>
        /// <param name="other">The edge to compare with the current edge.</param>
        /// <returns>true if the specified edge is equal to the current edge; otherwise, false.</returns>
        public bool Equals(Edge? other)
        {
            if (other == null)
                return false;

            var val = (Vertices[0].Equals(other.Vertices[0]) && Vertices[1].Equals(other.Vertices[1])) ||
                   (Vertices[0].Equals(other.Vertices[1]) && Vertices[1].Equals(other.Vertices[0]));

            Console.WriteLine($"V1: {Vertices[0]} V2: {Vertices[1]}\nO1: {other.Vertices[0]} O2: {other.Vertices[1]}\n{val}");

            // Edges are equal if they share the same vertices, regardless of the order
            return (Vertices[0].Equals(other.Vertices[0]) && Vertices[1].Equals(other.Vertices[1])) ||
                   (Vertices[0].Equals(other.Vertices[1]) && Vertices[1].Equals(other.Vertices[0]));
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current edge.
        /// </summary>
        /// <param name="obj">The object to compare with the current edge.</param>
        /// <returns>true if the object is equal to the current edge; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as Edge);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            // Hash code combines both vertices' positions, order-independent
            int hash1 = Vertices[0].GetHashCode();
            int hash2 = Vertices[1].GetHashCode();
            return hash1 ^ hash2; // XOR for order-independence
        }

        #endregion

        /// <summary>
        /// Returns a string that represents the current edge.
        /// </summary>
        /// <returns>A string that represents the edge's vertices.</returns>
        public override string ToString()
        {
            return $"Edge: ({Vertices[0]}, {Vertices[1]})";
        }
    }
}
