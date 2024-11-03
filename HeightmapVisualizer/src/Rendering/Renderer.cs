using HeightmapVisualizer.src.Components;
using HeightmapVisualizer.src.Scene;
using System.Numerics;
using static HeightmapVisualizer.src.Components.MeshComponent;

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

            // Preallocate renderOrder with an estimated capacity
            List<(float, GraphicsPipeline.RenderData[])> renderOrder = new(objects.Length);

            // Process each object without parallelization
            foreach (var obj in objects)
            {
                if (obj.TryGetComponents<MeshComponent>(out IComponent[] m) > 0)
                {
                    // Calculate the distance and project points in one step
                    var meshComponent = (MeshComponent)m[0];
                    float distance = Vector3.Distance(camera.Item1.Transform.Position, obj.Transform.Position);
                    GraphicsPipeline.RenderData[] projectedData = ProjectPoints(meshComponent.Renderable(), camera);

                    renderOrder.Add((distance, projectedData));
                }
            }

            // In-place sort for performance without creating additional lists
            renderOrder.Sort((a, b) => b.Item1.CompareTo(a.Item1));

            GraphicsPipeline.GraphicsPipeline.Render(bitmap, renderOrder);

            return bitmap;
        }

        private static GraphicsPipeline.RenderData[] ProjectPoints(RenderableTri[] mesh, (Gameobject, CameraBase) camera)
		{
			var points = new GraphicsPipeline.RenderData[mesh.Length];

            for (int i = 0; i < mesh.Length; i++)
			{
                RenderableTri part = mesh[i];
                var bounds = Window.Instance.Bounds;

				points[i] = new GraphicsPipeline.RenderData(
					camera.Item2.ProjectPoint(part.P1, bounds),
                    camera.Item2.ProjectPoint(part.P2, bounds), 
					camera.Item2.ProjectPoint(part.P3, bounds),
					part.Color,
					part.IsWireframe
				);
			}
			return points;
        }

	}
}
