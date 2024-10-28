using HeightmapVisualizer.src.Factories;
using System.Numerics;
using Plane = HeightmapVisualizer.src.Factories.Plane;

namespace UnitTests
{
    public class MeshTests
    {
        [Fact]
        public void CombineIdenticalEdges_OnCuboid_Test()
        {
            var mesh = Cuboid.CreateCentered(new Vector3(1, 1, 1));

            Assert.Equal(18, mesh.Edges.Count); // 18 because it is triangulated
        }

        [Fact]
        public void CombineIdenticalVerts_OnCuboid_Test()
        {
            var mesh = Cuboid.CreateCentered(new Vector3(1, 1, 1));

            Assert.Equal(8, mesh.Vertices.Count);
        }

        [Fact]
        public void CombineIdenticalEdges_OnPyramid_Test()
        {
            var mesh = Pyramid.CreateCentered(new Vector3(1, 1, 1));

            Assert.Equal(9, mesh.Edges.Count); // 18 because it is triangulated
        }

        [Fact]
        public void CombineIdenticalVerts_OnPyramid_Test()
        {
            var mesh = Pyramid.CreateCentered(new Vector3(1, 1, 1));

            Assert.Equal(5, mesh.Vertices.Count);
        }

        [Fact]
        public void CombineIdenticalEdges_OnPlane_Test()
        {
            var mesh = Plane.CreateCentered(new Vector2(1, 1));

            Assert.Equal(5, mesh.Edges.Count);
        }

        [Fact]
        public void CombineIdenticalVerts_OnPlane_Test()
        {
            var mesh = Plane.CreateCentered(new Vector2(1, 1));

            Assert.Equal(4, mesh.Vertices.Count);
        }
    }
}
