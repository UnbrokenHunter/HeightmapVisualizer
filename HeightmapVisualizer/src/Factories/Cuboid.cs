﻿using HeightmapVisualizer.src.Components;
using HeightmapVisualizer.src.Primitives;
using System.Drawing;
using System.Numerics;
using static HeightmapVisualizer.src.Components.MeshComponent;

namespace HeightmapVisualizer.src.Factories
{
    /// <summary>
    /// Factory class for creating a cuboid shape.
    /// </summary>
    public static class Cuboid
    {  

        /// <summary>
        /// Creates a cuboid mesh with the specified position and size, defaulting to no rotation (Quaternion.Identity), where the position is treated as the center of the cuboid.
        /// </summary>
        /// <param name="position">The center position of the cuboid in the scene.</param>
        /// <param name="size">The size of the cuboid (width, height, depth).</param>
        /// <param name="color">The color of the object. Defaults to black</param>
        /// <returns>A <see cref="Mesh"/> object representing the cuboid.</returns>
        public static MeshComponent CreateCentered(Vector3 size)
        {
            var faces = CreateCuboidFaces(size.X, size.Y, size.Z, true);
            var mesh = new MeshComponent(faces);
            return mesh;
        }

        /// <summary>
        /// Creates a cuboid mesh with the specified position and size, defaulting to no rotation (Quaternion.Identity), where the position is treated as one corner of the cuboid.
        /// </summary>
        /// <param name="position">The corner position of the cuboid in the scene.</param>
        /// <param name="size">The size of the cuboid (width, height, depth).</param>
        /// <param name="color">The color of the object. Defaults to black</param>
        /// <returns>A <see cref="Mesh"/> object representing the cuboid.</returns>
        public static MeshComponent CreateCorners(Vector3 size)
        {
            var faces = CreateCuboidFaces(size.X, size.Y, size.Z, false);
            var mesh = new MeshComponent(faces);
            return mesh;
        }

        /// <summary>
        /// Helper method that creates the six faces of the cuboid, with an option to center the vertices or not.
        /// </summary>
        private static Face[] CreateCuboidFaces(float width, float height, float depth, bool centered)
        {
            var halfWidth = centered ? width / 2 : 0;
            var halfHeight = centered ? height / 2 : 0;
            var halfDepth = centered ? depth / 2 : 0;

            Vector3 v1 = new Vector3(-halfWidth, -halfHeight, -halfDepth);
            Vector3 v2 = new Vector3(width - halfWidth, -halfHeight, -halfDepth);
            Vector3 v3 = new Vector3(width - halfWidth, height - halfHeight, -halfDepth);
            Vector3 v4 = new Vector3(-halfWidth, height - halfHeight, -halfDepth);
            Vector3 v5 = new Vector3(-halfWidth, -halfHeight, depth - halfDepth);
            Vector3 v6 = new Vector3(width - halfWidth, -halfHeight, depth - halfDepth);
            Vector3 v7 = new Vector3(width - halfWidth, height - halfHeight, depth - halfDepth);
            Vector3 v8 = new Vector3(-halfWidth, height - halfHeight, depth - halfDepth);

            return new Face[]
            {
                new Face(new[] { v1, v2, v3, v4 }, name: "Front"), // Front face // THESE COMMENTS WERE THE ORIGINAL LABELS! MAY NO LONGER BE ACCURATE!
                new Face(new[] { v5, v6, v7, v8 }, name: "Back"), // Back face
                new Face(new[] { v1, v5, v8, v4 }, name: "Left"), // Left face
                new Face(new[] { v2, v6, v7, v3 }, name: "Right"), // Right face
                new Face(new[] { v4, v3, v7, v8 }, name: "Top"), // Top face
                new Face(new[] { v1, v2, v6, v5 }, name: "Bottom")  // Bottom face
            };
        }
    }
}
