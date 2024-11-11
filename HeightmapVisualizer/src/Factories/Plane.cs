using HeightmapVisualizer.src.Components;
using HeightmapVisualizer.src.Primitives;
using System.Numerics;
using static HeightmapVisualizer.src.Components.MeshComponent;

namespace HeightmapVisualizer.src.Factories
{
    /// <summary>
    /// Factory class for creating a plane shape.
    /// </summary>
    internal static class Plane
    {
        /// <summary>
        /// Creates a plane mesh with the specified position and size, defaulting to no rotation (Quaternion.Identity), where the position is treated as the center of the plane.
        /// </summary>
        /// <param name="size">The size of the plane (width and depth).</param>
        /// <returns>A <see cref="MeshComponent"/> object representing the plane.</returns>
        public static MeshComponent CreateCentered(Vector2 size, Vector3? position = null)
        {
            var offset = position ?? Vector3.Zero;

            // Create the faces of the plane based on size, using the position as the center
            var faces = CreatePlaneFaces(size.X, size.Y, true, offset);

            // Create the mesh with the faces
            var mesh = new MeshComponent(faces);

            return mesh;
        }

        /// <summary>
        /// Creates a plane mesh with the specified position and size, defaulting to no rotation (Quaternion.Identity), where the position is treated as one corner of the plane.
        /// </summary>
        /// <param name="size">The size of the plane (width and depth).</param>
        /// <returns>A <see cref="MeshComponent"/> object representing the plane.</returns>
        public static MeshComponent CreateCorners(Vector2 size, Vector3? position = null)
        {
            var offset = position ?? Vector3.Zero;

            // Create the faces of the plane based on size, using the position as one of the corners
            var faces = CreatePlaneFaces(size.X, size.Y, false, offset);

            // Create the mesh with the faces
            var mesh = new MeshComponent(faces);

            return mesh;
        }

        /// <summary>
        /// Helper method that creates the two triangular faces of the plane, with an option to center the vertices or not.
        /// </summary>
        /// <param name="width">The width of the plane.</param>
        /// <param name="depth">The depth of the plane.</param>
        /// <param name="centered">Whether to treat the position as the center of the plane.</param>
        /// <returns>An array of <see cref="Face"/> objects representing the plane's triangles.</returns>
        private static Face[] CreatePlaneFaces(float width, float depth, bool centered, Vector3 offset)
        {
            var halfWidth = centered ? width / 2 : 0;
            var halfDepth = centered ? depth / 2 : 0;

            // Define the four vertices of the plane
            Vector3 v1 = new Vector3(-halfWidth, 0, -halfDepth) + offset; // Front-left
            Vector3 v2 = new Vector3(width - halfWidth, 0, -halfDepth) + offset; // Front-right
            Vector3 v3 = new Vector3(width - halfWidth, 0, depth - halfDepth) + offset; // Back-right
            Vector3 v4 = new Vector3(-halfWidth, 0, depth - halfDepth) + offset; // Back-left

            // Create the two triangular faces of the plane
            return new Face[]
            {
                new Face(new[] { v1, v2, v3, v4 }, name: "Plane"), // First triangle (front-right)
            };
        }
    }
}
