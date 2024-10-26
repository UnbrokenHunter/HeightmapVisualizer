using HeightmapVisualizer.src.Primitives;
using HeightmapVisualizer.Units;
using System.Drawing;

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
            var mesh = new Mesh(new Face[] { face });

            // Act
            var tris = face.Triangulate(mesh, Color.Black);

            // Assert
            Assert.Equal(2, tris.Length); // Quad should produce 2 triangles
        }

        [Fact]
        public void Triangulate_ShouldReturnCorrectNumberOfTriangles_ForTriangle()
        {
            // Arrange
            var points = new Vector3[]
            {
                new Vector3(0, 0, 0),
                new Vector3(1, 0, 0),
                new Vector3(1, 1, 0),
            };
            var face = new Face(points);
            var mesh = new Mesh(new Face[] { face });

            // Act
            var tris = face.Triangulate(mesh, Color.Black);

            // Assert
            Assert.Single(tris); // Quad should produce 2 triangles
        }

        [Fact]
        public void Triangulate_ShouldReturnCorrectNumberOfTriangles_ForPentagon()
        {
            // Arrange
            var points = new Vector3[]
            {
            new Vector3(0, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(2, 1, 0),
            new Vector3(1, 2, 0),
            new Vector3(0, 1, 0)
            };
            var face = new Face(points);
            var mesh = new Mesh(new Face[] { face });

            // Act
            var tris = face.Triangulate(mesh, Color.Black);

            // Assert
            Assert.Equal(3, tris.Length); // Pentagon should produce 3 triangles
        }

    }
}
