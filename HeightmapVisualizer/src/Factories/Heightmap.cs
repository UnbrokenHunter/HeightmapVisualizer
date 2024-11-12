using HeightmapVisualizer.src.Components;
using HeightmapVisualizer.src.Scene;
using System.Numerics;

namespace HeightmapVisualizer.src.Factories
{
    internal static class Heightmap
    {
        public static Gameobject[,] CreateCorners(float[,] values, float boxSize, Vector3? position = null)
        {
            Vector3 offset = position ?? Vector3.Zero;

            Gameobject[,] map3D = new Gameobject[values.GetLength(0), values.GetLength(1)];

            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    var localBoxPos = new Vector3(i * boxSize * boxSize, 0, j * boxSize * boxSize);
                    map3D[i, j] = new Gameobject(localBoxPos + offset)
                        .AddComponent(
                            new MeshComponent(Cuboid.CreateCorners(new Vector3(boxSize, -values[i, j], boxSize))));
                }
            }
            return map3D;
        }

        // Needs to accuratly account for the final size of the heightmap
        //public static Gameobject[,] CreateCentered(float[,] values, float boxSize)
        //{
        //    Gameobject[,] map3D = new Gameobject[values.GetLength(0), values.GetLength(1)];

        //    for (int i = 0; i < values.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < values.GetLength(1); j++)
        //        {
        //            var boxPos = new Vector3(i * boxSize * boxSize, 0, j * boxSize * boxSize);
        //            map3D[i, j] = new Gameobject(boxPos).AddComponent(Cuboid.CreateCorners(new Vector3(boxSize, -values[i, j], boxSize)));
        //        }
        //    }
        //    return map3D;
        //}

        /// <summary>
        /// Converts a 2D array of objects into a 1D array of objects
        /// </summary>
        /// <param name="array">The 2D Array of objects to convert</param>
        /// <returns></returns>
        public static Gameobject[] Convert2DArrayTo1DArray(Gameobject[,] array)
        {
            var objs = new Gameobject[array.GetLength(0) * array.GetLength(1)];
            for (var i = 0; i < array.GetLength(0); i++)
            {
                for (var j = 0; j < array.GetLength(1); j++)
                {
                    var index = array.GetLength(1) * i + j;
                    objs[index] = array[i, j];
                }
            }
            return objs;
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
