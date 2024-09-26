
namespace HeightmapVisualizer.Scene
{
	internal class Scene
	{
		public Camera Camera { get; set; }
		public Gameobject[] Shapes { get; set; }

		public Scene(Camera camera, Gameobject[] shapes) 
		{
			Camera = camera;
			Shapes = shapes;
		}

		public void UpdateShapes()
		{
			foreach (var shape in Shapes)
			{
				shape.Update();
			}
		}

		public void UpdateControllers()
		{
			foreach (var shape in Shapes)
			{
				if (shape.Controller != null)
				{
					shape.Controller.Update(shape.Transform);
				}
			}
		}

	}
}
