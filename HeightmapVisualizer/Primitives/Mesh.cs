using HeightmapVisualizer.Scene;
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer.Primitives
{
	/// <summary>
	/// Represents a mesh consisting of multiple triangular faces.
	/// The Mesh class stores the triangles and ensures that vertices and edges are reused when constructing the mesh,
	/// avoiding duplicate objects.
	/// </summary>
	public class Mesh : Gameobject
	{
		/// <summary>
		/// A list of triangles that form the mesh.
		/// </summary>
		private readonly List<Tri> Tris;

		/// <summary>
		/// Dictionary to track unique vertices in the mesh, allowing for reuse of existing vertices.
		/// </summary>
		internal Dictionary<Vector3, Vertex> vertexDict = new();

		/// <summary>
		/// Dictionary to track unique edges in the mesh, allowing for reuse of existing edges.
		/// </summary>
		internal Dictionary<(Vector3, Vector3), Edge> edgeDict = new();

		/// <summary>
		/// Initializes a new instance of the <see cref="Mesh"/> class with the provided faces.
		/// The constructor automatically triangulates the faces and stores them in the mesh. 
		/// Additionally, it ensures that vertices and edges are reused where possible to avoid duplicates.
		/// </summary>
		/// <param name="faces">An array of faces (IFace) used to construct the mesh.</param>
		public Mesh(Face[] faces)
		{
			// Triangulate the faces and store the resulting triangles
			Tris = faces.SelectMany(e => e.Triangulate(this)).ToList();
		}

		/// <summary>
		/// Renders the mesh by drawing each edge using the provided graphics context and camera.
		/// </summary>
		/// <param name="g">The graphics context used to render the mesh.</param>
		/// <param name="cam">The camera used to project the mesh for rendering.</param>
		public void Render(Graphics g, Camera cam)
		{
			// Draw all the edges in the mesh
			foreach (Edge edge in edgeDict.Values)
			{
				edge.Draw(g, cam);
			}
		}
	}
}
