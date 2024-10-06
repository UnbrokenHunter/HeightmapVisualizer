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

        [Fact]
        public void ToEulerAngles_StandardQuaternion_Test()
        {
            // Test for standard quaternion to Euler angles conversion
            Assert.Equal(
                new Vector3(z: 1.5707963f, x: 1.5707963f, y: 0f),
                Quaternion.ToEulerAngles(new Quaternion(0.5f, 0.5f, 0.5f, 0.5f))
            );
        }

        [Fact]
        public void ToEulerAngles_GimbalLock_Test()
        {
            // Quaternion for 90-degree rotation around the X-axis (gimbal lock scenario)
            Quaternion q = new Quaternion(0.7071068f, 0.7071068f, 0f, 0f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(z: 0f, x: 1.5707963f, y: 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToEulerAngles(q);

            // Assert they are approximately equal (within tolerance)
            Assert.True(AreApproximatelyEqual(expectedEulerAngles, resultEulerAngles),
                        $"Expected: {expectedEulerAngles}, Actual: {resultEulerAngles}");
        }

        [Fact]
        public void ToEulerAngles_90DegreesAroundY_Test()
        {
            // Quaternion for 90-degree rotation around Y-axis
            Quaternion q = new Quaternion(0.7071f, 0f, 0.7071f, 0f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0f, 1.5707963f, 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToEulerAngles(q);

            // Assert they are equal (within some tolerance)
            Assert.Equal(expectedEulerAngles, resultEulerAngles);
        }

        [Fact]
        public void ToEulerAngles_45DegreesAroundXYZ_Test()
        {
            // Quaternion representing a 45-degree rotation around X, Y, and Z axes
            Quaternion q = new Quaternion(0.8446232f, 0.1913417f, 0.4619398f, 0.1913417f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0.7853982f, 0.7853982f, 0.7853982f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToEulerAngles(q);

            // Assert they are equal (within some tolerance)
            Assert.Equal(expectedEulerAngles, resultEulerAngles);
        }

        [Fact]
        public void ToEulerAngles_IdentityQuaternion_Test()
        {
            // Identity quaternion (no rotation)
            Quaternion q = new Quaternion(1f, 0f, 0f, 0f);

            // Expected Euler angles (in radians)
            Vector3 expectedEulerAngles = new Vector3(0f, 0f, 0f);

            // Convert quaternion to Euler angles
            Vector3 resultEulerAngles = Quaternion.ToEulerAngles(q);

            // Assert they are equal (within some tolerance)
            Assert.Equal(expectedEulerAngles, resultEulerAngles);
        }


        [Fact]
        public void ToQuaternion_StandardEulerAngles_Test()
        {
            // Test for standard Euler angles to quaternion conversion
            Assert.Equal(
                new Quaternion(0.5f, 0.5f, 0.5f, 0.5f),
                Quaternion.ToQuaternion(new Vector3(z: 1.5707963f, x: 1.5707963f, y: 0f))
            );
        }

        [Fact]
        public void ToQuaternion_45DegreesAroundZ_Test()
        {
            // Euler angles for 45-degree rotation around Z-axis (in radians)
            Vector3 eulerAngles = new Vector3(0f, 0f, 0.7853982f);

            // Expected quaternion
            Quaternion expectedQuaternion = new Quaternion(0.9238795f, 0f, 0f, 0.3826834f);

            // Convert Euler angles to Quaternion
            Quaternion resultQuaternion = Quaternion.ToQuaternion(eulerAngles);

            // Assert they are equal (within some tolerance)
            Assert.Equal(expectedQuaternion, resultQuaternion);
        }

        [Fact]
        public void ToQuaternion_90DegreesAroundX_Test()
        {
            // Euler angles for 90-degree rotation around X-axis (in radians)
            Vector3 eulerAngles = new Vector3(1.5707963f, 0f, 0f);

            // Expected quaternion
            Quaternion expectedQuaternion = new Quaternion(0.7071068f, 0.7071068f, 0f, 0f);

            // Convert Euler angles to Quaternion
            Quaternion resultQuaternion = Quaternion.ToQuaternion(eulerAngles);

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
            Quaternion resultQuaternion = Quaternion.ToQuaternion(eulerAngles);

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
            Quaternion q = Quaternion.ToQuaternion(euler);

            // Convert back to Euler angles
            Vector3 eulerBack = Quaternion.ToEulerAngles(q);

            // Compare the original Euler angles with the result after conversion
            Assert.True(AreApproximatelyEqual(euler, eulerBack),
                        $"Original: {euler}, After round-trip: {eulerBack}");
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
