using HeightmapVisualizer.src.Utilities;
using System.Numerics;

namespace HeightmapVisualizer.src.UI
{
	class DebugText : UIElement
	{

		public string text;

		public DebugText(Vector2 position, Vector2 size, string text, string id = "") : base(position, size, id)
        {
            this.text = text;
        }

        public override void Draw(Graphics g)
        {
            Font font = new Font("Arial", 13f);
            Brush brush = ColorLookup.FindOrGetBrush(Color.Black);

            g.DrawString(text, font, brush, position1.X, position1.Y);
        }
    }
}
