using HeightmapVisualizer.src.Components;
using HeightmapVisualizer.src.Primitives;
using System.Numerics;
using static HeightmapVisualizer.src.Components.MeshComponent;

namespace HeightmapVisualizer.src.Factories
{
    /// <summary>
    /// Factory class for creating a pyramid shape.
    /// </summary>
    internal static class Pyramid
    {
        /// <summary>
        /// Creates a pyramid mesh with the specified position, rotation, and size, where the position is treated as the center of the pyramid.
        /// </summary>
        /// <param name="size">The size of the pyramid (baseWidth, baseDepth, height).</param>
        /// <returns>A <see cref="MeshComponent"/> object representing the pyramid.</returns>
        public static Face[] CreateCentered(Vector3 size, Vector3? position = null)
        {
            var offset = position ?? Vector3.Zero;
            return CreatePyramidFaces(size.X, size.Z, size.Y, true, offset);
        }

        /// <summary>
        /// Creates a pyramid mesh with the specified position, rotation, and size, where the position is treated as one corner of the pyramid.
        /// </summary>
        /// <param name="size">The size of the pyramid (baseWidth, baseDepth, height).</param>
        /// <returns>A <see cref="MeshComponent"/> object representing the pyramid.</returns>
        public static Face[] CreateCorners(Vector3 size, Vector3? position = null)
        {
            var offset = position ?? Vector3.Zero;
            return CreatePyramidFaces(size.X, size.Z, size.Y, false, offset);
        }

        /// <summary>
        /// Helper method that creates the faces of the pyramid, with an option to center the vertices or not.
        /// </summary>
        private static Face[] CreatePyramidFaces(float baseWidth, float baseDepth, float height, bool centered, Vector3 offset)
        {
            var halfWidth = centered ? baseWidth / 2 : 0;
            var halfDepth = centered ? baseDepth / 2 : 0;

            Vector3 v1 = new Vector3(-halfWidth, 0, -halfDepth) + offset;  // Base front left
            Vector3 v2 = new Vector3(baseWidth - halfWidth, 0, -halfDepth) + offset;   // Base front right
            Vector3 v3 = new Vector3(baseWidth - halfWidth, 0, baseDepth - halfDepth) + offset;    // Base back right
            Vector3 v4 = new Vector3(-halfWidth, 0, baseDepth - halfDepth) + offset;   // Base back left
            Vector3 vApex = new Vector3(0 - halfWidth, height, 0 - halfDepth) + offset;  // Apex of the pyramid

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
