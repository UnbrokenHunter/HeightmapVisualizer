
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

		#region Rotation

		public static Quaternion Rotate(Quaternion q1, Quaternion q2)
		{
			throw new NotImplementedException();
		}

		public static Quaternion Rotate(Quaternion q1, Vector3 e1)
		{
			throw new NotImplementedException();
		}

		#endregion

	}
}
