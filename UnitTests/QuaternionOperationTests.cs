
using HeightmapVisualizer.Units;

namespace UnitTests
{
	public class QuaternionOperationTests
	{

		[Fact]
		public void Rotate_Test()
		{
			Vector3 vector = new Vector3 (1, 1, 1);
			Quaternion quaternion = new Quaternion(1, 0, 1, 0);

			Vector3 expected = new Vector3(-1, 1, 1);

			Assert.Equal(expected, Quaternion.Rotate(vector, quaternion));
		}

		[Fact]
		public void Multiply_ShouldReturnCorrectResult_ForTwoQuaternions()
		{
			// Arrange
			var q1 = new Quaternion(1, 0, 1, 0); // (w, x, y, z)
			var q2 = new Quaternion(1, 0.5f, 0.5f, 0.75f); // (w, x, y, z)

			// Act
			var result = q1 * q2;

			// Assert
			var expected = new Quaternion(0.5f, 1.25f, 1.5f, 0.25f);

			Assert.Equal(expected, result);
		}

		[Fact]
		public void Multiply_ShouldReturnZeroQuaternion_WhenMultipliedByZeroQuaternion()
		{
			// Arrange
			var q1 = new Quaternion(1, 2, 3, 4); // (w, x, y, z)
			var zero = new Quaternion(0, 0, 0, 0); // (w, x, y, z)

			// Act
			var result = q1 * zero;

			// Assert
			var expected = new Quaternion(0, 0, 0, 0); // (w, x, y, z)
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Multiply_ShouldReturnSameQuaternion_WhenMultipliedByIdentityQuaternion()
		{
			// Arrange
			var q1 = new Quaternion(1, 2, 3, 4); // (w, x, y, z)
			var identity = new Quaternion(1, 0, 0, 0); // Identity quaternion (w, x, y, z)

			// Act
			var result = q1 * identity;

			// Assert
			Assert.Equal(q1, result);
		}

		[Fact]
		public void Mulitply_Test1()
		{
			// Arrange: Input quaternions q and r
			var q = new Quaternion(1, 7, 1, 2);
			var r = new Quaternion(0, -1, 8, 12);

			// Act: Perform quaternion multiplication
			var result = q * r;

			// Expected result from MATLAB: [-25, -5, -78, 69]
			var expected = new Quaternion(-25, -5, -78, 69);

			// Assert: Check if the result matches the expected quaternion
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Normalize_Test1()
		{
			var q = new Quaternion(1, 0, 1, 0);
			var expectedNormal = new Quaternion(0.7071f, 0, 0.7071f, 0);
			Assert.Equal(expectedNormal, Quaternion.Normalize(q));
		}

		[Fact]
		public void ToInverse_Test1()
		{
			var q = new Quaternion(1, 0, 1, 0);
			var expectedInverse = new Quaternion(0.5f, 0, -0.5f, 0);
			Assert.Equal(expectedInverse, Quaternion.ToInverse(q));
		}

		[Fact]
		public void ToInverse_Test2()
		{
			var q = new Quaternion(0.5f, 1, -2, 2);
			var expectedInverse = new Quaternion(0.5f / 9.25f, -1f / 9.25f, 2f / 9.25f, -2f / 9.25f);
			Assert.Equal(expectedInverse, Quaternion.ToInverse(q));
		}

		[Fact]
		public void ToInverse_Test3()
		{
			var q = new Quaternion(-1, 0.5f, 0.5f, -0.5f);
			var expectedInverse = new Quaternion(-4f / 7f, -2f / 7f, -2f / 7f, 2f / 7f);
			Assert.Equal(expectedInverse, Quaternion.ToInverse(q));
		}

		[Fact]
		public void Length_ShouldReturnCorrectValue_ForPositiveQuaternion()
		{
			// Arrange
			var quaternion = new Quaternion(1, 2, 3, 4);

			// Act
			float length = Quaternion.Length(quaternion);

			// Assert
			float expectedLength = (float)Math.Sqrt(1 * 1 + 2 * 2 + 3 * 3 + 4 * 4);
			Assert.Equal(expectedLength, length, precision: 5);
		}

		[Fact]
		public void Length_ShouldReturnZero_ForZeroQuaternion()
		{
			// Arrange
			var quaternion = new Quaternion(0, 0, 0, 0);

			// Act
			float length = Quaternion.Length(quaternion);

			// Assert
			Assert.Equal(0, length);
		}

		[Fact]
		public void Length_ShouldReturnCorrectValue_ForNegativeQuaternion()
		{
			// Arrange
			var quaternion = new Quaternion(-1, -2, -3, -4);

			// Act
			float length = Quaternion.Length(quaternion);

			// Assert
			float expectedLength = (float)Math.Sqrt((-1) * (-1) + (-2) * (-2) + (-3) * (-3) + (-4) * (-4));
			Assert.Equal(expectedLength, length, precision: 5);
		}
	}

	public class LengthSquaredTests
	{
		[Fact]
		public void LengthSquared_ShouldReturnCorrectValue_ForPositiveQuaternion()
		{
			// Arrange
			var quaternion = new Quaternion(1, 2, 3, 4);

			// Act
			float lengthSquared = Quaternion.LengthSquared(quaternion);

			// Assert
			float expectedLengthSquared = 1 * 1 + 2 * 2 + 3 * 3 + 4 * 4;
			Assert.Equal(expectedLengthSquared, lengthSquared);
		}

		[Fact]
		public void LengthSquared_ShouldReturnZero_ForZeroQuaternion()
		{
			// Arrange
			var quaternion = new Quaternion(0, 0, 0, 0);

			// Act
			float lengthSquared = Quaternion.LengthSquared(quaternion);

			// Assert
			Assert.Equal(0, lengthSquared);
		}

		[Fact]
		public void LengthSquared_ShouldReturnCorrectValue_ForNegativeQuaternion()
		{
			// Arrange
			var quaternion = new Quaternion(-1, -2, -3, -4);

			// Act
			float lengthSquared = Quaternion.LengthSquared(quaternion);

			// Assert
			float expectedLengthSquared = (-1) * (-1) + (-2) * (-2) + (-3) * (-3) + (-4) * (-4);
			Assert.Equal(expectedLengthSquared, lengthSquared);
		}

	}
}
