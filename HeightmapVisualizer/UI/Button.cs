using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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

            Window.GetInstance().MouseClick += new MouseEventHandler(Click);
        }

        public static void Draw(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            Font font = new Font("Arial", 13f);
            Brush brush = new SolidBrush(Color.Black);

            foreach (Button b in buttons)
            {
                g.DrawRectangle(pen, (int)b.position1.X, (int)b.position1.Y, (int)b.position2.X, (int)b.position2.Y);
                g.DrawString(b.text, font, brush, b.position1.X, b.position1.Y);
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
            var screen = Window.GetInstance().Bounds;
            var relativeCursor = new Vector2(cursor.X - screen.X, cursor.Y - screen.Y);
            return position1.X <= relativeCursor.X && position2.X >= relativeCursor.X &&
                position1.Y <= relativeCursor.Y && position2.Y >= relativeCursor.Y;
        }
    }
}
