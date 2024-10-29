using System.Numerics;

namespace HeightmapVisualizer.src.Utilities
{
    public class Transform
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        public Vector3 Forward => Rotate(Vector3.UnitZ, Rotation);
        public Vector3 Up => Rotate(Vector3.UnitY, Rotation);
        public Vector3 Right => Rotate(Vector3.UnitX, Rotation);

        public Transform() : this(Vector3.Zero, Quaternion.Identity) { }

        public Transform(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        public override string ToString()
        {
            return $"{GetHashCode()},\n\tPosition: {Position},\n\tRotation: {Rotation},\n\tForward: {Forward}";
        }

        public Vector3 ToWorldSpace(Vector3 v) => Right * v.X + Up * v.Y + Forward * v.Z;

        public Vector3 ToLocalSpace(Vector3 v, bool affectPosition = false) => Rotate(v, Rotation) + (affectPosition ? Position : Vector3.Zero);

        #region Movement

        public void Move(Vector3 vector)
        {
            Position += Rotate(vector, Quaternion.Inverse(Rotation));
        }

        /// <summary>
        /// Rotates a point using a quaternion.
        /// </summary>
        /// <param name="p1">The point to rotate.</param>
        /// <param name="q1">The quaternion representing the rotation.</param>
        /// <returns>The rotated point as a Vector3.</returns>
        public static Vector3 Rotate(Vector3 p1, Quaternion q1)
        {
            q1 = Quaternion.Normalize(q1); // Ensure q1 is a unit quaternion

            Quaternion p = new Quaternion(p1.X, p1.Y, p1.Z, 0);
            Quaternion qInverse = Quaternion.Inverse(q1);

            // Correct multiplication order for passive rotation: q * p * q^(-1)
            Quaternion rotation = qInverse * p * q1; // This is active rotation

            if (float.IsNaN(rotation.W) || Math.Round(rotation.W, 3) != 0)
            {
                Console.WriteLine($"Quaternion Rotation resulted in w = {rotation.W} instead of 0. {q1} {p} {qInverse}");
            }

            return new Vector3(rotation.X, rotation.Y, rotation.Z);
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

            float dot = Vector3.Dot(Vector3.UnitZ, forwardVector);

            if (Math.Abs(dot - (-1.0f)) < 0.000001f)
            {
                return new Quaternion(3.1415926535897932f, Vector3.UnitY.X, Vector3.UnitY.Y, Vector3.UnitY.Z); // THIS MAY BE WRONG AFTER FLIPPING W AND Z
            }
            if (Math.Abs(dot - 1.0f) < 0.000001f)
            {
                return Quaternion.Identity;
            }

            float rotAngle = (float)Math.Acos(dot); // In Deg
            Vector3 rotAxis = Vector3.Cross(Vector3.UnitZ, forwardVector);
            rotAxis = Vector3.Normalize(rotAxis);
            return Quaternion.CreateFromAxisAngle(rotAxis, rotAngle * 0.01745329f); // 0.01745329f is pi/180
        }

        #endregion

    }
}
