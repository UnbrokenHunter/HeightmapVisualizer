using HeightmapVisualizer.Units;

namespace UnitTests
{
	public class QuaternionConversionTests
	{
		[Fact]
		public void ToEulerAngles_Test()
		{
			Assert.Equal(
				new Vector3(z: 1.5707963f, x: 1.5707963f, y: 0f),
				Quaternion.ToEulerAngles(new Quaternion(0.5f, 0.5f, 0.5f, 0.5f))
			);
		}

		[Fact]
		public void ToQuaternion_Test()
		{
			Assert.Equal(
				new Quaternion(0.5f, 0.5f, 0.5f, 0.5f),
				Quaternion.ToQuaternion(new Vector3(z: 1.5707963f, x: 1.5707963f, y: 0f))
			);
		}

		[Fact]
		public void ToEulerAngles_GimbalLock_Test()
		{
			// Quaternion for 90-degree rotation around the X-axis (gimbal lock scenario)
			Quaternion q = new Quaternion(0.7071068f, 0.7071068f, 0f, 0f); // Quaternion for 90 degrees pitch

			// Expected Euler angles (in radians)
			Vector3 expectedEulerAngles = new Vector3(z: 0f, x: 1.5707963f, y: 0f); // Roll = 0, Pitch = π/2, Yaw = 0

			// Convert quaternion to Euler angles
			Vector3 resultEulerAngles = Quaternion.ToEulerAngles(q);

			// Assert they are approximately equal (within tolerance)
			Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
						$"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
		}

		[Fact]
		public void ToEulerAnglesAndBack_Test()
		{
			// Define an initial set of Euler angles
			Vector3 euler = new Vector3(0.5f, 0.5f, 0.5f);

			// Convert to quaternion
			Quaternion q = Quaternion.ToQuaternion(euler);

			// Convert back to Euler angles
			Vector3 eulerBack = Quaternion.ToEulerAngles(q);

			// Compare the original Euler angles with the result after conversion
			Assert.True(AreApproximatelyEqual(euler, eulerBack),
						$"Original: {euler}, After round-trip: {eulerBack}");
		}

		public static bool AreApproximatelyEqual(Vector3 v1, Vector3 v2, float epsilon = 0.0001f)
		{
			return Math.Abs(v1.x - v2.x) < epsilon &&
				   Math.Abs(v1.y - v2.y) < epsilon &&
				   Math.Abs(v1.z - v2.z) < epsilon;
		}

		[Fact]
		public void ToEulerAngles_90DegreesAroundY_Test()
		{
			// Quaternion for 90-degree rotation around Y-axis
			Quaternion q = new Quaternion(0.7071f, 0f, 0.7071f, 0f);

			// Expected Euler angles (in radians)
			Vector3 expectedEulerAngles = new Vector3(0f, 1.5707963f, 0f); // Pitch, Yaw, Roll

			// Convert quaternion to Euler angles
			Vector3 resultEulerAngles = Quaternion.ToEulerAngles(q);

			// Assert they are equal (within some tolerance)
			Assert.Equal(expectedEulerAngles, resultEulerAngles);
		}

		[Fact]
		public void ToQuaternion_45DegreesAroundZ_Test()
		{
			// Euler angles for 45-degree rotation around Z-axis (in radians)
			Vector3 eulerAngles = new Vector3(0f, 0f, 0.7853982f); // Pitch, Yaw, Roll (Z-axis)

			// Expected quaternion
			Quaternion expectedQuaternion = new Quaternion(0.9238795f, 0f, 0f, 0.3826834f);

			// Convert Euler angles to Quaternion
			Quaternion resultQuaternion = Quaternion.ToQuaternion(eulerAngles);

			// Assert they are equal (within some tolerance)
			Assert.Equal(expectedQuaternion, resultQuaternion);
		}

		[Fact]
		public void ToEulerAngles_45DegreesAroundXYZ_Test()
		{
			// Quaternion representing a 45-degree rotation around X, Y, and Z axes
			Quaternion q = new Quaternion(0.8446232f, 0.1913417f, 0.4619398f, 0.1913417f);

			// Expected Euler angles (in radians)
			Vector3 expectedEulerAngles = new Vector3(0.7853982f, 0.7853982f, 0.7853982f); // Pitch, Yaw, Roll

			// Convert quaternion to Euler angles
			Vector3 resultEulerAngles = Quaternion.ToEulerAngles(q);

			// Assert they are equal (within some tolerance)
			Assert.Equal(expectedEulerAngles, resultEulerAngles);
		}

		[Fact]
		public void ToQuaternion_90DegreesAroundX_Test()
		{
			// Euler angles for 90-degree rotation around X-axis (in radians)
			Vector3 eulerAngles = new Vector3(1.5707963f, 0f, 0f); // Pitch, Yaw, Roll

			// Expected quaternion
			Quaternion expectedQuaternion = new Quaternion(0.7071068f, 0.7071068f, 0f, 0f);

			// Convert Euler angles to Quaternion
			Quaternion resultQuaternion = Quaternion.ToQuaternion(eulerAngles);

			// Assert they are equal (within some tolerance)
			Assert.Equal(expectedQuaternion, resultQuaternion);
		}

		[Fact]
		public void ToEulerAngles_IdentityQuaternion_Test()
		{
			// Identity quaternion (no rotation)
			Quaternion q = new Quaternion(1f, 0f, 0f, 0f);

			// Expected Euler angles (in radians)
			Vector3 expectedEulerAngles = new Vector3(0f, 0f, 0f); // No rotation

			// Convert quaternion to Euler angles
			Vector3 resultEulerAngles = Quaternion.ToEulerAngles(q);

			// Assert they are equal (within some tolerance)
			Assert.Equal(expectedEulerAngles, resultEulerAngles);
		}

		[Fact]
		public void ToQuaternion_IdentityEulerAngles_Test()
		{
			// Euler angles for no rotation (identity rotation)
			Vector3 eulerAngles = new Vector3(0f, 0f, 0f);

			// Expected quaternion (identity quaternion)
			Quaternion expectedQuaternion = new Quaternion(1f, 0f, 0f, 0f);

			// Convert Euler angles to Quaternion
			Quaternion resultQuaternion = Quaternion.ToQuaternion(eulerAngles);

			// Assert they are equal (within some tolerance)
			Assert.Equal(expectedQuaternion, resultQuaternion);
		}
	}
}

