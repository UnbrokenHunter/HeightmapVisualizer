using HeightmapVisualizer.src.Components;
using HeightmapVisualizer.src.Primitives;
using System.Numerics;

namespace HeightmapVisualizer.src.Factories
{
    public static class Heightmap
    {
        public static MeshComponent[,] CreateCorners(float[,] values, float boxSize)
        {
            MeshComponent[,] map3D = new MeshComponent[values.GetLength(0), values.GetLength(1)];

            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    var boxPos = new Vector3(i * boxSize * boxSize, 0, j * boxSize * boxSize);
                    map3D[i, j] = Cuboid.CreateCorners(new Vector3(boxSize, -values[i, j], boxSize));
                }
            }
            return map3D;
        }

        //public static MeshComponent Combine(MeshComponent[,] mesh)
        //{
        //    var array = MeshUtility.Convert2DArrayTo1DArray(mesh);

        //    MeshComponent combined = array[0];

        //    for (int i = 1; i < array.GetLength(0); i++)
        //    {
        //        combined = MeshUtility.CombineGeometry(combined, array[i]);
        //    }

        //    return combined;
        //}
    }
}
