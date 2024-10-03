using HeightmapVisualizer.Units;
using HeightmapVisualizer.Shapes;

namespace UnitTests
{
	public class MeshTests
	{
		[Fact]
		public void CombineIdenticalEdges_OnCuboid_Test()
		{
			var mesh = Cuboid.CreateCentered(new Vector3(0, 0, 0), new Vector3(1, 1, 1));

			Assert.Equal(12, mesh.edgeDict.Count);
		}

		[Fact]
		public void CombineIdenticalVerts_OnCuboid_Test()
		{
			var mesh = Cuboid.CreateCentered(new Vector3(0, 0, 0), new Vector3(1, 1, 1));

			Assert.Equal(8, mesh.vertexDict.Count);
		}
	}
}
