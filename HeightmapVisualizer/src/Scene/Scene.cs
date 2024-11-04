using HeightmapVisualizer.src.Components;
using HeightmapVisualizer.src.Rendering;
using HeightmapVisualizer.src.UI;
using HeightmapVisualizer.src.Utilities;
using System;
using System.Numerics;

namespace HeightmapVisualizer.src.Scene
{
    public class Scene
    {
        private Renderer renderer {  get; set; }
        public (Gameobject, Camera) Camera { get; set; }
        public Gameobject[] Gameobjects { get; set; }
        public UIElement[] UIElements { get; set; }

        public Scene(Gameobject[] gameobjects, UIElement[] ui)
        {
            Gameobjects = gameobjects;
            UpdateSelectedCamera();
            UIElements = ui;
            renderer = new Renderer(Window.Instance.Width, Window.Instance.Height);
        }

        public void Update()
        {
            UpdateGameobjects();
        }

        public void Render(Graphics g)
        {
            g.DrawImage(renderer.Render(Camera, Gameobjects), 0, 0);

            RenderUI(g);
        }

        private void UpdateGameobjects()
        {
            foreach (var gameobject in Gameobjects)
            {
                gameobject.Update();
            }
        }

        public void UpdateSelectedCamera()
        {
			// Find all Cameras
			List<(Gameobject, Camera)> cams = new();
			foreach (var gameobj in Gameobjects)
			{
                if (gameobj.TryGetComponents<Camera>(out IComponent[] cam) > 0)
				    cams.Add((gameobj, (Camera)cam[0]));
			}

			// If no cameras are present, add a default one
			if (cams.Count <= 0)
			{
				// Create object
				var cameraObject = new Gameobject();

				// Create Perspective Camera Component
				var cameraComponent = new PerspectiveCameraComponent();

				// Add Component to Object
				cameraObject.AddComponent(cameraComponent);

				// Add new Camera to gameobjects in scene
				var g = Gameobjects.ToList();
				g.Add(cameraObject);
				Gameobjects = g.ToArray();

				// Add Camera to Cameras List
				cams.Add((cameraObject, cameraComponent));
			}

			// Select the first camera found
			var camera = cams[0];
			foreach (var cam in cams)
			{
				if (cam.Item2.Priority > camera.Item2.Priority)
					camera = cam;
			}

			Camera = camera;
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
