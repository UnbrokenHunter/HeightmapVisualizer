using System.Numerics;

namespace HeightmapVisualizer.Utilities
{
    public static class Numerics_Extentions
    {

        public static Quaternion CreateQuaternionFromYawPitchRoll(this Vector3 yawPitchRoll)
        {
            return Quaternion.CreateFromYawPitchRoll(yawPitchRoll.X, yawPitchRoll.Y, yawPitchRoll.Z); 
        }


        /// <summary>
        /// Converts a Quaternion into a Vector3 that contains the Euler Angles equivalent to the Quaternion.
        /// </summary>
        /// <param name="q">The Quaternion to convert</param>
        /// <returns>A Vector3 that contains (Roll, Pitch, Yaw)</returns>
        public static Vector3 ToYawPitchRoll(this Quaternion q1)
        {
            double test = q1.X * q1.Y + q1.Z * q1.W;
            double heading, attitude, bank;

            if (test > 0.499) // singularity at north pole
            {
                heading = 2 * Math.Atan2(q1.X, q1.W);
                attitude = Math.PI / 2;
                bank = 0;
                return new Vector3((float)heading, (float)bank, (float)attitude);
            }

            if (test < -0.499) // singularity at south pole
            {
                heading = -2 * Math.Atan2(q1.X, q1.W);
                attitude = -Math.PI / 2;
                bank = 0;
                return new Vector3((float)heading, (float)bank, (float)attitude);
            }

            double sqx = q1.X * q1.X;
            double sqy = q1.Y * q1.Y;
            double sqz = q1.Z * q1.Z;

            heading = Math.Atan2(2 * q1.Y * q1.W - 2 * q1.X * q1.Z, 1 - 2 * sqy - 2 * sqz);
            attitude = Math.Asin(2 * test);
            bank = Math.Atan2(2 * q1.X * q1.W - 2 * q1.Y * q1.Z, 1 - 2 * sqx - 2 * sqz);

            return new Vector3((float)heading, (float)bank, (float)attitude);
        }
    }
}
