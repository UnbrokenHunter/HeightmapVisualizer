
using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.Rendering;
using HeightmapVisualizer.Units;
using HeightmapVisualizer.Utilities;
using System.Drawing;

namespace HeightmapVisualizer.Scene
{
    public class Scene
    {
        public Camera Camera { get; set; }
        public Gameobject[] Gameobjects { get; set; }

        public Scene(Camera camera, Gameobject[] gameobjects)
        {
            this.Camera = camera;
            this.Gameobjects = gameobjects;
        }

        public void Init()
        {
            foreach (var gameobject in Gameobjects)
            {
                gameobject.Init();
            }
        }

        public void Update(Graphics g)
        {
            UpdateGameobjects();

            RenderCamera(g);
        }

        private void UpdateGameobjects()
        {
            foreach (var gameobject in Gameobjects)
            {
                gameobject.Update();
            }
        }

        private void RenderCamera(Graphics g)
        {
            List<Renderable> renderOrder = new List<Renderable>();

            foreach (var gameobject in Gameobjects)
            {
                var mesh = gameobject.GetRenderable();
                if (mesh != null)
                {
                    var renderables = mesh.PointsToRender();

                    foreach (var renderable in renderables)
                    {
                        // Cull Objects off screen
                        if (renderable.GetOrCalculateScreenPosition(Camera).All(e => e.Item2 == false))
                            continue;


                        renderOrder.Add(renderable);
                    }
                }
            }

            // Draw the furthest first, and draw nearer ones on top
            Renderable[] toRender = renderOrder.OrderBy(e => -e.GetOrCalculateDistance(Camera).Min(e => e)).ToArray();

            for (int i = 0; i < toRender.Length; i++)
            {
                Renderable renderable = toRender[i];

                PointF pF(Vector2 v) => new PointF(v.x, v.y);
                var screenPositions = renderable.GetOrCalculateScreenPosition(Camera);
                var p = new PointF[] { pF(screenPositions[0].Item1), pF(screenPositions[1].Item1), pF(screenPositions[2].Item1) };

                if (renderable.DrawingMode == DrawingMode.Lines)
                    g.DrawPolygon(ColorLookup.FindOrGetPen(renderable.Tri.GetColor()), p);
                if (renderable.DrawingMode == DrawingMode.Faces)
                    g.FillPolygon(ColorLookup.FindOrGetBrush(renderable.Tri.GetColor()), p);


            }
        }
    }
}
