using System.Numerics;
using HeightmapVisualizer.Controls;
using HeightmapVisualizer.Player;

namespace HeightmapVisualizer
{
    internal class Menu
	{
		public Menu() 
		{
			new UI.Button("Text", new Vector2(0, 0), new Vector2(60, 60), (button) =>
			{
				Window.GetInstance().Invalidate();
			});
		}
	}
}
