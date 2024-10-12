using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.Rendering;
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer.Shapes
{
    /// <summary>
    /// Factory class for creating a plane shape.
    /// </summary>
    public static class Plane
    {
        /// <summary>
        /// Creates a plane mesh with the specified position, rotation, and size, where the position is treated as the center of the plane.
        /// </summary>
        /// <param name="position">The center position of the plane in the scene.</param>
        /// <param name="rotation">The rotation of the plane (as a quaternion).</param>
        /// <param name="size">The size of the plane (width and depth).</param>
        /// <param name="color">The color of the object. Defaults to black</param>
        /// <returns>A <see cref="Mesh"/> object representing the plane.</returns>
        public static Mesh CreateCentered(Vector3 position, Quaternion rotation, Vector2 size, Color? color = null, DrawingMode mode = DrawingMode.None)
        {
            // Create the faces of the plane based on size, using the position as the center
            var faces = CreatePlaneFaces(size.x, size.y, true);

            // Create the mesh with the faces
            var mesh = new Mesh(faces, color, mode);

            // Apply the position and rotation to the mesh's transform
            mesh.Transform.Position = position;
            mesh.Transform.Rotation = rotation;

            return mesh;
        }

        /// <summary>
        /// Creates a plane mesh with the specified position and size, defaulting to no rotation (Quaternion.Identity), where the position is treated as the center of the plane.
        /// </summary>
        /// <param name="position">The center position of the plane in the scene.</param>
        /// <param name="size">The size of the plane (width and depth).</param>
        /// <param name="color">The color of the object. Defaults to black</param>
        /// <returns>A <see cref="Mesh"/> object representing the plane.</returns>
        public static Mesh CreateCentered(Vector3 position, Vector2 size, Color? color = null, DrawingMode mode = DrawingMode.None)
        {
            return CreateCentered(position, Quaternion.Identity, size, color, mode);
        }

        /// <summary>
        /// Creates a plane mesh with the specified position, rotation, and size, where the position is treated as one corner of the plane.
        /// </summary>
        /// <param name="position">The corner position of the plane in the scene.</param>
        /// <param name="rotation">The rotation of the plane (as a quaternion).</param>
        /// <param name="size">The size of the plane (width and depth).</param>
        /// <param name="color">The color of the object. Defaults to black</param>
        /// <returns>A <see cref="Mesh"/> object representing the plane.</returns>
        public static Mesh CreateCorners(Vector3 position, Quaternion rotation, Vector2 size, Color? color = null, DrawingMode mode = DrawingMode.None)
        {
            // Create the faces of the plane based on size, using the position as one of the corners
            var faces = CreatePlaneFaces(size.x, size.y, false);

            // Create the mesh with the faces
            var mesh = new Mesh(faces, color, mode);

            // Apply the position and rotation to the mesh's transform
            mesh.Transform.Position = position;
            mesh.Transform.Rotation = rotation;

            return mesh;
        }

        /// <summary>
        /// Creates a plane mesh with the specified position and size, defaulting to no rotation (Quaternion.Identity), where the position is treated as one corner of the plane.
        /// </summary>
        /// <param name="position">The corner position of the plane in the scene.</param>
        /// <param name="size">The size of the plane (width and depth).</param>
        /// <param name="color">The color of the object. Defaults to black</param>
        /// <returns>A <see cref="Mesh"/> object representing the plane.</returns>
        public static Mesh CreateCorners(Vector3 position, Vector2 size, Color? color = null, DrawingMode mode = DrawingMode.None)
        {
            return CreateCorners(position, Quaternion.Identity, size, color, mode);
        }

        /// <summary>
        /// Helper method that creates the two triangular faces of the plane, with an option to center the vertices or not.
        /// </summary>
        /// <param name="width">The width of the plane.</param>
        /// <param name="depth">The depth of the plane.</param>
        /// <param name="centered">Whether to treat the position as the center of the plane.</param>
        /// <returns>An array of <see cref="Face"/> objects representing the plane's triangles.</returns>
        private static Face[] CreatePlaneFaces(float width, float depth, bool centered)
        {
            var halfWidth = centered ? width / 2 : 0;
            var halfDepth = centered ? depth / 2 : 0;

            // Define the four vertices of the plane
            Vector3 v1 = new Vector3(-halfWidth, 0, -halfDepth); // Front-left
            Vector3 v2 = new Vector3(width - halfWidth, 0, -halfDepth); // Front-right
            Vector3 v3 = new Vector3(width - halfWidth, 0, depth - halfDepth); // Back-right
            Vector3 v4 = new Vector3(-halfWidth, 0, depth - halfDepth); // Back-left

            // Create the two triangular faces of the plane
            return new Face[]
            {
                new Face(new[] { v1, v2, v3 }), // First triangle (front-right)
                new Face(new[] { v1, v3, v4 })  // Second triangle (back-left)
            };
        }
    }
}
