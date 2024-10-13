
using HeightmapVisualizer.Controls;
using HeightmapVisualizer.Units;
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
            //// Set color to black for drawing (equivalent to the pen color)
            //GL.Color3(0.0f, 0.0f, 0.0f);  // Black color

            //foreach (Button b in buttons)
            //{
            //    // Convert button position and size to a rectangle
            //    var rect = new Rectangle((int)b.position1.x, (int)b.position1.y, (int)b.size.x, (int)b.size.y);

            //    // Draw the rectangle (equivalent of g.DrawRectangle)
            //    GL.Begin(PrimitiveType.LineLoop);
            //    GL.Vertex2(rect.Left, rect.Top);
            //    GL.Vertex2(rect.Right, rect.Top);
            //    GL.Vertex2(rect.Right, rect.Bottom);
            //    GL.Vertex2(rect.Left, rect.Bottom);
            //    GL.End();

            //    // Handle clipping if necessary (equivalent of g.SetClip)
            //    if (b.clip)
            //    {
            //        GL.Enable(EnableCap.ScissorTest);
            //        GL.Scissor(rect.Left, rect.Top, rect.Width, rect.Height);
            //    }

            //    // Placeholder for drawing text (equivalent of g.DrawString)
            //    //DrawText(b.text, b.position1);

            //    // Reset clipping if necessary (equivalent of g.ResetClip)
            //    if (b.clip)
            //    {
            //        GL.Disable(EnableCap.ScissorTest);
            //    }
            //}
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
