
using System.Runtime.CompilerServices;

namespace HeightmapVisualizer.Units
{
    public class Quaternion : IEquatable<Quaternion>
    {

        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float w { get; set; }

        public Quaternion() : this(1, 0, 0, 0) { }

        public Quaternion(float w, float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        // Epsilon for approximate equality
        private const float Epsilon = 0.0001f;

        #region Equality Comparison

        /// <summary>
        /// Helper method to compare two floats with a specified epsilon value.
        /// </summary>
        /// <param name="a">The first float value.</param>
        /// <param name="b">The second float value.</param>
        /// <param name="epsilon">The allowed difference for approximate equality.</param>
        /// <returns>True if the two floats are approximately equal; otherwise, false.</returns>
        private static bool AreApproximatelyEqual(float a, float b, float epsilon = Epsilon)
        {
            return Math.Abs(a - b) < epsilon;
        }

        /// <summary>
        /// Overloaded equality operator to check if two quaternions are approximately equal.
        /// </summary>
        /// <param name="q1">The first quaternion.</param>
        /// <param name="q2">The second quaternion.</param>
        /// <returns>True if the quaternions are approximately equal; otherwise, false.</returns>
        public static bool operator ==(Quaternion q1, Quaternion q2)
        {
            if (ReferenceEquals(q1, q2))
                return true;

            if (q1 is null || q2 is null)
                return false;

            // Compare quaternions using epsilon for approximate equality
            return AreApproximatelyEqual(q1.w, q2.w) &&
                   AreApproximatelyEqual(q1.x, q2.x) &&
                   AreApproximatelyEqual(q1.y, q2.y) &&
                   AreApproximatelyEqual(q1.z, q2.z);
        }

        /// <summary>
        /// Overloaded inequality operator to check if two quaternions are not equal.
        /// </summary>
        /// <param name="q1">The first quaternion.</param>
        /// <param name="q2">The second quaternion.</param>
        /// <returns>True if the quaternions are not equal; otherwise, false.</returns>
        public static bool operator !=(Quaternion q1, Quaternion q2)
        {
            return !(q1 == q2);
        }

        /// <summary>
        /// Checks if the current quaternion is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare with the current quaternion.</param>
        /// <returns>True if the object is equal to the current quaternion; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            return obj is Quaternion other && Equals(other);
        }

        /// <summary>
        /// Checks if the current quaternion is equal to another quaternion.
        /// </summary>
        /// <param name="other">The quaternion to compare with the current quaternion.</param>
        /// <returns>True if the specified quaternion is equal to the current quaternion; otherwise, false.</returns>
        public bool Equals(Quaternion? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            // Compare quaternions using epsilon for approximate equality
            return AreApproximatelyEqual(w, other.w) &&
                   AreApproximatelyEqual(x, other.x) &&
                   AreApproximatelyEqual(y, other.y) &&
                   AreApproximatelyEqual(z, other.z);
        }

        /// <summary>
        /// Generates a hash code for the quaternion based on its components.
        /// Note: This hash code calculation does not account for epsilon, which might cause issues
        /// in hash-based collections like dictionaries.
        /// </summary>
        /// <returns>A hash code for the current quaternion.</returns>
        public override int GetHashCode()
        {
            // Combine hash codes of each component
            return w.GetHashCode() ^ x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns a string that represents the current quaternion.
        /// </summary>
        /// <returns>A string that represents the quaternion.</returns>
        public override string ToString()
        {
            return $"Quaternion: ({w}, {x}, {y}, {z})";
        }

        #endregion

        #region Formatting Conversions

        /// <summary>
        /// Converts a Quaternion into a Vector3 that contains the Euler Angles equivalent to the Quaternion.
        /// </summary>
        /// <param name="q">The Quaternion to convert</param>
        /// <returns>A Vector3 that contains (Roll, Pitch, Yaw)</returns>
        public static Vector3 ToEulerAngles(Quaternion q)
        {

            // Roll (X-axis rotation)
            double sinr_cosp = 2 * (q.w * q.x + q.y * q.z);
            double cosr_cosp = 1 - 2 * (q.x * q.x + q.y * q.y);
            float roll = (float)Math.Atan2(sinr_cosp, cosr_cosp);

            // Pitch (Y-axis rotation)
            double sinp = 2 * (q.w * q.y - q.z * q.x); // Possible Fix: Use 2 * (q.w * q.y - q.z * q.x) instead of the square root
            float pitch;
            if (Math.Abs(sinp) >= 1)
                pitch = (float)Math.CopySign(Math.PI / 2, sinp); // Clamp to 90 degrees if out of bounds
            else
                pitch = (float)Math.Asin(sinp); // Possible Fix: Use Math.Asin instead of Atan2 to avoid incorrect calculation

            // Yaw (Z-axis rotation)
            double siny_cosp = 2 * (q.w * q.z + q.x * q.y);
            double cosy_cosp = 1 - 2 * (q.y * q.y + q.z * q.z);
            float yaw = (float)Math.Atan2(siny_cosp, cosy_cosp);

            return new Vector3(roll, pitch, yaw);
        }

        /// <summary>
        /// Converts Euler angles (roll, pitch, yaw) into a Quaternion.
        /// </summary>
        /// <param name="e">The Vector3 containing the Euler angles (roll, pitch, yaw)</param>
        /// <returns>A Quaternion representing the rotation</returns>
        public static Quaternion ToQuaternion(Vector3 e)
        {
            // Convert Euler angles (roll, pitch, yaw) to quaternion
            double cy = Math.Cos(e.z * 0.5); // Yaw
            double sy = Math.Sin(e.z * 0.5);
            double cp = Math.Cos(e.y * 0.5); // Pitch
            double sp = Math.Sin(e.y * 0.5);
            double cr = Math.Cos(e.x * 0.5); // Roll
            double sr = Math.Sin(e.x * 0.5);

            var w = cr * cp * cy + sr * sp * sy;
            var x = sr * cp * cy - cr * sp * sy;
            var y = cr * sp * cy + sr * cp * sy;
            var z = cr * cp * sy - sr * sp * cy;

            return new Quaternion((float)w, (float)x, (float)y, (float)z);
        }

        /// <summary>
        /// Creates a Quaternion from a unit axis and an angle of rotation around that axis.
        /// </summary>
        /// <param name="axis">A Vector3 representing the axis of rotation</param>
        /// <param name="angle">The angle of rotation in radians</param>
        /// <param name="inDegrees">Whether the angle should be interpreted as a radian or a degree</param>
        /// <returns>A Quaternion representing the rotation around the axis</returns>
        public static Quaternion CreateFromAxisAngle(Vector3 axis, float angle, bool inDegrees = true)
        {
            Vector3 normalizedAxis = Vector3.Normalize(axis);

            float convertedAngle;
            if (inDegrees)
                convertedAngle = (float)(angle * Math.PI) / 180;

            else
                convertedAngle = angle;

            float halfAngle = convertedAngle * 0.5f;
            float sinHalfAngle = (float)Math.Sin(halfAngle);

            float qx = normalizedAxis.x * sinHalfAngle;
            float qy = normalizedAxis.y * sinHalfAngle;
            float qz = normalizedAxis.z * sinHalfAngle;
            float qw = (float)Math.Cos(halfAngle);

            Quaternion q = new Quaternion(qw, qx, qy, qz);
            return q;
        }

        #endregion

        #region Operations

        /// <summary>
        /// Gets the identity quaternion (no rotation), which has components (1, 0, 0, 0).
        /// </summary>
        public static Quaternion Identity => new Quaternion(1, 0, 0, 0);

        /// <summary>
        /// Calculates the inverse of the quaternion, which reverses the rotation.
        /// </summary>
        /// <param name="q">The quaternion to invert.</param>
        /// <returns>The inverse of the quaternion.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the quaternion has zero magnitude.</exception>
        public static Quaternion ToInverse(Quaternion q)
        {
            float lengthSquared = LengthSquared(q);
            if (lengthSquared > 0.0f)
            {
                float l = 1 / lengthSquared;
                return new Quaternion(q.w * l, -q.x * l, -q.y * l, -q.z * l);
            }
            throw new InvalidOperationException("Cannot invert a quaternion with zero magnitude.");
        }

        /// <summary>
        /// Normalizes the quaternion to ensure its magnitude is 1 (unit quaternion).
        /// </summary>
        /// <param name="q">The quaternion to normalize.</param>
        /// <returns>The normalized quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Normalize(Quaternion q)
        {
            var l = 1 / Length(q);
            return new Quaternion(q.w * l, q.x * l, q.y * l, q.z * l);
        }

        /// <summary>
        /// Calculates the magnitude (length) of the quaternion.
        /// </summary>
        /// <param name="q">The quaternion to evaluate.</param>
        /// <returns>The magnitude of the quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Length(Quaternion q)
        {
            return (float)Math.Sqrt(q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w);
        }

        /// <summary>
        /// Calculates the squared magnitude of the quaternion.
        /// </summary>
        /// <param name="q">The quaternion to evaluate.</param>
        /// <returns>The squared magnitude of the quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float LengthSquared(Quaternion q)
        {
            return (float)(q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w);
        }

        /// <summary>
        /// Adds two quaternions together, component-wise.
        /// </summary>
        /// <param name="q1">The first quaternion.</param>
        /// <param name="q2">The second quaternion.</param>
        /// <returns>The sum of the two quaternions.</returns>
        public static Quaternion operator +(Quaternion q1, Quaternion q2)
        {
            return new Quaternion(
                q1.w + q2.w,
                q1.x + q2.x,
                q1.y + q2.y,
                q2.z + q2.z
            );
        }

        /// <summary>
        /// Subtracts the second quaternion from the first, component-wise.
        /// </summary>
        /// <param name="q1">The first quaternion.</param>
        /// <param name="q2">The second quaternion.</param>
        /// <returns>The result of subtracting the second quaternion from the first.</returns>
        public static Quaternion operator -(Quaternion q1, Quaternion q2)
        {
            return new Quaternion(
                q1.w - q2.w,
                q1.x - q2.x,
                q1.y - q2.y,
                q2.z - q2.z
            );
        }

        /// <summary>
        /// Multiplies two quaternions to combine their rotations.
        /// </summary>
        /// <param name="q">The first quaternion.</param>
        /// <param name="r">The second quaternion.</param>
        /// <returns>The result of multiplying the two quaternions.</returns>
        public static Quaternion operator *(Quaternion q, Quaternion r)
        {
            var t0 = (r.w * q.w) - (r.x * q.x) - (r.y * q.y) - (r.z * q.z);   // t0 corresponds to w
            var t1 = (r.w * q.x) + (r.x * q.w) - (r.y * q.z) + (r.z * q.y);   // t1 corresponds to x
            var t2 = (r.w * q.y) + (r.x * q.z) + (r.y * q.w) - (r.z * q.x);   // t2 corresponds to y
            var t3 = (r.w * q.z) - (r.x * q.y) + (r.y * q.x) + (r.z * q.w);   // t3 corresponds to z

            return new Quaternion(t0, t1, t2, t3);
        }

        /// <summary>
        /// Converts a vector point to a quaternion. Used for point rotation.
        /// </summary>
        /// <param name="v">The vector point to convert.</param>
        /// <returns>A quaternion representing the vector.</returns>
        private static Quaternion PointToQuaternion(Vector3 v)
        {
            return new Quaternion(0, v.x, v.y, v.z);
        }

        #endregion

        #region Rotation

        /// <summary>
        /// Rotates a point using a quaternion.
        /// </summary>
        /// <param name="p1">The point to rotate.</param>
        /// <param name="q1">The quaternion representing the rotation.</param>
        /// <returns>The rotated point as a Vector3.</returns>
        public static Vector3 Rotate(Vector3 p1, Quaternion q1)
        {
            q1 = Normalize(q1); // Ensure q1 is a unit quaternion

            Quaternion p = PointToQuaternion(p1);
            Quaternion qInverse = ToInverse(q1);

            // Correct multiplication order for passive rotation: q * p * q^(-1)
            Quaternion rotation = qInverse * p * q1; // This is active rotation

            if (float.IsNaN(rotation.w) || Math.Round(rotation.w, 3) != 0)
            {
                Console.WriteLine($"Quaternion Rotation resulted in w = {rotation.w} instead of 0. {q1} {p} {qInverse}");
            }

            return new Vector3(rotation.x, rotation.y, rotation.z);
        }

        /// <summary>
        /// Evaluates a rotation needed to be applied to an object positioned at sourcePoint to face destPoint
        /// </summary>
        /// <param name="sourcePoint">Coordinates of source point</param>
        /// <param name="destPoint">Coordinates of destionation point</param>
        /// <returns></returns>
        public static Quaternion LookAt(Vector3 sourcePoint, Vector3 destPoint)
        {
            Vector3 forwardVector = Vector3.Normalize(destPoint - sourcePoint);

            float dot = Vector3.Dot(Vector3.Forward, forwardVector);

            if (Math.Abs(dot - (-1.0f)) < 0.000001f)
            {
                return new Quaternion(Vector3.Up.x, Vector3.Up.y, Vector3.Up.z, 3.1415926535897932f);
            }
            if (Math.Abs(dot - (1.0f)) < 0.000001f)
            {
                return Quaternion.Identity;
            }

            float rotAngle = (float)Math.Acos(dot); // In Deg
            Vector3 rotAxis = Vector3.Cross(Vector3.Forward, forwardVector);
            rotAxis = Vector3.Normalize(rotAxis);
            return CreateFromAxisAngle(rotAxis, rotAngle, false);
        }

        #endregion

    }
}
