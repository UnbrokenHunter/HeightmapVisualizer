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

        [MethodTimer.Time]
        public Bitmap Render((Gameobject, CameraBase) camera, Gameobject[] objects)
		{
			ClearBitmap();

			List<(float, ((Vector2, bool), (Vector2, bool), (Vector2, bool), Color, bool)[])> renderOrder = new();

			// Get all meshes
			List<MeshComponent> meshes = new();
			foreach (var obj in objects)
			{
				if (obj.TryGetComponents<MeshComponent>(out IComponent[] m) > 0)
				{
					// Calculates the distance between camera and the transform's position
					var distance = Vector3.Distance(camera.Item1.Transform.Position, obj.Transform.Position);
					var points = (distance, ProjectPoints(((MeshComponent)m[0]).Renderable(), camera));
                    renderOrder.Add(points);
                }
            }

			// Draw the furthest first, and draw nearer ones on top
			renderOrder.OrderBy(e => -e.Item1).ToList().ForEach(e => RenderMesh(bitmap, e.Item2));

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

        private static void Bresenham(Bitmap bitmap, int x1, int y1, int x2, int y2, Color color)
		{
			// Point 1 is outside of bitmap
            if (!(x1 >= 0 && x1 < bitmap.Width 
				&& y1 >= 0 && y1 < bitmap.Height))
			{        
				// Swaps starting point
                // Will draw line starting with point on screen, until no longer on screen
                var tempX = x1;
				var tempY = y1;
				x1 = x2;
				y1 = y2;
				x2 = tempX;
                y2 = tempY;
			}

            int w = x2 - x1;
			int h = y2 - y1;
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
				if (bitmap.Width > x1 && bitmap.Height > y1 && 0 <= x1 && 0 <= y1)
					bitmap.SetPixel(x1, y1, color);
				else
					break;

				numerator += shortest;
				if (!(numerator < longest))
				{
					numerator -= longest;
					x1 += dx1;
					y1 += dy1;
				}
				else
				{
					x1 += dx2;
					y1 += dy2;
				}
			}
		}
	}
}
