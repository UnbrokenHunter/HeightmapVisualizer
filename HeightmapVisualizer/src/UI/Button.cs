using HeightmapVisualizer.Controls;
using HeightmapVisualizer.src.Utilities;
using System.Numerics;

namespace HeightmapVisualizer.src.UI
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
            position1 = position;
            this.size = size;
            this.onClick = onClick;
            clip = clips;

            MouseHandler.OnLeftClick += Click;
        }

        public static void Draw(Graphics g)
        {
            Pen pen = ColorLookup.FindOrGetPen(Color.Black);
            Font font = new Font("Arial", 13f);
            Brush brush = ColorLookup.FindOrGetBrush(Color.Black);

            foreach (Button b in buttons)
            {
                var rect = new Rectangle((int)b.position1.X, (int)b.position1.Y, (int)b.size.X, (int)b.size.Y);

                g.DrawRectangle(pen, rect);

                if (b.clip)
                    g.SetClip(rect);

                g.DrawString(b.text, font, brush, b.position1.X, b.position1.Y);

                if (b.clip)
                    g.ResetClip();
            }
        }

        // Method to simulate clicking the button
        private void Click(Point p)
        {
            if (!MouseInBounds(p))
                return;

            Console.WriteLine("Clicked");

            if (onClick != null)
            {
                onClick(this); // Invoke the delegate
            }
        }

        private bool MouseInBounds(Point p)
        {
            return position1.X <= p.X && size.X >= p.X &&
                position1.Y <= p.Y && size.Y >= p.Y;
        }

        public void SetText(string text)
        {
            this.text = text;
        }
    }
}
