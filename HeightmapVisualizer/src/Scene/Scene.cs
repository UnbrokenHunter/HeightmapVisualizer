using HeightmapVisualizer.src.Components;
using HeightmapVisualizer.src.UI;
using System.Numerics;

namespace HeightmapVisualizer.src.Scene
{
    public class Scene
    {
        public (Gameobject, CameraBase) Camera { get; set; }
        public Gameobject[] Gameobjects { get; set; }
        public UIElement[] UIElements { get; set; }

        public Scene(Gameobject[] gameobjects, UIElement[] ui)
        {
            // Find all Cameras
            List<(Gameobject, CameraBase)> cams = new();
            foreach (var gameobj in gameobjects)
            {
                foreach (var component in gameobj.Components)
                {
                    if (component is CameraBase @base) 
                        cams.Add((gameobj, @base));
                }
            }

            // If no cameras are present, add a default one
            if (cams.Count > 0)
            {
                // Create object
                var cameraObject = new Gameobject();

                // Create Perspective Camera Component
                var cameraComponent = new PerspectiveCameraComponent(Window.Instance.Bounds);

                // Add Component to Object
                cameraObject.AddComponent(cameraComponent);

                // Add new Camera to gameobjects in scene
                var g = gameobjects.ToList();
                g.Add(cameraObject);
                gameobjects = g.ToArray();

                // Add Camera to Cameras List
                cams.Add((cameraObject, cameraComponent));
            }

            // Select the first camera found
            var camera = cams[0];
            foreach (var cam in  cams)
            {
                if (cam.Item2.Priority > camera.Item2.Priority) 
                    camera = cam;
            }

            Camera = camera;
            Gameobjects = gameobjects;
            UIElements = ui;
        }

        public void Update()
        {
            UpdateGameobjects();
        }

        public void Render(Graphics g)
        {
            RenderCamera(g);

            RenderUI(g);
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
            if (Camera.Item1 == null || Camera.Item2 == null)
                return;

            List<Tuple<float, MeshComponent>> renderOrder = new();
            Gameobject[] hasMesh = Gameobjects.Where(e => e.Components.Any(c => c is MeshComponent)).ToArray();

            foreach (var gameobject in hasMesh) 
            {

                if (gameobject.Components.FirstOrDefault(c => c is MeshComponent) is MeshComponent meshComponent)
                {
                    var distance = Vector3.Distance(Camera.Item1.Transform.Position, gameobject.Transform.Position);
                    renderOrder.Add(new Tuple<float, MeshComponent>(distance, meshComponent));
                }
            }

            // Draw the furthest first, and draw nearer ones on top
            renderOrder.OrderBy(e => -e.Item1).ToList().ForEach(e => e.Item2.Render(g, Camera.Item2));
        }

        private void RenderUI(Graphics g)
        {
            foreach (var ui in UIElements)
            {
                ui.Update();
                ui.Draw(g);
            }
        }
    }
}
