
using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.Units;

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
            List<Tuple<float, Mesh>> renderOrder = new();

            foreach (var gameobject in Gameobjects)
            {
                var renderable = gameobject.GetRenderable();
                if (renderable != null)
                {
                    var distance = Vector3.Distance(Camera.Transform.Position, renderable.Transform.Position);
                    renderOrder.Add(new Tuple<float, Mesh>(distance, renderable));
                }
            }

            // Draw the furthest first, and draw nearer ones on top
            renderOrder.OrderBy(e => -e.Item1).ToList().ForEach(e => e.Item2.Render(g, Camera)); 

        }
    }
}
