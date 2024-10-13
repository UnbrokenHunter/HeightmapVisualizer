using HeightmapVisualizer.Rendering;
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
        internal readonly List<Tri> Tris;

        /// <summary>
        /// Dictionary to track unique vertices in the mesh, allowing for reuse of existing vertices.
        /// </summary>
        internal Dictionary<Vector3, Vertex> vertexDict = new();

        /// <summary>
        /// Dictionary to track unique edges in the mesh, allowing for reuse of existing edges.
        /// </summary>
        internal Dictionary<(Vector3, Vector3), Edge> edgeDict = new();

        /// <summary>
        /// How the mesh will be displayed
        /// </summary>
        internal DrawingMode mode;

        /// <summary>
        /// Initializes a new instance of the <see cref="Mesh"/> class with the provided faces.
        /// The constructor automatically triangulates the faces and stores them in the mesh. 
        /// Additionally, it ensures that vertices and edges are reused where possible to avoid duplicates.
        /// 
        /// A color can also be set here. What ever color is set on the mesh will be treated as the default.
        /// A Face can also set a color. If a face has a set color, it will have priotiry, however, if there is not
        /// color selected on a given face, it will default to the color of the mesh.
        /// 
        /// </summary>
        /// <param name="faces">An array of faces (IFace) used to construct the mesh.</param>
        /// <param name="color">The default color of this mesh. If a face does not have an override, then that face will default to this color.</param>
        /// <param name="mode">How the mesh will be drawn to the screen. It will default to lines</param>
        public Mesh(Face[] faces, Color? color = null, DrawingMode mode = DrawingMode.None)
        {
            // You cannot set black as a default value for some reason
            Color defaultColor = color ?? Color.Black;

            // Set the way that the points are drawn, defaults to lines
            this.mode = mode == DrawingMode.None ? DrawingMode.Lines : mode;

			// Triangulate the faces and store the resulting triangles
			Tris = faces.SelectMany(e => e.Triangulate(this, defaultColor)).ToList();
        }

        /// <summary>
        /// Returns all Tris on the mesh that need to be rendered
        /// </summary>
        public Renderable[] PointsToRender()
        {
                List<Renderable> renderables = new List<Renderable>();
                // Draw all the edges in the mesh
                foreach (Tri tri in Tris)
                {
                    renderables.Add(new Renderable(mode, tri));
                }
                return renderables.ToArray();
		}

		/// <summary>
		/// Overrides the Gameobject Method "GetRenderable()" to return itself. By doing this, it allows
		/// other gameobjects to instead have references to Meshs, rather than itself being a mesh. This 
		/// could be useful for example, to allow having multiple meshes controlled by one object.
		/// </summary>
		/// <returns>This object</returns>
		public override Mesh? GetRenderable()
        {
            return this;
        }

        /// <summary>
        /// Changes the color of all faces of the mesh to the selected color
        /// </summary>
        /// <param name="color">The color to change to</param>
        /// <returns></returns>
        public Mesh SetColor(Color color)
        {
            foreach (Tri tri in Tris)
            {
                tri.SetColor(color);
                foreach (Edge e in tri.Edges)
                {
                    e.SetColor(color);
                    foreach (Vertex v in e.Vertices)
                    {
                        v.SetColor(color);
                    }
                }
            }
            return this;
        }
    }
}
