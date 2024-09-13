using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HeightmapVisualizer
{
	internal class DrawDebug
	{
		public static void Draw(PaintEventArgs e, Graphics g, Heightmap hm)
		{
			if (e == null || g == null || hm == null) return;

			Pen blackPen = new Pen(Color.Black);

			// Draw Point at (0, 0, 0)
			DrawCircleAroundPoint(g, Vector3.Zero, 5, blackPen);
			DrawCircleAroundPoint(g, Vector3.Zero, 10, blackPen);
			DrawCircleAroundPoint(g, Vector3.Zero, 15, blackPen);

			for (int i = 0; i < 20; i++)
			{
				DrawPerspectiveLines(g, 10 * i, 30f);
			}
		}

		private static void DrawCircleAroundPoint(Graphics g, Vector3 position, float radius, Pen pen)
		{
			var originProjection = Camera.Instance.ProjectVertex(position);
			g.DrawEllipse(pen, originProjection.X - radius / 2, originProjection.Y - radius / 2, radius, radius);

		}

		private static void DrawPerspectiveLines(Graphics g, float distance, float offset)
		{
			Camera cam = Camera.Instance;

			Edge[] edges = new Edge[] {
				new Edge(
					new Vertex(cam.MovePointInCameraDirection(new Vector3(cam.position.X - offset,      cam.position.Y - offset,     cam.position.Z), distance)),
					new Vertex(cam.MovePointInCameraDirection(new Vector3(cam.position.X - offset,		cam.position.Y + offset,     cam.position.Z), distance))
				),
				new Edge(
					new Vertex(cam.MovePointInCameraDirection(new Vector3(cam.position.X + offset,		cam.position.Y - offset,     cam.position.Z), distance)),
					new Vertex(cam.MovePointInCameraDirection(new Vector3(cam.position.X + offset,		cam.position.Y + offset,     cam.position.Z), distance))
				),
				new Edge(
					new Vertex(cam.MovePointInCameraDirection(new Vector3(cam.position.X + offset,		cam.position.Y + offset,     cam.position.Z), distance)),
					new Vertex(cam.MovePointInCameraDirection(new Vector3(cam.position.X - offset,		cam.position.Y + offset,     cam.position.Z), distance))
				),
				new Edge(
					new Vertex(cam.MovePointInCameraDirection(new Vector3(cam.position.X + offset,		cam.position.Y - offset,     cam.position.Z), distance)),
					new Vertex(cam.MovePointInCameraDirection(new Vector3(cam.position.X - offset,		cam.position.Y - offset,     cam.position.Z), distance))
				),
			};

			var pen = new Pen(Color.FromArgb(255, (int) Math.Clamp(distance, 0f, 255f), 0, 0));
			foreach (var edge in edges)
			{
				edge.DrawEdge(g, pen);
			}

		}
	}
}
