
namespace HeightmapVisualizer.Scene
{
	internal class Scene
	{
		public Camera camera { get; set; }
		public Gameobject[] gameobjects { get; set; }

		public Scene(Camera camera, Gameobject[] gameobjects) 
		{
			this.camera = camera;
			this.gameobjects = gameobjects;
		}

		private void UpdateGameobjects()
		{
			foreach (var gameobject in gameobjects)
			{
				gameobject.Update();
			}
		}

		private void RenderCamera(Graphics g)
		{
			foreach (var gameobject in gameobjects)
			{
				var renderable = gameobject.GetRenderable();
				if (renderable != null)
				{
					renderable.Render(g, camera);
				}
			}
		}

		public void Init()
		{
			foreach (var gameobject in gameobjects)
			{ 
				gameobject.Init(); 
			}
		}

		public void Update(Graphics g)
		{
			UpdateGameobjects();

			RenderCamera(g);
		}

	}
}
