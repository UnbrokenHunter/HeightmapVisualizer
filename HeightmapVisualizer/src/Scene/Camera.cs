using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.src;
using HeightmapVisualizer.src.Shapes;
using HeightmapVisualizer.src.Utilities;
using System.Numerics;

namespace HeightmapVisualizer.Scene
{
    public class Camera : Gameobject
    {
		public override Mesh? GetRenderable()
        {
            var debug = false;
            if (debug)
            {
                var combined = MeshUtility.CombineGeometry(
                    MeshUtility.CombineGeometry(
                        Line.CreateRay(Transform.Position, Transform.Forward, 5, Color.CornflowerBlue),
                        Line.CreateRay(Transform.Position, Transform.Up, 5, Color.Green)
                    ),
                    Line.CreateRay(Transform.Position, Transform.Right, 5, Color.Red)
                );
                return combined;
            }
            return null;
        }
    }
}
