
namespace HeightmapVisualizer
{
	internal class Window : Form
	{
		public Window()
		{
			// Set up form properties
			this.Text = "Drawing Example";
			this.Width = 400;
			this.Height = 300;
		}

		// Override the OnPaint method to perform custom drawing
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			// Get the Graphics object to draw on the form
			Graphics g = e.Graphics;

			Heightmap hm = new CreateHeightmap(30, 30).Map;

			DrawHeightmap.Draw(e, g, hm);
		}
	}
}
