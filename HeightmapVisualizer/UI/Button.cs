
using HeightmapVisualizer.Controls;
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer.UI
{
    class Button
    {
        private static List<Button> buttons = new List<Button>();

        public string text;
        public Vector2 position1;
        public Vector2 position2;
        public Action<Button> onClick;  // Delegate to handle the button click action

        // Constructor for the Button class
        public Button(string text, Vector2 position, Vector2 size, Action<Button> onClick)
        {
            buttons.Add(this);

            this.text = text;
            position1 = position;
            position2 = position1 + size;
            this.onClick = onClick;

            MouseHandler.OnLeftClick += Click;
        }

        public static void Draw(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            Font font = new Font("Arial", 13f);
            Brush brush = new SolidBrush(Color.Black);

            foreach (Button b in buttons)
            {
                g.DrawRectangle(pen, (int)b.position1.x, (int)b.position1.y, (int)b.position2.x, (int)b.position2.y);
                g.DrawString(b.text, font, brush, b.position1.x, b.position1.y);
            }
        }

        // Method to simulate clicking the button
        private void Click(Point p)
        {
            if (!MouseInBounds(p))
                return;
            Console.WriteLine("Test");

            if (onClick != null)
            {
                onClick(this); // Invoke the delegate
            }
        }

        private bool MouseInBounds(Point p)
        {
            return position1.x <= p.X && position2.x >= p.X &&
                position1.y <= p.Y && position2.y >= p.Y;
        }
    }
}
