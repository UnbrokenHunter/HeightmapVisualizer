using System.Numerics;
using System.Runtime.CompilerServices;

namespace HeightmapVisualizer.Units
{
    public class Vector3 : IEquatable<Vector3>
    {

        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public static Vector3 Zero = new Vector3(0, 0, 0);
        public static Vector3 Forward = new Vector3(0, 0, 1);
        public static Vector3 Up = new Vector3(0, 1, 0);
        public static Vector3 Right = new Vector3(1, 0, 0);

        public Vector3() : this(0, 0, 0) { }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        #region Operators

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static Vector3 operator *(Vector3 v1, float s)
        {
            return new Vector3(v1.x * s, v1.y * s, v1.z * s);
        }

        public static Vector3 operator /(Vector3 v1, float s)
        {
            return new Vector3(v1.x / s, v1.y / s, v1.z / s);
        }

        // Epsilon value for comparison
        private const float Epsilon = 0.01f;

        // Method to compare two floats within an epsilon
        private static bool AreApproximatelyEqual(float a, float b, float epsilon = Epsilon)
        {
            return Math.Abs(a - b) < epsilon;
        }

        // Override the == operator
        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            if (ReferenceEquals(v1, v2))
            {
                return true;
            }

            if (v1 is null || v2 is null)
            {
                return false;
            }

            // Compare each component using the epsilon
            return AreApproximatelyEqual(v1.x, v2.x) &&
                   AreApproximatelyEqual(v1.y, v2.y) &&
                   AreApproximatelyEqual(v1.z, v2.z);
        }

        // Override the != operator
        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            return !(v1 == v2);
        }

        // Override Equals method
        public override bool Equals(object? obj)
        {
            return obj is Vector3 other && Equals(other);
        }

        /// <summary>
        /// Checks if the current Vector3 is equal to another Vector3.
        /// </summary>
        /// <param name="other">The Vector3 to compare with the current Vector3.</param>
        /// <returns>True if the specified Vector3 is equal to the current Vector3; otherwise, false.</returns>
        public bool Equals(Vector3? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return AreApproximatelyEqual(x, other.x) &&
                   AreApproximatelyEqual(y, other.y) &&
                   AreApproximatelyEqual(z, other.z);
        }

        // Override GetHashCode method
        public override int GetHashCode()
        {
            // Note: Consider documenting that hashcode doesn't account for epsilon precision.
            return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        }

        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }

        #endregion

        /// <summary>Returns the dot product of two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The dot product.</returns>
        public static float Dot(Vector3 vector1, Vector3 vector2)
        {
            return (vector1.x * vector2.x)
                 + (vector1.y * vector2.y)
                 + (vector1.z * vector2.z);
        }

        /// <summary>Computes the cross product of two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The cross product.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Cross(Vector3 vector1, Vector3 vector2)
        {
            return new Vector3(
                (vector1.y * vector2.z) - (vector1.z * vector2.y),
                (vector1.z * vector2.x) - (vector1.x * vector2.z),
                (vector1.x * vector2.y) - (vector1.y * vector2.x)
            );
        }

        /// <summary>Returns a vector with the same direction as the specified vector, but with a length of one.</summary>
        /// <param name="value">The vector to normalize.</param>
        /// <returns>The normalized vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Normalize(Vector3 value)
        {
            return value / value.Length();
        }

        /// <summary>Returns the reflection of a vector off a surface that has the specified normal.</summary>
        /// <param name="vector">The source vector.</param>
        /// <param name="normal">The normal of the surface being reflected off.</param>
        /// <returns>The reflected vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Reflect(Vector3 vector, Vector3 normal)
        {
            float dot = Dot(vector, normal);
            return vector - ((normal * dot) * 2.0f);
        }

        /// <summary>Returns the length of this vector object.</summary>
        /// <returns>The vector's length.</returns>
        /// <altmember cref="LengthSquared"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Length()
        {
            float lengthSquared = LengthSquared();
            return MathF.Sqrt(lengthSquared);
        }

        /// <summary>Returns the length of the vector squared.</summary>
        /// <returns>The vector's length squared.</returns>
        /// <remarks>This operation offers better performance than a call to the <see cref="Length" /> method.</remarks>
        /// <altmember cref="Length"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float LengthSquared()
        {
            return Dot(this, this);
        }

        /// <summary>Performs a linear interpolation between two vectors based on the given weighting.</summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">A value between 0 and 1 that indicates the weight of <paramref name="value2" />.</param>
        /// <returns>The interpolated vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Lerp(Vector3 value1, Vector3 value2, float amount)
        {
            return (value1 * (1.0f - amount)) + (value2 * amount);
        }
    }
}
