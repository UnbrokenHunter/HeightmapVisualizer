
namespace HeightmapVisualizer.Units
{
	public class Quaternion
	{

		public float x {  get; set; }
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


		#region Operator Overloading for Equality

		// Epsilon for approximate equality
		private const float Epsilon = 0.0001f;

		// Helper method to compare two floats with epsilon
		private static bool AreApproximatelyEqual(float a, float b, float epsilon = Epsilon)
		{
			return Math.Abs(a - b) < epsilon;
		}

		public static bool operator ==(Quaternion q1, Quaternion q2)
		{
			if (ReferenceEquals(q1, q2))
				return true;

			if (q1 is null || q2 is null)
				return false;

			// Compare using epsilon
			return AreApproximatelyEqual(q1.w, q2.w) &&
				   AreApproximatelyEqual(q1.x, q2.x) &&
				   AreApproximatelyEqual(q1.y, q2.y) &&
				   AreApproximatelyEqual(q1.z, q2.z);
		}

		public static bool operator !=(Quaternion q1, Quaternion q2)
		{
			return !(q1 == q2);
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			Quaternion q = (Quaternion)obj;
			return this == q; // Uses the overridden == operator
		}

		public override int GetHashCode()
		{
			// This doesn't consider epsilon, keep in mind this may cause hash-based collection issues
			return w.GetHashCode() ^ x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
		}

		public override string ToString()
		{
			return $"({w},{x}, {y}, {z})";
		}

		#endregion

		#region Formatting Conversions

		public static Vector3 ToEulerAngles(Quaternion q)
		{
			Vector3 angles = new Vector3(0, 0, 0);

			// Roll (X-axis rotation)
			double sinr_cosp = 2 * (q.w * q.x + q.y * q.z);
			double cosr_cosp = 1 - 2 * (q.x * q.x + q.y * q.y);
			angles.x = (float)Math.Atan2(sinr_cosp, cosr_cosp);

			// Pitch (Y-axis rotation)
			double sinp = Math.Sqrt(1 + 2 * (q.w * q.y - q.x * q.z));
			double cosp = Math.Sqrt(1 - 2 * (q.w * q.y - q.x * q.z));
			angles.y = (float)(2 * Math.Atan2(sinp, cosp) - Math.PI / 2);

			// Yaw (Z-axis rotation)
			double siny_cosp = 2 * (q.w * q.z + q.x * q.y);
			double cosy_cosp = 1 - 2 * (q.y * q.y + q.z * q.z);
			angles.z = (float)Math.Atan2(siny_cosp, cosy_cosp);

			return angles;
		}

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

		#endregion

		#region Operations

		public static Quaternion Identity()
		{
			return new Quaternion(1, 0, 0, 0);
		}

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

		public static Quaternion Normalize(Quaternion q)
		{
			var l = 1/ Length(q);
			return new Quaternion(q.w * l, q.x * l, q.y * l, q.z * l);
		}

		public static float Length(Quaternion q)
		{
			return (float)Math.Sqrt(q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w);
		}

		public static float LengthSquared(Quaternion q)
		{
			return (float)(q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w);
		}

		public static Quaternion operator +(Quaternion q1, Quaternion q2)
		{
			return new Quaternion(
				q1.w + q2.w,
				q1.x + q2.x,
				q1.y + q2.y,
				q2.z + q2.z
			);
		}

		public static Quaternion operator -(Quaternion q1, Quaternion q2)
		{
			return new Quaternion(
				q1.w - q2.w,
				q1.x - q2.x,
				q1.y - q2.y,
				q2.z - q2.z
			);
		}

		public static Quaternion operator *(Quaternion q, Quaternion r)
		{
			var t0 = (r.w * q.w) - (r.x * q.x) - (r.y * q.y) - (r.z * q.z);   // t0 corresponds to w
			var t1 = (r.w * q.x) + (r.x * q.w) - (r.y * q.z) + (r.z * q.y);   // t1 corresponds to x
			var t2 = (r.w * q.y) + (r.x * q.z) + (r.y * q.w) - (r.z * q.x);   // t2 corresponds to y
			var t3 = (r.w * q.z) - (r.x * q.y) + (r.y * q.x) + (r.z * q.w);   // t3 corresponds to z

			return new Quaternion(t0, t1, t2, t3);
		}

		private static Quaternion PointToQuaternion(Vector3 v) // For Rotatating A Point
		{
			return new Quaternion(0, v.x, v.y, v.z);
		}

		#endregion

		#region Rotation

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

		#endregion

	}
}
