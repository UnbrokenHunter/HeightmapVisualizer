
using HeightmapVisualizer.Units;
using HeightmapVisualizer.Utilities;
using System.Numerics;

namespace HeightmapVisualizer.UI
{
	class DebugText
	{
		private static List<DebugText> texts = new List<DebugText>();

		public string text;
		public Vector2 position1;

		// Constructor for the Button class
		public DebugText(string text, Vector2 position)
		{
			texts.Add(this);

			this.text = text;
			position1 = position;
		}

		public static void Draw(Graphics g)
		{
			Font font = new Font("Arial", 13f);
			Brush brush = ColorLookup.FindOrGetBrush(Color.Black);

			foreach (DebugText b in texts)
			{
				g.DrawString(b.text, font, brush, b.position1.X, b.position1.Y);
			}
			texts.Clear();
		}
	}
}
