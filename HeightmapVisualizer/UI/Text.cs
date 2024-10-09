
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer.UI
{
	class Text
	{
		private static List<Text> texts = new List<Text>();

		public string text;
		public Vector2 position1;
		public Vector2 position2;

		// Constructor for the Button class
		public Text(string text, Vector2 position, Vector2 size)
		{
			texts.Add(this);

			this.text = text;
			position1 = position;
			position2 = position1 + size;
		}

		public static void Draw(Graphics g)
		{
			Pen pen = new Pen(Color.Black);
			Font font = new Font("Arial", 13f);
			Brush brush = new SolidBrush(Color.Black);

			foreach (Text b in texts)
			{
				g.DrawRectangle(pen, (int)b.position1.x, (int)b.position1.y, (int)b.position2.x, (int)b.position2.y);
				g.DrawString(b.text, font, brush, b.position1.x, b.position1.y);
			}
		}
	}
}
