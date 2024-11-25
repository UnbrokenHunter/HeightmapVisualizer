using HeightmapVisualizer.src.Components;
using ReflectionMagic;
using System.Numerics;
using System.Reflection;
using static HeightmapVisualizer.src.Components.MeshComponent;

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
            var mesh = new MeshComponent(new Face[] { face });

            // Act
            face.Triangulate(mesh);

            // Assert
            Assert.Equal(2, mesh.AsDynamic().Tris.Count); // Quad should produce 2 triangles
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
            var mesh = new MeshComponent(new Face[] { face });

            // Act
            face.Triangulate(mesh);

            // Assert
            Assert.Equal(1, mesh.AsDynamic().Tris.Count); // Quad should produce 1 triangles
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
            var mesh = new MeshComponent(new Face[] { face });

            // Act
            face.Triangulate(mesh);

            // Assert
            Assert.Equal(3, mesh.AsDynamic().Tris.Count); // Pentagon should produce 3 triangles
        }
    }
}
