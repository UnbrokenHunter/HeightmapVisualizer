using System.Numerics;

namespace HeightmapVisualizer.src.UI
{
	public abstract class UIElement
	{
		protected string ID { get; set; }

		protected Vector2 position1;
		protected Vector2 size;

		public UIElement(Vector2 position, Vector2 size, string id = "") 
		{
			this.position1 = position;
			this.size = size;
			this.ID = id;
		}

		public abstract void Draw(Graphics g);

		public static UIElement[] FindElementsByID(UIElement[] elements, string id)
		{
			var matches = elements.AsParallel().Where(o => o.ID.Contains(id)).ToList();
			return matches.ToArray();
		}
	}
}
