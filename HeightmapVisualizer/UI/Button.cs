
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

            //Window.Instance.MouseClick += new MouseEventHandler(Click);
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
        private void Click(object sender, MouseEventArgs e)
        {
            if (!MouseInBounds())
                return;
            Console.WriteLine("Test");

            if (onClick != null)
            {
                onClick(this); // Invoke the delegate
            }
        }

        public bool MouseInBounds()
        {
            var cursor = Cursor.Position;
            var screen = Window.Instance.Bounds;
            var relativeCursor = new Vector2(cursor.X - screen.X, cursor.Y - screen.Y);
            return position1.x <= relativeCursor.x && position2.x >= relativeCursor.x &&
                position1.y <= relativeCursor.y && position2.y >= relativeCursor.y;
        }
    }
}
