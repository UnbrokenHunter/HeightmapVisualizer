
using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.src;
using HeightmapVisualizer.src.Components;
using System.Numerics;

namespace HeightmapVisualizer.Scene
{
    public class Scene
    {
        public CameraComponent Camera { get; set; }
		public Gameobject[] Gameobjects { get; set; }

        public Scene(Gameobject[] gameobjects)
        {
            // Find all Cameras
            List<CameraComponent> cams = new List<CameraComponent>();
            foreach (var objs in gameobjects)
            {
                foreach (var component in objs.Components)
                {
                    if (component is CameraComponent) cams.Add((CameraComponent)component);
                }
            }

            // If no cameras are present, add a default one
            if (cams.Count > 0)
            {
                // Create object
                var cameraObject = new Gameobject();

                // Create Camera Component
                var cameraComponent = new CameraComponent(Window.Instance.Bounds);

                // Add Component to Object
				cameraObject.AddComponent(cameraComponent);

                // Add new Camera to gameobjects in scene
                var g = gameobjects.ToList();
                g.Add(cameraObject);
                gameobjects = g.ToArray();

                // Add Camera to Cameras List
                cams.Add(cameraComponent);
            }

            // Select the first camera found
            var camera = cams[0];

            this.Camera = camera;
            this.Gameobjects = gameobjects;
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
                    var distance = Vector3.Distance(Camera.Gameobject.Transform.Position, renderable.Transform.Position);
                    renderOrder.Add(new Tuple<float, Mesh>(distance, renderable));
                }
            }

            // Draw the furthest first, and draw nearer ones on top
            renderOrder.OrderBy(e => -e.Item1).ToList().ForEach(e => e.Item2.Render(g, Camera)); 

        }
    }
}
