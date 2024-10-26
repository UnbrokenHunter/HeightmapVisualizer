using System.Numerics;

namespace HeightmapVisualizer.src.UI
{
	public abstract class UIElement
	{
		protected string ID { get; set; }

		protected Vector2 position;
		protected Vector2 size;
		protected Action<UIElement>? ScriptableUpdate { get; private set; }


		public UIElement(Vector2 position, Vector2 size, string id = "", Action<UIElement>? update = null) 
		{
			this.position = position;
			this.size = size;
			this.ID = id;

			this.ScriptableUpdate = update;
		}

		public abstract void Draw(Graphics g);

		public void Update()
		{
			if (ScriptableUpdate != null)
			{
				ScriptableUpdate(this);  
			}
		}

		public static UIElement[] FindElementsByID(UIElement[] elements, string id)
		{
			var matches = elements.AsParallel().Where(o => o.ID.Contains(id)).ToList();
			return matches.ToArray();
		}
	}
}
