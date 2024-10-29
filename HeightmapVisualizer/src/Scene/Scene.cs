using HeightmapVisualizer.src.Components;
using HeightmapVisualizer.src.UI;
using HeightmapVisualizer.src.Utilities;
using System;
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
            Gameobjects = gameobjects;
            UpdateSelectedCamera();
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

        public void UpdateSelectedCamera()
        {
			// Find all Cameras
			List<(Gameobject, CameraBase)> cams = new();
			foreach (var gameobj in Gameobjects)
			{
                if (gameobj.TryGetComponents<CameraBase>(out IComponent[] cam) > 0)
				    cams.Add((gameobj, (CameraBase)cam[0]));
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

        private void RenderCamera(Graphics g)
        {
            if (Camera.Item1 == null || Camera.Item2 == null)
                return;

            List<Tuple<float, MeshComponent>> renderOrder = new();
            
            // Get all meshes
            Gameobject[] hasMesh = Gameobjects.Where(e => e.Components.Any(c => c is MeshComponent)).ToArray();

            foreach (var gameobject in hasMesh)
            {

                // Get the Mesh component on gameobject
                if (gameobject.Components.FirstOrDefault(c => c is MeshComponent) is MeshComponent meshComponent)
                {
                    // Calculates the distance between camera and the transform's position
                    var distance = Vector3.Distance(Camera.Item1.Transform.Position, gameobject.Transform.Position);
                    renderOrder.Add(new Tuple<float, MeshComponent>(distance, meshComponent));
                }
            }

            // Draw the furthest first, and draw nearer ones on top
            renderOrder.OrderBy(e => -e.Item1).ToList().ForEach(e => RenderMesh(g, e.Item2.Renderable()));
        }
		private void RenderMesh(Graphics g, (Vector3, Vector3, Vector3, Color, bool)[] mesh) 
        {
            foreach (var part in mesh)
            {
				var bounds = Window.Instance.Bounds;

				var p1 = Camera.Item2.ProjectPoint(part.Item1, bounds);
			    var p2 = Camera.Item2.ProjectPoint(part.Item2, bounds);
			    var p3 = Camera.Item2.ProjectPoint(part.Item3, bounds);

                // Atleast one point on screen
                if (p1.Item2 || p2.Item2 || p3.Item2)
                {
                    var p = new PointF[] {
                        p1.Item1,
                        p2.Item1,
                        p3.Item1
                };


                    // Fill Tri
                    if (!part.Item5)
                        g.FillPolygon(ColorLookup.FindOrGetBrush(part.Item4), p);


                    g.DrawPolygon(ColorLookup.FindOrGetPen(part.Item4), p);
                }
			}
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
