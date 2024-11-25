using HeightmapVisualizer.src.Components;
using HeightmapVisualizer.src.Factories;
using ReflectionMagic;
using System.Numerics;
using static HeightmapVisualizer.src.Components.MeshComponent;

namespace UnitTests
{
    public class MeshTests
    {
        [Fact]
        public void MeshCenter_2D_Test()
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

            Assert.Equal(new Vector3(0.5f, 0.5f, 0), mesh.GetMeshCenter());
        }

        [Fact]
        public void MeshCenter_Cuboid_Test()
        {
            // Arrange
            var face = Cuboid.CreateCorners(new Vector3(2f, 1f, 0.5f), new Vector3(-1f, 1f, 0f));
            var mesh = new MeshComponent(face);

            Assert.Equal(new Vector3(0, 1.5f, 0.25f), mesh.GetMeshCenter());
        }

        [Fact]
        public void MeshSize_2D_Test()
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

            Assert.Equal(new Vector3(1, 1, 0), mesh.GetMeshSize());
        }

        [Fact]
        public void MeshSize_Cuboid_Test()
        {
            // Arrange
            var face = Cuboid.CreateCorners(new Vector3(2f, 1f, 0.5f), new Vector3(-1f, 1f, 0f));
            var mesh = new MeshComponent(face);

            Assert.Equal(new Vector3(2, 1, 0.5f), mesh.GetMeshSize());
        }

        [Fact]
        public void CombineIdenticalEdges_OnCuboid_Test()
        {
            var faces = Cuboid.CreateCentered(new Vector3(1, 1, 1));
            var mesh = new MeshComponent(faces).AsDynamic();

            Assert.Equal(18, mesh.Edges.Count); // 18 because it is triangulated
        }

        [Fact]
        public void CombineIdenticalVerts_OnCuboid_Test()
        {
            var faces = Cuboid.CreateCentered(new Vector3(1, 1, 1));
			var mesh = new MeshComponent(faces).AsDynamic();

			Assert.Equal(8, mesh.Vertices.Count);
        }

        [Fact]
        public void CombineIdenticalEdges_OnPyramid_Test()
        {
            var faces = Pyramid.CreateCentered(new Vector3(1, 1, 1));
			var mesh = new MeshComponent(faces).AsDynamic();

			Assert.Equal(9, mesh.Edges.Count); // 18 because it is triangulated
        }

        [Fact]
        public void CombineIdenticalVerts_OnPyramid_Test()
        {
            var faces = Pyramid.CreateCentered(new Vector3(1, 1, 1));
			var mesh = new MeshComponent(faces).AsDynamic();

			Assert.Equal(5, mesh.Vertices.Count);
        }

        [Fact]
        public void CombineIdenticalEdges_OnPlane_Test()
        {
            var faces = HeightmapVisualizer.src.Factories.Plane.CreateCentered(new Vector2(1, 1));
			var mesh = new MeshComponent(faces).AsDynamic();

			Assert.Equal(5, mesh.Edges.Count);
        }

        [Fact]
        public void CombineIdenticalVerts_OnPlane_Test()
        {
            var faces = HeightmapVisualizer.src.Factories.Plane.CreateCentered(new Vector2(1, 1));
			var mesh = new MeshComponent(faces).AsDynamic();

			Assert.Equal(4, mesh.Vertices.Count);
        }
    }
}
