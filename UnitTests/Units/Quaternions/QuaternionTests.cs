using HeightmapVisualizer.Units;

namespace UnitTests.Units.Quaternions
{
    // Main class for quaternion operation and conversion tests
    public class QuaternionTests
    {

        [Fact]
        public void LookAt_TestingNonUnitVectors()
        {
            // Arrange: Create a vector and a quaternion
            Vector3 sourcePosition = Vector3.Zero;
            Vector3 LookAtPosition = new Vector3(3, 4, 5);

            // Expected result after rotation
            Quaternion expectedQuaternion = new Quaternion(0.9238796f, -0.3061468f, 0.2296101f, 0f);

            // Assert: Verify that the rotation produces the expected result
            Assert.Equal(expectedQuaternion, Quaternion.LookAt(sourcePosition, LookAtPosition));

        }

        [Fact]
        public void LookAt_TestingMultipleAxisAtOnce()
        {
            // Arrange: Create a vector and a quaternion
            Vector3 sourcePosition = Vector3.Zero;
            Vector3 LookAtPosition = new Vector3(1, 1, 1);

            // Expected result after rotation
            Quaternion expectedQuaternion = new Quaternion(0.8880739f, -0.3250576f, 0.3250576f, 0f);

            // Assert: Verify that the rotation produces the expected result
            Assert.Equal(expectedQuaternion, Quaternion.LookAt(sourcePosition, LookAtPosition));

        }

        [Fact]
        public void LookAt_LookForward()
        {
            // Arrange: Create a vector and a quaternion
            Vector3 sourcePosition = Vector3.Zero;
            Vector3 LookAtPosition = new Vector3(0, 0, 1);

            // Expected result after rotation
            Quaternion expectedQuaternion = new Quaternion(1f, 0f, 0f, 0f);

            // Assert: Verify that the rotation produces the expected result
            Assert.Equal(expectedQuaternion, Quaternion.LookAt(sourcePosition, LookAtPosition));

        }

        [Fact]
        public void LookAt_Look90DegreesUp()
        {
            // Arrange: Create a vector and a quaternion
            Vector3 sourcePosition = Vector3.Zero;
            Vector3 LookAtPosition = new Vector3(0, 1, 0);

            // Expected result after rotation
            Quaternion expectedQuaternion = new Quaternion(0.7071068f, -0.7071068f, 0f, 0f);

            // Assert: Verify that the rotation produces the expected result
            Assert.Equal(expectedQuaternion, Quaternion.LookAt(sourcePosition, LookAtPosition));

        }

        [Fact]
        public void LookAt_Look90DegreesRight()
        {
            // Arrange: Create a vector and a quaternion
            Vector3 sourcePosition = Vector3.Zero;
            Vector3 LookAtPosition = new Vector3(1, 0, 0);

            // Expected result after rotation
            Quaternion expectedQuaternion = new Quaternion(0.7071068f, 0f, 0.7071068f, 0f);

            // Assert: Verify that the rotation produces the expected result
            Assert.Equal(expectedQuaternion, Quaternion.LookAt(sourcePosition, LookAtPosition));

        }

        // Test for rotating a vector using a quaternion
        [Fact]
        public void Rotate_Test()
        {
            // Arrange: Create a vector and a quaternion
            Vector3 vector = new Vector3(1, 1, 1);
            Quaternion quaternion = new Quaternion(1, 0, 1, 0);

            // Expected result after rotation
            Vector3 expected = new Vector3(-1, 1, 1);

            // Assert: Verify that the rotation produces the expected result
            Assert.Equal(expected, Quaternion.Rotate(vector, quaternion));

        }


        // Test: Quaternion multiplication returns correct result
        [Fact]
        public void Multiply_ShouldReturnCorrectResult_ForTwoQuaternions()
        {
            // Arrange: Create two quaternions
            var q1 = new Quaternion(1, 0, 1, 0);
            var q2 = new Quaternion(1, 0.5f, 0.5f, 0.75f);

            // Act: Multiply the quaternions
            var result = q1 * q2;

            // Assert: Check that the result matches the expected quaternion
            var expected = new Quaternion(0.5f, 1.25f, 1.5f, 0.25f);
            Assert.Equal(expected, result);
        }

        // Test: Multiplying a quaternion by a zero quaternion results in zero quaternion
        [Fact]
        public void Multiply_ShouldReturnZeroQuaternion_WhenMultipliedByZeroQuaternion()
        {
            // Arrange: Create a non-zero and a zero quaternion
            var q1 = new Quaternion(1, 2, 3, 4);
            var zero = new Quaternion(0, 0, 0, 0);

            // Act: Multiply the quaternions
            var result = q1 * zero;

            // Assert: Check that the result is the zero quaternion
            var expected = new Quaternion(0, 0, 0, 0);
            Assert.Equal(expected, result);
        }

        // Test: Multiplying a quaternion by the identity quaternion returns the same quaternion
        [Fact]
        public void Multiply_ShouldReturnSameQuaternion_WhenMultipliedByIdentityQuaternion()
        {
            // Arrange: Create a quaternion and the identity quaternion
            var q1 = new Quaternion(1, 2, 3, 4);
            var identity = new Quaternion(1, 0, 0, 0);

            // Act: Multiply the quaternions
            var result = q1 * identity;

            // Assert: The result should be the original quaternion
            Assert.Equal(q1, result);
        }

        // Test: Custom quaternion multiplication test based on known values
        [Fact]
        public void Mulitply_Test1()
        {
            // Arrange: Input quaternions
            var q = new Quaternion(1, 7, 1, 2);
            var r = new Quaternion(0, -1, 8, 12);

            // Act: Perform quaternion multiplication
            var result = q * r;

            // Expected result based on MATLAB computation
            var expected = new Quaternion(-25, -5, -78, 69);

            // Assert: Check if the result matches the expected quaternion
            Assert.Equal(expected, result);
        }

        // Test for normalizing a quaternion
        [Fact]
        public void Normalize_Test1()
        {
            // Arrange: Create a quaternion
            var q = new Quaternion(1, 0, 1, 0);

            // Expected normalized quaternion
            var expectedNormal = new Quaternion(0.7071f, 0, 0.7071f, 0);

            // Assert: Verify that the quaternion is normalized correctly
            Assert.Equal(expectedNormal, Quaternion.Normalize(q));
        }
        // Test: Calculating the inverse of a quaternion
        [Fact]
        public void ToInverse_Test1()
        {
            var q = new Quaternion(1, 0, 1, 0);
            var expectedInverse = new Quaternion(0.5f, 0, -0.5f, 0);

            // Assert: Verify the inverse calculation
            Assert.Equal(expectedInverse, Quaternion.ToInverse(q));
        }

        // Test: Inverse calculation for a different quaternion
        [Fact]
        public void ToInverse_Test2()
        {
            var q = new Quaternion(0.5f, 1, -2, 2);
            var expectedInverse = new Quaternion(0.5f / 9.25f, -1f / 9.25f, 2f / 9.25f, -2f / 9.25f);

            // Assert: Verify the inverse calculation
            Assert.Equal(expectedInverse, Quaternion.ToInverse(q));
        }

        // Test: Inverse calculation for another quaternion
        [Fact]
        public void ToInverse_Test3()
        {
            var q = new Quaternion(-1, 0.5f, 0.5f, -0.5f);
            var expectedInverse = new Quaternion(-4f / 7f, -2f / 7f, -2f / 7f, 2f / 7f);

            // Assert: Verify the inverse calculation
            Assert.Equal(expectedInverse, Quaternion.ToInverse(q));
        }
        // Test: Length of a positive quaternion
        [Fact]
        public void Length_ShouldReturnCorrectValue_ForPositiveQuaternion()
        {
            // Arrange: Create a quaternion
            var quaternion = new Quaternion(1, 2, 3, 4);

            // Act: Calculate the length
            float length = Quaternion.Length(quaternion);

            // Assert: Verify that the length is correct
            float expectedLength = (float)Math.Sqrt(1 * 1 + 2 * 2 + 3 * 3 + 4 * 4);
            Assert.Equal(expectedLength, length, precision: 5);
        }

        // Test: Length of a zero quaternion
        [Fact]
        public void Length_ShouldReturnZero_ForZeroQuaternion()
        {
            // Arrange: Create a zero quaternion
            var quaternion = new Quaternion(0, 0, 0, 0);

            // Act: Calculate the length
            float length = Quaternion.Length(quaternion);

            // Assert: The length should be zero
            Assert.Equal(0, length);
        }

        // Test: Length of a negative quaternion
        [Fact]
        public void Length_ShouldReturnCorrectValue_ForNegativeQuaternion()
        {
            // Arrange: Create a negative quaternion
            var quaternion = new Quaternion(-1, -2, -3, -4);

            // Act: Calculate the length
            float length = Quaternion.Length(quaternion);

            // Assert: The length should be the same as for positive values
            float expectedLength = (float)Math.Sqrt(-1 * -1 + -2 * -2 + -3 * -3 + -4 * -4);
            Assert.Equal(expectedLength, length, precision: 5);
        }

        // Test: Length squared of a positive quaternion
        [Fact]
        public void LengthSquared_ShouldReturnCorrectValue_ForPositiveQuaternion()
        {
            // Arrange: Create a quaternion
            var quaternion = new Quaternion(1, 2, 3, 4);

            // Act: Calculate the squared length
            float lengthSquared = Quaternion.LengthSquared(quaternion);

            // Assert: Verify that the squared length is correct
            float expectedLengthSquared = 1 * 1 + 2 * 2 + 3 * 3 + 4 * 4;
            Assert.Equal(expectedLengthSquared, lengthSquared);
        }

        // Test: Length squared of a zero quaternion
        [Fact]
        public void LengthSquared_ShouldReturnZero_ForZeroQuaternion()
        {
            // Arrange: Create a zero quaternion
            var quaternion = new Quaternion(0, 0, 0, 0);

            // Act: Calculate the squared length
            float lengthSquared = Quaternion.LengthSquared(quaternion);

            // Assert: Verify that the squared length is zero
            Assert.Equal(0, lengthSquared);
        }

        // Test: Length squared of a negative quaternion
        [Fact]
        public void LengthSquared_ShouldReturnCorrectValue_ForNegativeQuaternion()
        {
            // Arrange: Create a negative quaternion
            var quaternion = new Quaternion(-1, -2, -3, -4);

            // Act: Calculate the squared length
            float lengthSquared = Quaternion.LengthSquared(quaternion);

            // Assert: Verify that the squared length is correct
            float expectedLengthSquared = -1 * -1 + -2 * -2 + -3 * -3 + -4 * -4;
            Assert.Equal(expectedLengthSquared, lengthSquared);
        }

        #region ToPitchYawRoll Around 1 Axis (Non-Gimble Lock)

        [Fact]
        public void ToPitchYawRoll_45DegreesAroundYAxis()
        {
            Quaternion q = new Quaternion(0.9238796f, 0f, 0.3826834f, 0f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0f, 0.7853981f, 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_Negative45DegreesAroundYAxis()
        {
            Quaternion q = new Quaternion(0.9238796f, 0f, -0.3826834f, 0f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0f, 5.497787f, 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_45DegreesAroundXAxis()
        {
            Quaternion q = new Quaternion(0.9238796f, 0.3826834f, 0f, 0f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0.7853982f, 0f, 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_Negative45DegreesAroundXAxis()
        {
            Quaternion q = new Quaternion(0.9238796f, -0.3826834f, 0f, 0f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(5.497787f, 0f, 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_45DegreesAroundZAxis()
        {
            Quaternion q = new Quaternion(0.9238796f, 0f, 0f, 0.3826834f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0f, 0f, 0.7853981f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_Negative45DegreesAroundZAxis()
        {
            Quaternion q = new Quaternion(0.9238796f, 0f, 0f, -0.3826834f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0f, 0f, 5.497787f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        #endregion

        #region ToPitchYawRoll Around 1 Axis (Gimble Lock)

        [Fact]
        public void ToPitchYawRoll_90DegreesAroundYAxis()
        {
            // Quaternion for 90-degree rotation around the Y-axis (gimbal lock scenario)
            Quaternion q = new Quaternion(0.7071068f, 0f, 0.7071068f, 0f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0f, 1.570796f, 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_270DegreesAroundYAxis()
        {
            // Quaternion for 270 degree rotation around the Y-axis (gimbal lock scenario)
            Quaternion q = new Quaternion(-0.7071068f, 0f, 0.7071068f, 0f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0f, 4.712389f, 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_180DegreesAroundYAxis()
        {
            // Quaternion for 180-degree rotation around the Y-axis (gimbal lock scenario)
            Quaternion q = new Quaternion(0, 0f, 1f, 0f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0f, 3.141593f, 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_360DegreesAroundYAxis()
        {
            // Quaternion for 360-degree rotation around the Y-axis (gimbal lock scenario)
            Quaternion q = new Quaternion(-1f, 0f, 0f, 0f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0f, 0f, 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_90DegreesAroundXAxis()
        {
            // Quaternion for 90-degree rotation around the X-axis (gimbal lock scenario)
            Quaternion q = new Quaternion(0.7071068f, 0.7071068f, 0f, 0f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(1.570796f, 0f, 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_270DegreesAroundXAxis()
        {
            // Quaternion for 270-degree rotation around the X-axis (gimbal lock scenario)
            Quaternion q = new Quaternion(-0.7071068f, 0.7071068f, 0f, 0f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(4.712389f, 0f, 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_180DegreesAroundXAxis()
        {
            // Quaternion for 180-degree rotation around the X-axis (gimbal lock scenario)
            Quaternion q = new Quaternion(0, 1f, 0f, 0f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0, 3.141593f, 3.141593f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_360DegreesAroundXAxis()
        {
            // Quaternion for 360-degree rotation around the X-axis (gimbal lock scenario)
            Quaternion q = new Quaternion(-1f, 0f, 0f, 0f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0f, 0f, 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_90DegreesAroundZAxis()
        {
            // Quaternion for 90-degree rotation around the Z-axis (gimbal lock scenario)
            Quaternion q = new Quaternion(0.7071068f, 0f, 0f, 0.7071068f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0f, 0f, 1.570796f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_270DegreesAroundZAxis()
        {
            // Quaternion for 270-degree rotation around the Z-axis (gimbal lock scenario)
            Quaternion q = new Quaternion(-0.7071068f, 0f, 0f, 0.7071068f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0f, 0f, 4.712389f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_180DegreesAroundZAxis()
        {
            // Quaternion for 180-degree rotation around the Z-axis (gimbal lock scenario)
            Quaternion q = new Quaternion(0, 0f, 0f, 1f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0f, 0f, 3.141593f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_360DegreesAroundZAxis()
        {
            // Quaternion for 360-degree rotation around the Z-axis (gimbal lock scenario)
            Quaternion q = new Quaternion(-1f, 0f, 0f, 0f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0f, 0f, 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        #endregion

        #region ToPitchYawRoll Around 2 Axies (Non Gimble Lock)

        [Fact]
        public void ToPitchYawRoll_30DegreesAroundXAxis_45DegreesAroundYAxis()
        {
            Quaternion q = new Quaternion(0.8923991f, 0.2391177f, 0.3696438f, -0.09904577f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0.5235988f, 0.7853982f, 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_45DegreesAroundXAxis_30DegreesAroundYAxis()
        {
            Quaternion q = new Quaternion(0.8923991f, 0.3696438f, 0.2391177f, -0.09904577f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0.7853981f, 0.5235988f, 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_30DegreesAroundXAxis_45DegreesAroundZAxis()
        {
            Quaternion q = new Quaternion(0.8923991f, 0.2391177f, -0.09904576f, 0.3696438f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0.5235989f, 0, 0.7853981f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_45DegreesAroundXAxis_30DegreesAroundZAxis()
        {
            Quaternion q = new Quaternion(0.8923991f, 0.3696438f, -0.09904578f, 0.2391177f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0.7853982f, 0, 0.5235988f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_30DegreesAroundYAxis_45DegreesAroundZAxis()
        {
            Quaternion q = new Quaternion(0.8923991f, 0.09904578f, 0.2391177f, 0.3696438f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0f, 0.5235988f, 0.7853982f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_45DegreesAroundYAxis_30DegreesAroundZAxis()
        {
            Quaternion q = new Quaternion(0.8923991f, 0.09904578f, 0.3696438f, 0.2391177f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0f, 0.7853982f, 0.5235988f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }


        #endregion

        #region ToPitchYawRoll Around 2 Axis (Gimble Lock) 

        [Fact]
        public void ToPitchYawRoll_90DegreesAroundXAxis_45DegreesAroundYAxis()
        {
            Quaternion q = new Quaternion(0.6532815f, 0.6532815f, 0.2705981f, -0.2705981f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(1.570796f, 0.7853982f, 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_45DegreesAroundXAxis_90DegreesAroundYAxis()
        {
            Quaternion q = new Quaternion(0.6532815f, 0.2705981f, 0.6532815f, -0.2705981f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0.7853982f, 1.570796f, 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_90DegreesAroundXAxis_45DegreesAroundZAxis()
        {
            Quaternion q = new Quaternion(0.6532815f, 0.6532815f, -0.2705981f, 0.2705981f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(1.570796f, 5.497787f, 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_45DegreesAroundXAxis_90DegreesAroundZAxis()
        {
            Quaternion q = new Quaternion(0.6532815f, 0.2705981f, -0.2705981f, 0.6532815f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0.7853982f, 0f, 1.570796f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_90DegreesAroundYAxis_45DegreesAroundZAxis()
        {
            Quaternion q = new Quaternion(0.6532815f, 0.2705981f, 0.6532815f, 0.2705981f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0f, 1.570796f, 0.7853982f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToPitchYawRoll_45DegreesAroundYAxis_90DegreesAroundZAxis()
        {
            Quaternion q = new Quaternion(0.6532815f, 0.2705981f, 0.2705981f, 0.6532815f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0f, 0.7853982f, 1.570796f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        #endregion

        [Fact]
        public void ToEulerAngles_IdentityQuaternion_Test()
        {
            // Identity quaternion (no rotation)
            Quaternion q = new Quaternion(1f, 0f, 0f, 0f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0f, 0f, 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToPitchYawRoll(q);

            // Assert they are equal (within some tolerance)
            Assert.Equal(expectedEulerAngles, resultEulerAngles);
        }


        [Fact]
        public void ToQuaternion_StandardEulerAngles_Test()
        {
            // Test for standard Euler angles to quaternion conversion
            Assert.Equal(
                new Quaternion(0.5f, 0.5f, 0.5f, 0.5f),
                Quaternion.CreateFromPitchYawRoll(new Vector3(z: 1.5707963f, x: 1.5707963f, y: 0f))
            );
        }

        [Fact]
        public void ToQuaternion_45DegreesAroundZ_Test()
        {
            // Euler angles for 45-degree rotation around Z-axis (in radians)
            Vector3 eulerAngles = new Vector3(0f, 0f, 0.7853982f);

            // Expected quaternion
            Quaternion expectedQuaternion = new Quaternion(0.9238795f, 0f, 0.3826835f, 0f);

            // Convert Euler angles to Quaternion
            Quaternion resultQuaternion = Quaternion.CreateFromPitchYawRoll(eulerAngles);

            // Assert they are equal (within some tolerance)
            Assert.Equal(expectedQuaternion, resultQuaternion);
        }

        [Fact]
        public void ToQuaternion_90DegreesAroundX_Test()
        {
            // Euler angles for 90-degree rotation around X-axis (in radians)
            Vector3 eulerAngles = new Vector3(1.5707963f, 0f, 0f);

            // Expected quaternion
            Quaternion expectedQuaternion = new Quaternion(0.7071068f, 0f, 0f, 0.7071067f);

            // Convert Euler angles to Quaternion
            Quaternion resultQuaternion = Quaternion.CreateFromPitchYawRoll(eulerAngles);

            // Assert they are equal (within some tolerance)
            Assert.Equal(expectedQuaternion, resultQuaternion);
        }

        [Fact]
        public void ToQuaternion_IdentityEulerAngles_Test()
        {
            // Euler angles for no rotation (identity rotation)
            Vector3 eulerAngles = new Vector3(0f, 0f, 0f);

            // Expected quaternion (identity quaternion)
            Quaternion expectedQuaternion = new Quaternion(1f, 0f, 0f, 0f);

            // Convert Euler angles to Quaternion
            Quaternion resultQuaternion = Quaternion.CreateFromPitchYawRoll(eulerAngles);

            // Assert they are equal (within some tolerance)
            Assert.Equal(expectedQuaternion, resultQuaternion);
        }



        [Fact]
        public void CreateFromAxisAngle_TestingNonNomalizedAxis()
        {
            // Axis to rotate around
            Vector3 axis = new Vector3(3, 1, 0f);

            // Amount to rotate around the axis
            float angle = 30f;

            // Expected quaternion (identity quaternion)
            Quaternion expectedQuaternion = new Quaternion(0.9659259f, 0.2455373f, 0.08184578f, 0f);

            // Convert Euler angles to Quaternion
            Quaternion resultQuaternion = Quaternion.CreateFromAxisAngle(axis, angle);

            // Assert they are equal (within some tolerance)
            Assert.True(AreApproximatelyEqual(expectedQuaternion, resultQuaternion),
                        $"Original: {expectedQuaternion}, After Conversion: {resultQuaternion}");
        }

        [Fact]
        public void CreateFromAxisAngle_Testing2Axis() 
        {
            // Axis to rotate around
            Vector3 axis = new Vector3(0.7071068f, 0.7071068f, 0f);

            // Amount to rotate around the axis
            float angle = 45f;

            // Expected quaternion (identity quaternion)
            Quaternion expectedQuaternion = new Quaternion(0.9238795f, 0.2705981f, 0.2705981f, 0f);

            // Convert Euler angles to Quaternion
            Quaternion resultQuaternion = Quaternion.CreateFromAxisAngle(axis, angle);

            // Assert they are equal (within some tolerance)
            Assert.True(AreApproximatelyEqual(expectedQuaternion, resultQuaternion),
                        $"Original: {expectedQuaternion}, After Conversion: {resultQuaternion}");
        }

        [Fact]
        public void CreateFromAxisAngle_Testing3Axis() // (45, 45, 45) deg euler angles
        {
            // Axis to rotate around
            Vector3 axis = new Vector3(0.8628563f, 0.3574068f, 0.3574068f);

            // Amount to rotate around the axis
            float angle = 64.73682f;

            // Expected quaternion (identity quaternion)
            Quaternion expectedQuaternion = new Quaternion(0.8446232f, 0.4619398f, 0.1913417f, 0.1913417f);

            // Convert Euler angles to Quaternion
            Quaternion resultQuaternion = Quaternion.CreateFromAxisAngle(axis, angle);

            // Assert they are equal (within some tolerance)
            Assert.True(AreApproximatelyEqual(expectedQuaternion, resultQuaternion),
                        $"Original: {expectedQuaternion}, After Conversion: {resultQuaternion}");
        }

        [Fact]
        public void CreateFromAxisAngle_90DegreeAsRadian()
        {
            // Axis to rotate around
            Vector3 axis = new Vector3(1f, 0f, 0f);

            // Amount to rotate around the axis
            float angle = (float)(Math.PI / 2);

            // Expected quaternion (identity quaternion)
            Quaternion expectedQuaternion = new Quaternion(0.7071f, 0.7071f, 0f, 0f);

            // Convert Euler angles to Quaternion
            Quaternion resultQuaternion = Quaternion.CreateFromAxisAngle(axis, angle, false);

            // Assert they are equal (within some tolerance)
            Assert.True(AreApproximatelyEqual(expectedQuaternion, resultQuaternion),
                        $"Original: {expectedQuaternion}, After Conversion: {resultQuaternion}");
        }

        [Fact]
        public void CreateFromAxisAngle_180DegreeAsRadian()
        {
            // Axis to rotate around
            Vector3 axis = new Vector3(0, 0.7071068f, -0.7071068f);

            // Amount to rotate around the axis
            float angle = (float)Math.PI;

            // Expected quaternion (identity quaternion)
            Quaternion expectedQuaternion = new Quaternion(0, 0, 0.7071068f, -0.7071068f);

            // Convert Euler angles to Quaternion
            Quaternion resultQuaternion = Quaternion.CreateFromAxisAngle(axis, angle, false);

            // Assert they are equal (within some tolerance)
            Assert.True(AreApproximatelyEqual(expectedQuaternion, resultQuaternion),
                        $"Original: {expectedQuaternion}, After Conversion: {resultQuaternion}");
        }

        [Fact]
        public void ToEulerAnglesAndBack_RoundTripConversion_Test()
        {
            // Define an initial set of Euler angles
            Vector3 euler = new Vector3(0.5f, 0.5f, 0.5f);

            // Convert to quaternion
            Quaternion q = Quaternion.CreateFromPitchYawRoll(euler);

            // Convert back to Euler angles
            Vector3 eulerBack = Quaternion.ToPitchYawRoll(q);

            // Compare the original Euler angles with the result after conversion
            Assert.True(AreApproximatelyEqual(euler, eulerBack),
                        $"Original: {euler}, After round-trip: {eulerBack}");
        }

        [Fact]
        public void ToEulerAnglesAndBack_RoundTripConversion2_Test()
        {
            // Define an initial set of Euler angles
            Vector3 euler = Vector3.Normalize(new Vector3(9, 0, 0));

            // Convert to quaternion
            Quaternion q = Quaternion.CreateFromPitchYawRoll(euler);

            // Convert back to Euler angles
            Vector3 eulerBack = Quaternion.ToPitchYawRoll(q);

            // Compare the original Euler angles with the result after conversion
            Assert.True(AreApproximatelyEqual(euler, eulerBack),
                        $"Original: {euler}, After round-trip: {eulerBack}");
        }

        [Fact]
        public void ToEulerAnglesAndBack_RoundTripConversion3_Test()
        {
            // Define an initial set of Euler angles
            Vector3 euler = new Vector3(0f, 0.5f, 1f);

            // Convert to quaternion
            Quaternion q = Quaternion.CreateFromPitchYawRoll(euler);

            // Convert back to Euler angles
            Vector3 eulerBack = Quaternion.ToPitchYawRoll(q);

            // Compare the original Euler angles with the result after conversion
            Assert.True(AreApproximatelyEqual(euler, eulerBack),
                        $"Original: {euler}, After round-trip: {eulerBack}");
        }

        [Fact]
        public void ToQuaternionAndBack_RoundTripConversion1_Test()
        {
            // Define an initial Quaternion
            Quaternion quat = new Quaternion(0.5f, 0.5f, 0.5f, 0.5f);

            // Convert to Euler Angles
            Vector3 v = Quaternion.ToPitchYawRoll(quat);

            // Convert back to Quaternion
            Quaternion quatBack = Quaternion.CreateFromPitchYawRoll(v);

            // Compare the original Quaternion with the result after conversion
            Assert.True(AreApproximatelyEqual(quat, quatBack),
                        $"Original: {quat}, After round-trip: {quatBack}");
        }

        [Fact]
        public void ToQuaternionAndBack_RoundTripConversion2_Test()
        {
            // Define an initial Quaternion
            Quaternion quat = new Quaternion(1f, 0f, 0f, 0f);

            // Convert to Euler Angles
            Vector3 v = Quaternion.ToPitchYawRoll(quat);

            // Convert back to Quaternion
            Quaternion quatBack = Quaternion.CreateFromPitchYawRoll(v);

            // Compare the original Quaternion with the result after conversion
            Assert.True(AreApproximatelyEqual(quat, quatBack),
                        $"Original: {quat}, After round-trip: {quatBack}");
        }

        [Fact]
        public void ToQuaternionAndBack_RoundTripConversion3_Test()
        {
            // Define an initial Quaternion
            Quaternion quat = Quaternion.Normalize(new Quaternion(12f, 9f, 4f, 0f));

            // Convert to Euler Angles
            Vector3 v = Quaternion.ToPitchYawRoll(quat);

            // Convert back to Quaternion
            Quaternion quatBack = Quaternion.CreateFromPitchYawRoll(v);

            // Compare the original Quaternion with the result after conversion
            Assert.True(AreApproximatelyEqual(quat, quatBack),
                        $"Original: {quat}, After round-trip: {quatBack}");
        }


        [Fact]
        public void Equals_TestIfObjectIsEqualToItself()
        {
            // Create Object
            Quaternion quaternion = new Quaternion(1, 3, 2, 1);

            // Assert they are equal (within some tolerance)
            Assert.True(quaternion.Equals(quaternion),
                        $"Original: {quaternion}");

        }

        [Fact]
        public void Equals_TestIfObjectIsEqualToIdenticalObject()
        {
            // Create Object
            Quaternion quaternion1 = new Quaternion(1, 3, 2, 1);
            Quaternion quaternion2 = new Quaternion(1, 3, 2, 1);

            // Assert they are equal (within some tolerance)
            Assert.True(quaternion1.Equals(quaternion2),
                        $"Quaternion 1: {quaternion1}, Quaternion 2: {quaternion2}");

        }

        [Fact]
        public void Equals_TestIfDifferentQuaternionsAreNotEqual()
        {
            // Create Object
            Quaternion quaternion1 = new Quaternion(1, 3, 2, 1);
            Quaternion quaternion2 = new Quaternion(1, 2, 3, 4);

            // Assert they are equal (within some tolerance)
            Assert.False(quaternion1.Equals(quaternion2),
                        $"Quaternion 1: {quaternion1}, Quaternion 2: {quaternion2}");

        }

        [Fact]
        public void Equals_TestIfDifferentTypeOfObjectIsNotEqualToQuaternion()
        {
            // Create Object
            Quaternion quaternion = new Quaternion(1, 3, 2, 1);
            Vector3 vector3 = new Vector3(3, 2, 1);

            // Assert they are equal (within some tolerance)
            Assert.False(quaternion.Equals(vector3),
                        $"Quaternion: {quaternion}, Vector3: {vector3}");

        }

        [Fact]
        public void EqualsOperator_TestIfObjectIsEqualToIdenticalObject()
        {
            // Create Object
            Quaternion quaternion1 = new Quaternion(4, 3, 2, 1);
            Quaternion quaternion2 = new Quaternion(4, 3, 2, 1);

            // Assert they are equal (within some tolerance)
            Assert.True(quaternion1 == quaternion2,
                        $"Quaternion 1: {quaternion1}, Quaternion 2: {quaternion2}");

        }

        [Fact]
        public void EqualsOperator_TestIfDifferentQuaternionsAreNotEqual()
        {
            // Create Object
            Quaternion quaternion1 = new Quaternion(4, 3, 2, 1);
            Quaternion quaternion2 = new Quaternion(3, 4, 2, 1);

            // Assert they are equal (within some tolerance)
            Assert.False(quaternion1 == quaternion2,
                        $"Quaternion 1: {quaternion1}, Quaternion 2: {quaternion2}");

        }

        [Fact]
        public void NotEqualsOperator_TestIfObjectIsEqualToIdenticalObject()
        {
            // Create Object
            Quaternion quaternion1 = new Quaternion(4, 3, 2, 1);
            Quaternion quaternion2 = new Quaternion(4, 3, 2, 1);

            // Assert they are equal (within some tolerance)
            Assert.False(quaternion1 != quaternion2,
                        $"Quaternion 1: {quaternion1}, Quaternion 2: {quaternion2}");

        }

        [Fact]
        public void NotEqualsOperator_TestIfDifferentQuaternionsAreNotEqual()
        {
            // Create Object
            Quaternion quaternion1 = new Quaternion(4, 3, 2, 1);
            Quaternion quaternion2 = new Quaternion(3, 4, 2, 1);

            // Assert they are equal (within some tolerance)
            Assert.True(quaternion1 != quaternion2,
                        $"Quaternion 1: {quaternion1}, Quaternion 2: {quaternion2}");

        }


        // Helper method for approximate equality checks
        public static bool AreApproximatelyEqual(Vector3 v1, Vector3 v2, float epsilon = 0.0001f)
        {
            return Math.Abs(v1.x - v2.x) < epsilon &&
                   Math.Abs(v1.y - v2.y) < epsilon &&
                   Math.Abs(v1.z - v2.z) < epsilon;
        }

        public static bool AreApproximatelyEqual(Quaternion v1, Quaternion v2, float epsilon = 0.0001f)
        {
            return Math.Abs(v1.w - v2.w) < epsilon &&
                   Math.Abs(v1.x - v2.x) < epsilon &&
                   Math.Abs(v1.y - v2.y) < epsilon &&
                   Math.Abs(v1.z - v2.z) < epsilon;
        }
    }
}
