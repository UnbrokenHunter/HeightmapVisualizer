using HeightmapVisualizer.src.Components;
using HeightmapVisualizer.src.Scene;
using HeightmapVisualizer.src.Utilities;
using System.Numerics;

namespace HeightmapVisualizer.src.Rendering
{
	internal class Renderer
	{
		public static void Render((Gameobject, CameraBase) camera, Gameobject[] objects, Graphics g)
		{
			if (camera.Item1 == null || camera.Item2 == null)
				return;

			List<Tuple<float, MeshComponent>> renderOrder = new();

			// Get all meshes
			Gameobject[] hasMesh = objects.Where(e => e.Components.Any(c => c is MeshComponent)).ToArray();

			foreach (var gameobject in hasMesh)
			{

				// Get the Mesh component on gameobject
				if (gameobject.Components.FirstOrDefault(c => c is MeshComponent) is MeshComponent meshComponent)
				{
					// Calculates the distance between camera and the transform's position
					var distance = Vector3.Distance(camera.Item1.Transform.Position, gameobject.Transform.Position);
					renderOrder.Add(new Tuple<float, MeshComponent>(distance, meshComponent));
				}
			}

			// Draw the furthest first, and draw nearer ones on top
			renderOrder.OrderBy(e => -e.Item1).ToList().ForEach(e => RenderMesh(g, e.Item2.Renderable(), camera));
		}

		private static void RenderMesh(Graphics g, (Vector3, Vector3, Vector3, Color, bool)[] mesh, (Gameobject, CameraBase) camera)
		{
			foreach (var part in mesh)
			{
				var bounds = Window.Instance.Bounds;

				var p1 = camera.Item2.ProjectPoint(part.Item1, bounds);
				var p2 = camera.Item2.ProjectPoint(part.Item2, bounds);
				var p3 = camera.Item2.ProjectPoint(part.Item3, bounds);

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
	}
}
