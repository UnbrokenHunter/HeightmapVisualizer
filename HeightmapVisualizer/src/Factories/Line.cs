﻿using HeightmapVisualizer.src.Components;
using HeightmapVisualizer.src.Primitives;
using System.Numerics;
using static HeightmapVisualizer.src.Components.MeshComponent;

namespace HeightmapVisualizer.src.Factories
{
    /// <summary>
    /// Factory class for creating a line.
    /// </summary>
    internal static class Line
    {
        /// <summary>
        /// Creates a line mesh between two points in 3D space.
        /// </summary>
        /// <param name="start">The starting point of the line.</param>
        /// <param name="end">The ending point of the line.</param>
        /// <param name="color">The color of the object. Defaults to black</param>
        /// <returns>A <see cref="Mesh"/> object representing the line between the two points.</returns>
        public static Face[] CreateCorners(Vector3 start, Vector3 end)
        {
            // Create the edge representing the line between the start and end points
            return new Face[] { new Face(new[] { start, end }) };
        }

        /// <summary>
        /// Creates a line mesh starting from a given position, extending in the direction of the vector, for the specified length.
        /// </summary>
        /// <param name="position">The starting position of the line.</param>
        /// <param name="direction">The direction vector in which the line extends.</param>
        /// <param name="length">The length of the line.</param>
        /// <param name="color">The color of the object. Defaults to black</param>
        /// <returns>A <see cref="Mesh"/> object representing the ray-like line.</returns>
        public static Face[] CreateRay(Vector3 direction, float length)
        {
            // Normalize the direction vector to ensure correct scaling
            Vector3 normalizedDirection = Vector3.Normalize(direction);

            // Calculate the end point of the line based on the direction and length
            Vector3 endPoint = normalizedDirection * length;

            // Create the edge representing the line in the given direction and length
            return new Face[] { new Face(new[] { Vector3.Zero, endPoint }) };
        }
    }
}
