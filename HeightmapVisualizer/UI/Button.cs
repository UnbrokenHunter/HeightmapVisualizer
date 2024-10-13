
using HeightmapVisualizer.Controls;
using HeightmapVisualizer.Units;
using HeightmapVisualizer.Utilities;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;

namespace HeightmapVisualizer.UI
{
    class Button
    {
        private static List<Button> buttons = new List<Button>();

		private string text;
        private Vector2 position1;
		private Vector2 size;
		private Action<Button>? onClick;  // Delegate to handle the button click action
		private bool clip;

        // Constructor for the Button class
        public Button(string text, Vector2 position, Vector2 size, Action<Button>? onClick = null, bool clips = true) 
        {
            buttons.Add(this);

            this.text = text;
            this.position1 = position;
            this.size = size;
            this.onClick = onClick;
            this.clip = clips;

            MouseHandler.OnLeftClick += Click;
        }

        public static void Draw(FrameEventArgs args)
        {
            foreach (Button b in buttons)
            {

                GLUtilities.DrawRect(b.position1, b.position1 + b.size.x, Color.Black);

                // Handle clipping if necessary (equivalent of g.SetClip)
                if (b.clip)
                {
                    GL.Enable(EnableCap.ScissorTest);
                    GL.Scissor((int)b.position1.x, (int)(b.position1.y - b.size.y), (int)b.size.x, (int)b.size.y);
                }

                // Placeholder for drawing text (equivalent of g.DrawString)
                //DrawText(b.text, b.position1);

                // Reset clipping if necessary (equivalent of g.ResetClip)
                if (b.clip)
                {
                    GL.Disable(EnableCap.ScissorTest);
                }
            }
        }

        // Method to simulate clicking the button
        private void Click(Vector2 p)
        {
            if (!MouseInBounds(p))
                return;

            Console.WriteLine("Clicked");

            if (onClick != null)
            {
                onClick(this); // Invoke the delegate
            }
        }

        private bool MouseInBounds(Vector2 p)
        {
            return position1.x <= p.x && size.x >= p.x &&
                position1.y <= p.y && size.y >= p.y;
        }

        public void SetText(string text)
        {
            this.text = text;
        }
    }
}
