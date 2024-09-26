using System.Numerics;
using HeightmapVisualizer.Controls;
using HeightmapVisualizer.Scene;

namespace HeightmapVisualizer
{
    internal class Menu
	{
		/*
		private UI.Button x;
		private UI.Button y;
		private UI.Button z;
		private UI.Button pitch;
		private UI.Button yaw;
		private UI.Button fov;

		public Menu()
		{
			new UI.Button("Refresh", new Vector2(0, 0), new Vector2(100, 60), (button) =>
			{
				Window.GetInstance().Invalidate();
			});

			x = new UI.Button("Position", new Vector2(0, 60), new Vector2(100, 60), (button) => { });
			y = new UI.Button("Position", new Vector2(0, 120), new Vector2(100, 60), (button) => { });
			z = new UI.Button("Position", new Vector2(0, 180), new Vector2(100, 60), (button) => { });

			pitch = new UI.Button("Rotation", new Vector2(0, 240), new Vector2(100, 60), (button) => { });
			yaw = new UI.Button("Rotation", new Vector2(0, 300), new Vector2(100, 60), (button) => { });

			fov = new UI.Button("FOV", new Vector2(0, 360), new Vector2(100, 60), (button) => { });

		}

		public void Update()
		{
			while (true)
			{
				x.text = $"X:{Camera.Instance.position.X}";
				y.text = $"Y:{Camera.Instance.position.Y}";
				z.text = $"Z:{Camera.Instance.position.Z}";

				pitch.text = $"Quat:{Camera.Instance.rotation}";
				yaw.text = $"Euler:{Camera.Instance.EulerRotation()}";

				fov.text = $"FOV:{Camera.Instance.fov}";

				Thread.Sleep(60);
			}
		}
		*/
	}
}
