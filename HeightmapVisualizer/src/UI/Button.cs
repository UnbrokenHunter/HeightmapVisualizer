using HeightmapVisualizer.Controls;
using HeightmapVisualizer.src.Utilities;
using System.Numerics;

namespace HeightmapVisualizer.src.UI
{
    class Button : UIElement
    {
        private string text;
        private Action<Button>? onClick;  // Delegate to handle the button click action
        private bool clip;

        // Constructor for the Button class
        public Button(Vector2 position, Vector2 size, string text, Action<Button>? onClick = null, bool clips = true, string id = "") : base(position, size, id)
        {
            this.text = text;
            this.onClick = onClick;
            clip = clips;

            MouseHandler.OnLeftClick += Click;
        }

        public override void Draw(Graphics g)
        {
            Pen pen = ColorLookup.FindOrGetPen(Color.Black);
            Font font = new Font("Arial", 13f);
            Brush brush = ColorLookup.FindOrGetBrush(Color.Black);

            var rect = new Rectangle((int)position1.X, (int)position1.Y, (int)size.X, (int)size.Y);

            g.DrawRectangle(pen, rect);

            if (clip)
                g.SetClip(rect);

            g.DrawString(text, font, brush, position1.X, position1.Y);

            if (clip)
                g.ResetClip();
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
