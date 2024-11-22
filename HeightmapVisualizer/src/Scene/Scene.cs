using HeightmapVisualizer.src.Components;
using HeightmapVisualizer.src.Components.Camera;
using HeightmapVisualizer.src.Components.Collision;
using HeightmapVisualizer.src.Components.Physics;
using HeightmapVisualizer.src.Rendering;
using HeightmapVisualizer.src.UI;
using HeightmapVisualizer.src.Utilities;
using System;
using System.Numerics;
using static HeightmapVisualizer.src.Scene.Gameobject;

namespace HeightmapVisualizer.src.Scene
{
    internal class Scene
    {
        private Renderer renderer {  get; set; }
        public CameraComponent Camera { get; set; }
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
            UpdateColliders();

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

        private void UpdateColliders()
        {
            var colliders = IDManager.GetObjectsByType<CollisionComponent>();

			for (int i = 0; i < colliders.Count; i++)
            {
                for (int j = i;  j < colliders.Count; j++)
                {
					if (colliders[i].Equals(colliders[j])) continue;

                    colliders[i].ColliderCalculation();
                    colliders[j].ColliderCalculation();

					if (CollisionComponent.AABBIntersect(colliders[i], colliders[j]))
                    {
                        var info1 = new CollisionInfo(colliders[i], colliders[j]);
                        var info2 = new CollisionInfo(colliders[j], colliders[i]);

						colliders[i].Collide(info1);
						colliders[j].Collide(info2);
					}
				}
			}
        }

        public void UpdateSelectedCamera()
        {
            // Find all Cameras
            var cameras = IDManager.GetObjectsByType<CameraComponent>();

			// If no cameras are present, add a default one
			if (cameras.Count <= 0)
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
                cameras.Add(cameraComponent);
			}

			// Select the first camera found
			var camera = cameras[0];
			foreach (var cam in cameras)
			{
				if (cam.Priority > camera.Priority)
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
