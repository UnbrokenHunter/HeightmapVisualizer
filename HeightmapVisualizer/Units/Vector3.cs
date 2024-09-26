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

		public override string ToString()
		{
			return $"({x}, {y}, {z})";
		}

	}
}
