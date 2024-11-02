using HeightmapVisualizer.src.Components;
using HeightmapVisualizer.src.Scene;
using System.Numerics;

namespace HeightmapVisualizer.src.Rendering
{
	internal class Renderer
	{
		private Bitmap bitmap { get; set; }

		public Renderer(int width, int height) 
		{
			bitmap = new Bitmap(width, height);	
		}

		private void ClearBitmap()
		{
			using (Graphics g = Graphics.FromImage(bitmap))
			{
				g.Clear(Color.White); // Clear to a solid color
			}
		}

		public Bitmap Render((Gameobject, CameraBase) camera, Gameobject[] objects)
		{
			ClearBitmap();

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
			renderOrder.OrderBy(e => -e.Item1).ToList().ForEach(e => RenderMesh(bitmap, ProjectPoints(e.Item2.Renderable(), camera)));

			return bitmap;
		}

		private static ((Vector2, bool), (Vector2, bool), (Vector2, bool), Color, bool)[] ProjectPoints((Vector3, Vector3, Vector3, Color, bool)[] mesh, (Gameobject, CameraBase) camera)
		{
			var points = new ((Vector2, bool), (Vector2, bool), (Vector2, bool), Color, bool)[mesh.Length];

            for (int i = 0; i < mesh.Length; i++)
			{
                (Vector3, Vector3, Vector3, Color, bool) part = mesh[i];
                var bounds = Window.Instance.Bounds;

				points[i] = (
                    camera.Item2.ProjectPoint(part.Item1, bounds),
                    camera.Item2.ProjectPoint(part.Item2, bounds), 
					camera.Item2.ProjectPoint(part.Item3, bounds),
					part.Item4,
					part.Item5
				);
			}
			return points;
        }

        private static void RenderMesh(Bitmap bitmap, ((Vector2, bool), (Vector2, bool), (Vector2, bool), Color, bool)[] mesh)
		{
			foreach (var part in mesh)
			{
				if (part.Item1.Item2 || part.Item2.Item2 || part.Item3.Item2)
				{
					DrawTriangle();
				}

				void DrawTriangle()
				{
					Bresenham(bitmap, (int)part.Item1.Item1.X, (int)part.Item1.Item1.Y, (int)part.Item2.Item1.X, (int)part.Item2.Item1.Y, part.Item4);
					Bresenham(bitmap, (int)part.Item2.Item1.X, (int)part.Item2.Item1.Y, (int)part.Item3.Item1.X, (int)part.Item3.Item1.Y, part.Item4);
					Bresenham(bitmap, (int)part.Item3.Item1.X, (int)part.Item3.Item1.Y, (int)part.Item1.Item1.X, (int)part.Item1.Item1.Y, part.Item4);
				}
			}
		}

		private static void Bresenham(Bitmap bitmap, int x, int y, int x2, int y2, Color color)
		{
			int w = x2 - x;
			int h = y2 - y;
			int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
			if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
			if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
			if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
			int longest = Math.Abs(w);
			int shortest = Math.Abs(h);
			if (!(longest > shortest))
			{
				longest = Math.Abs(h);
				shortest = Math.Abs(w);
				if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
				dx2 = 0;
			}
			int numerator = longest >> 1;
			for (int i = 0; i <= longest; i++)
			{
				// Only draw it to the screen if it is on the screen
				if (bitmap.Width > x &&  bitmap.Height > y && 0 <= x && 0 <= y)
					bitmap.SetPixel(x, y, color);
				
				numerator += shortest;
				if (!(numerator < longest))
				{
					numerator -= longest;
					x += dx1;
					y += dy1;
				}
				else
				{
					x += dx2;
					y += dy2;
				}
			}
		}
	}
}
