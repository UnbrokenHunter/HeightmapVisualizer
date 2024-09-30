using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.Units;

namespace UnitTests
{
	public class FaceTests
	{
		[Fact]
		public void Triangulate_ShouldReturnCorrectNumberOfTriangles_ForQuad()
		{
			// Arrange
			var points = new Vector3[]
			{
			new Vector3(0, 0, 0),
			new Vector3(1, 0, 0),
			new Vector3(1, 1, 0),
			new Vector3(0, 1, 0)
			};
			var face = new Face(points);

			// Act
			var tris = face.Triangulate(mesh);

			// Assert
			Assert.Equal(2, tris.Length); // Quad should produce 2 triangles
		}
	}
}
