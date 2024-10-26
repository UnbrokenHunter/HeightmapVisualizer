using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.src.Primitives;
using System.Numerics;

namespace HeightmapVisualizer.src.Shapes
{
    public static class Heightmap
    {
        public static Mesh[,] CreateCentered(Vector3 position, float[,] values, float boxSize, Color? color = null, bool drawWireframe = false)
        {
            return CreateCorners(position / 2, values, boxSize, color, drawWireframe);
        }

        public static Mesh[,] CreateCorners(Vector3 position, float[,] values, float boxSize, Color? color = null, bool drawWireframe = false)
        {
            Mesh[,] map3D = new Mesh[values.GetLength(0), values.GetLength(1)];

            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    var boxPos = new Vector3(i * boxSize * boxSize, 0, j * boxSize * boxSize) + position;
                    map3D[i, j] = Cuboid.CreateCorners(boxPos, new Vector3(boxSize, -values[i, j], boxSize), color, drawWireframe);
                }
            }
            return map3D;
        }

        public static Mesh Combine(Mesh[,] mesh)
        {
            var array = MeshUtility.Convert2DArrayTo1DArray(mesh);

            Mesh combined = array[0];

            for (int i = 1; i < array.GetLength(0); i++)
            {
                combined = MeshUtility.CombineGeometry(combined, array[i]);
            }

            return combined;
        }
    }
}
