

using HeightmapVisualizer.Units;
using System.Numerics;

namespace HeightmapVisualizer.Primitives
{
    public static class MeshUtility
    {
		/// <summary>
		/// Converts a 2D array of objects into a 1D array of objects
		/// </summary>
		/// <param name="array">The 2D Array of objects to convert</param>
		/// <returns></returns>
		public static Mesh[] Convert2DArrayTo1DArray(Mesh[,] array)
		{
			var objs = new Mesh[array.GetLength(0) * array.GetLength(1)];
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

		/// <summary>
		/// Combines two mesh Geometries into a single mesh object. 
		/// </summary>
		/// <param name="m1"></param>
		/// <param name="m2"></param>
		/// <returns>A Mesh Object with combined geometry</returns>
		public static Mesh CombineGeometry(Mesh m1, Mesh m2)
        {
            
            var faces1 = new List<Face>();

            foreach (Tri tri in m1.Tris)
            {
				Vector3[] points = new Vector3[3] { tri.Points[0].Position, tri.Points[1].Position, tri.Points[2].Position };
                faces1.Add(new Face(points, tri.GetColor()));
            }

            var faces2 = new List<Face>();

            foreach (Tri tri in m2.Tris)
            {
				Vector3[] points = new Vector3[3] { tri.Points[0].Position, tri.Points[1].Position, tri.Points[2].Position };
				faces2.Add(new Face(points, tri.GetColor()));
            }

            var faces = faces1.Concat(faces2);

            return new Mesh(faces.ToArray());
        }
    }
}
