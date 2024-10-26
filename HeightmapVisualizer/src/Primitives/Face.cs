using HeightmapVisualizer.src.Primitives;
using System.Numerics;

namespace HeightmapVisualizer.Primitives
{
    /// <summary>
    /// Represents a face (polygon) in 3D space, defined by an array of vertices.
    /// Provides functionality to triangulate the face into triangles for rendering or processing.
    /// </summary>
    public class Face : IEquatable<Face>
    {
        /// <summary>
        /// An array of vertices that define the face.
        /// </summary>
        private readonly Vector3[] points;

        /// <summary>
        /// The color of this face. If null, will default to the color of this face's mesh
        /// </summary>
        private readonly Color? color;

        private readonly string? name;

        /// <summary>
        /// Initializes a new instance of the <see cref="Face"/> class with the specified points.
        /// </summary>
        /// <param name="points">An array of <see cref="Vector3"/> points that define the face.</param>
        /// <param name="color">The color to draw this face as. If left blank, will default to the color of the mesh</param>
        /// <param name="name">The name of this face, which can be searched for, in order to make changes to the mesh later.</param>
        public Face(Vector3[] points, Color? color = null, string? name = null)
        {               
            this.color = color;
            this.points = points;
            this.name = name;
        }

        /// <summary>
        /// Triangulates the face into an array of triangles.
        /// Note: This method assumes that the face is a convex polygon.
        /// Currently, it performs a simple fan triangulation starting from the first vertex.
        /// For concave polygons or polygons with holes, this method would not produce correct results.
        /// </summary>
        /// <param name="mesh">The mesh to which the resulting triangles will belong.</param>
        /// <param name="defaultColor">The color of the mesh. This will be the default color if a face does not have one selected</param>
        /// <returns>An array of <see cref="Tri"/> objects representing the triangulated face.</returns>
        internal Tri[] Triangulate(Mesh mesh, Color defaultColor)
        {
            // Note: This method only works correctly for convex polygons.
            // To handle concave polygons or polygons with holes, a more advanced triangulation algorithm,
            // such as ear clipping or Delaunay triangulation, would be required in the future.

            Color colorToUse = color ?? defaultColor;
            string nameToUse = name ?? string.Empty;

            List<Tri> tris = new();

            int n = points.Length;

            // Mesh is a Line
            if (n == 2)
            {
                tris.Add(new Tri(mesh, colorToUse, points[0], points[0], points[1], nameToUse));
            }
            else
            {
                // Mesh is not a line
                for (int i = 1; i < n - 1; i++)
                {
                    tris.Add(new Tri(mesh, colorToUse, points[0], points[i], points[i + 1], nameToUse));
                }
            }

            return tris.ToArray();
        }

        #region Overriding Equality

        /// <summary>
        /// Determines whether the specified object is equal to the current face.
        /// </summary>
        /// <param name="obj">The object to compare with the current face.</param>
        /// <returns><c>true</c> if the specified object is equal to the current face; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as Face);
        }

        /// <summary>
        /// Determines whether the specified face is equal to the current face.
        /// Two faces are considered equal if they have the same set of points in the same order.
        /// </summary>
        /// <param name="other">The face to compare with the current face.</param>
        /// <returns><c>true</c> if the specified face is equal to the current face; otherwise, <c>false</c>.</returns>
        public bool Equals(Face? other)
        {
            if (other == null)
                return false;

            if (points.Length != other.points.Length)
                return false;

            for (int i = 0; i < points.Length; i++)
            {
                if (!points[i].Equals(other.points[i]))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current face.</returns>
        public override int GetHashCode()
        {
            int hash = 17;
            foreach (var point in points)
            {
                hash = hash * 31 + point.GetHashCode();
            }
            return hash;
        }

        #endregion


        /// <summary>
        /// Returns a string that represents the current face.
        /// </summary>
        /// <returns>A string that lists the points defining the face.</returns>
        public override string ToString() => $"Face: ({string.Join(',', points.ToString())})";

    }
}
