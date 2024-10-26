using HeightmapVisualizer.Primitives;
using System.Numerics;

namespace HeightmapVisualizer.src.Shapes
{
    /// <summary>
    /// Factory class for creating a pyramid shape.
    /// </summary>
    public static class Pyramid
    {
        /// <summary>
        /// Creates a pyramid mesh with the specified position, rotation, and size, where the position is treated as the center of the pyramid.
        /// </summary>
        /// <param name="position">The center position of the pyramid in the scene.</param>
        /// <param name="rotation">The rotation of the pyramid (as a quaternion).</param>
        /// <param name="size">The size of the pyramid (baseWidth, baseDepth, height).</param>
        /// <param name="color">The color of the object. Defaults to black</param>
        /// <returns>A <see cref="Mesh"/> object representing the pyramid.</returns>
        public static Mesh CreateCentered(Vector3 position, Quaternion rotation, Vector3 size, Color? color = null, bool drawWireframe = false)
        {
            var faces = CreatePyramidFaces(size.X, size.Z, size.Y, true);
            var mesh = new Mesh(faces, color, drawWireframe);
            mesh.Transform.Position = position;
            mesh.Transform.Rotation = rotation;
            return mesh;
        }

        /// <summary>
        /// Creates a pyramid mesh with the specified position and size, defaulting to no rotation (Quaternion.Identity), where the position is treated as the center of the pyramid.
        /// </summary>
        /// <param name="position">The center position of the pyramid in the scene.</param>
        /// <param name="size">The size of the pyramid (baseWidth, baseDepth, height).</param>
        /// <param name="color">The color of the object. Defaults to black</param>
        /// <returns>A <see cref="Mesh"/> object representing the pyramid.</returns>
        public static Mesh CreateCentered(Vector3 position, Vector3 size, Color? color = null, bool drawWireframe = false)
        {
            return CreateCentered(position, Quaternion.Identity, size, color, drawWireframe);
        }

        /// <summary>
        /// Creates a pyramid mesh with the specified position, rotation, and size, where the position is treated as one corner of the pyramid.
        /// </summary>
        /// <param name="position">The corner position of the pyramid in the scene.</param>
        /// <param name="rotation">The rotation of the pyramid (as a quaternion).</param>
        /// <param name="size">The size of the pyramid (baseWidth, baseDepth, height).</param>
        /// <param name="color">The color of the object. Defaults to black</param>
        /// <returns>A <see cref="Mesh"/> object representing the pyramid.</returns>
        public static Mesh CreateCorners(Vector3 position, Quaternion rotation, Vector3 size, Color? color = null, bool drawWireframe = false)
        {
            var faces = CreatePyramidFaces(size.X, size.Z, size.Y, false);
            var mesh = new Mesh(faces, color, drawWireframe);
            mesh.Transform.Position = position;
            mesh.Transform.Rotation = rotation;
            return mesh;
        }

        /// <summary>
        /// Creates a pyramid mesh with the specified position and size, defaulting to no rotation (Quaternion.Identity), where the position is treated as one corner of the pyramid.
        /// </summary>
        /// <param name="position">The corner position of the pyramid in the scene.</param>
        /// <param name="size">The size of the pyramid (baseWidth, baseDepth, height).</param>
        /// <param name="color">The color of the object. Defaults to black</param>
        /// <returns>A <see cref="Mesh"/> object representing the pyramid.</returns>
        public static Mesh CreateCorners(Vector3 position, Vector3 size, Color? color = null, bool drawWireframe = false)
        {
            return CreateCorners(position, Quaternion.Identity, size, color, drawWireframe);
        }

        /// <summary>
        /// Helper method that creates the faces of the pyramid, with an option to center the vertices or not.
        /// </summary>
        private static Face[] CreatePyramidFaces(float baseWidth, float baseDepth, float height, bool centered)
        {
            var halfWidth = centered ? baseWidth / 2 : 0;
            var halfDepth = centered ? baseDepth / 2 : 0;

            Vector3 v1 = new Vector3(-halfWidth, 0, -halfDepth);  // Base front left
            Vector3 v2 = new Vector3(baseWidth - halfWidth, 0, -halfDepth);   // Base front right
            Vector3 v3 = new Vector3(baseWidth - halfWidth, 0, baseDepth - halfDepth);    // Base back right
            Vector3 v4 = new Vector3(-halfWidth, 0, baseDepth - halfDepth);   // Base back left
            Vector3 vApex = new Vector3(0 - halfWidth, height, 0 - halfDepth);  // Apex of the pyramid

            return new Face[]
            {
                new Face(new[] { v1, v2, vApex }, name: "Front"), // Front face
                new Face(new[] { v2, v3, vApex }, name: "Right"), // Right face
                new Face(new[] { v3, v4, vApex }, name: "Back"), // Back face
                new Face(new[] { v4, v1, vApex }, name: "Left"), // Left face
                new Face(new[] { v1, v2, v3, v4 }, name: "Base") // Base face (quad) 
            };
        }
    }
}
