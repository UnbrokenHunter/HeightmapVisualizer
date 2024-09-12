
namespace HeightmapVisualizer
{
	internal class Window : Form
	{
		public Window()
		{
			// Set up form properties
			this.Text = "Drawing Example";
			this.Width = 16 * 100;
			this.Height = 9 * 100;

			AllocConsole();
		}

		[System.Runtime.InteropServices.DllImport("kernel32.dll")]
		private static extern bool AllocConsole();

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
