using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeightmapVisualizer.Units
{
	public class Vector3
	{
		public float x {  get; set; }
		public float y { get; set; }
		public float z { get; set; }

		public Vector3() : this(0, 0, 0) { }

		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		#region Operators

		public static Vector3 operator+ (Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
		}

		public static Vector3 operator- (Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
		}

		public static Vector3 operator* (Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
		}

		public static Vector3 operator *(Vector3 v1, float s)
		{
			return new Vector3(v1.x * s, v1.y * s, v1.z * s);
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
		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			Vector3 v = (Vector3)obj;
			return AreApproximatelyEqual(x, v.x) &&
				   AreApproximatelyEqual(y, v.y) &&
				   AreApproximatelyEqual(z, v.z);
		}

		// Override GetHashCode method (hashcode may not reflect epsilon logic)
		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
		}

		public override string ToString()
		{
			return $"({x}, {y}, {z})";
		}

		#endregion

	}
}
