
using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.Scene;
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer.Rendering
{
    public sealed class Renderable
    {
        public DrawingMode DrawingMode { get; private set; }
        public Tri Tri { get; private set; }

        private Tuple<Vector2, bool>[] ScreenPosition = null;
        private float[] Distance = null;

        public float[] GetOrCalculateDistance(Camera cam) 
        {
            if (Distance == null)
                Distance = new float[3] { Vector3.Distance(cam.Transform.Position, Tri.Points[0].Position), Vector3.Distance(cam.Transform.Position, Tri.Points[1].Position), Vector3.Distance(cam.Transform.Position, Tri.Points[2].Position) };
            return Distance; 
        }

        public Tuple<Vector2, bool>[] GetOrCalculateScreenPosition(Camera cam)
        {
            if (ScreenPosition == null)
                ScreenPosition = new Tuple<Vector2, bool>[3] { cam.ProjectPoint(Tri.Points[0].Position), cam.ProjectPoint(Tri.Points[1].Position), cam.ProjectPoint(Tri.Points[2].Position) };
            return ScreenPosition;                
        }

        public Renderable(DrawingMode drawingMode, Tri tri)
        {
            DrawingMode = drawingMode;
            Tri = tri;
        }
    }
}
