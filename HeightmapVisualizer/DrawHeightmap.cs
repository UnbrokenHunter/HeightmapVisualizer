using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HeightmapVisualizer
{
	internal class DrawHeightmap
	{
		public static void Draw(PaintEventArgs e, Graphics g, Heightmap hm)
		{
			if (e == null || g == null || hm == null) return;

			Camera cam = new Camera(new Vector3(-3, -1, -0.01f), e.ClipRectangle, 16f/9f, 90f, 0.001f, 0, 0);
			//Cuboid[,] hm3D = hm.Map3D();

//			for (int i = 0; i < hm.Map.GetLength(0); i++)
//			{
//				for (int j = 0; j < hm.Map.GetLength(1); j++)
//				{
					Cuboid value = new Cuboid(0, 0, 0, -1, 1);// hm3D[0, 0];
					Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));

					foreach (Edge edge in value.edges)
					{
						var vec1 = new Vector3(edge.P1.X, edge.P1.Y, edge.P1.Z);
						var p1 = cam.ProjectVertex(vec1);

						var vec2 = new Vector3(edge.P2.X, edge.P2.Y, edge.P2.Z);
						var p2 = cam.ProjectVertex(vec2);

						g.DrawLine(pen, p1.X, p1.Y, p2.X, p2.Y);
					}

					// Draw Point at (0, 0, 0)
					g.DrawEllipse(pen, cam.ProjectVertex(new Vector3(0, 0, 0)).X, cam.ProjectVertex(new Vector3(0, 0, 0)).Y, 5, 5);

			float o = 10;
			DrawCameraLine(o, o);
			DrawCameraLine(o, -o);
			DrawCameraLine(-o, o);
			DrawCameraLine(-o, -o);

			// Draw Point at ( , , )
			void DrawCameraLine(float offsetX, float offsetY)
			{
				var camLinePen = new Pen(Color.FromArgb(255, 255, 0, 0));
				var camPos = new Vector3(cam.position.X + offsetX, cam.position.Y + offsetY, cam.position.Z);
				var camLine1 = cam.ProjectVertex(cam.MovePointInCameraDirection(camPos, 10));
				var camLine2 = cam.ProjectVertex(cam.MovePointInCameraDirection(camPos, 30));

				g.DrawLine(camLinePen, camLine1.X, camLine1.Y, camLine2.X, camLine2.Y);
				g.DrawEllipse(camLinePen, camLine1.X - 2.5f, camLine1.Y - 2.5f, 5, 5);
				g.DrawEllipse(camLinePen, camLine2.X - 2.5f, camLine2.Y - 2.5f, 5, 5);

				camLinePen.Dispose();
			}

			pen.Dispose();
//				}
//			}
		}
	}
}
