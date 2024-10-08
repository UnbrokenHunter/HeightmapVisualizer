
using System.Drawing.Drawing2D;

namespace HeightmapVisualizer.Primitives
{
    public static class MeshUtility
    {
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
                faces1.Add(new Face(tri.Points, tri.GetColor()));
            }

            var faces2 = new List<Face>();

            foreach (Tri tri in m2.Tris)
            {
                faces2.Add(new Face(tri.Points, tri.GetColor()));
            }

            var faces = faces1.Concat(faces2);

            return new Mesh(faces.ToArray());
        }
    }
}
