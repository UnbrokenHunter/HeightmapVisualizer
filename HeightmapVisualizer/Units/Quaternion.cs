
namespace HeightmapVisualizer.Units
{
	public class Quaternion
	{

		public float x {  get; set; }
		public float y { get; set; }
		public float z { get; set; }
		public float w { get; set; }

		public Quaternion() : this(0, 0, 0, 0) { }

		public Quaternion(float w, float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		#region Formatting Conversions

		public static Vector3 ToEulerAngles(Quaternion q)
		{
			var roll = Math.Atan2(2 * (q.x * q.y + q.z * q.w), Math.Pow(q.x, 2) - Math.Pow(q.y, 2) - Math.Pow(q.z, 2) + Math.Pow(q.w, 2));

			var pitch = Math.Asin(2 * (q.x * q.z - q.y * q.w));

			var yaw = Math.Atan2(2 * (q.x * q.w), Math.Pow(q.x, 2) + Math.Pow(q.y, 2) - Math.Pow(q.z, 2) - Math.Pow(q.w, 2));

			return new Vector3((float) roll, (float) pitch, (float) yaw);
		}

		public static Quaternion ToQuaternion(Vector3 e)
		{
			double cy = Math.Cos(e.z * 0.5);
			double sy = Math.Sin(e.z * 0.5);
			double cp = Math.Cos(e.y * 0.5);
			double sp = Math.Sin(e.y * 0.5);
			double cr = Math.Cos(e.x * 0.5);
			double sr = Math.Sin(e.x * 0.5);

			var w = cr * cp * cy + sr * sp * sy;
			var x = sr * cp * cy - cr * sp * sy;
			var y = cr * sp * cy + sr * cp * sy;
			var z = cr * cp * sy - sr * sp * cy;

			return new Quaternion((float) w, (float) x, (float) y, (float) z);
		}

		#endregion

		#region Operations

		public static Quaternion ToInverse(Quaternion q)
		{
			return new Quaternion(q.w, -q.x, -q.y, -q.z);
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

		public static Quaternion operator *(Quaternion r, Quaternion s)
		{
			var t0 = (r.w * s.w) - (r.x + s.x) - (r.y * s.y) - (r.z * s.z);

			var t1 = (r.w * s.x) + (r.x + s.w) - (r.y * s.z) + (r.z + s.y); 

			var t2 = (r.w * s.y) + (r.x + s.w) + (r.y * s.w) - (r.z * s.x);

			var t3 = (r.w * s.z) - (r.x * s.y) + (r.y * s.x) + (r.z * s.w);

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
			Quaternion p = PointToQuaternion(p1);
			Quaternion q2 = ToInverse(q1);

			Quaternion rotation = q1 * p * q2;

			if (rotation.w != 0)
			{
				Console.WriteLine($"Quaternion Rotation resulted in w = {rotation.w} instead of 0. {q1} {p} {q2}");
			}

			return new Vector3(rotation.x, rotation.y, rotation.z);
		}

		#endregion

	}
}
