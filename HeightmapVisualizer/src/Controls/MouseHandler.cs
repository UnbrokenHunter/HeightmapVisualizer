
using HeightmapVisualizer.src;
using System.Numerics;

namespace HeightmapVisualizer.Controls
{
    internal static class MouseHandler
    {
        public static Point MousePosition { get; private set; }

        public static Point[] LastMousePositions = new Point[5];

        public static Vector2 MouseTrend => ComputeAverageDirection(LastMousePositions);


        public static Point DragStart { get; private set; }
        public static Point DragEnd { get; private set; }
        public static bool Dragging { get; private set; }
        public static Vector2 DragOffset { get; private set; }


        public delegate void OnClick(Point point);
        public static event OnClick? OnLeftClick;
        public static event OnClick? OnRightClick;
        public static event OnClick? OnMiddleClick;

        public static void Debug(Graphics g)
        {
            g.DrawLines(new Pen(Color.Red), LastMousePositions);

            g.DrawPie(new Pen(Color.Black), MousePosition.X - 15, MousePosition.Y - 15, 30, 30, 0, 360);

            Vector2 trend = ComputeAverageDirection(LastMousePositions);
            g.DrawLine(new Pen(Color.DarkBlue), MousePosition,
                new Point((int)(MousePosition.X + trend.X), (int)(MousePosition.Y + trend.Y)));
        }

        public static void Update()
        {
            UpdatePositions();
            UpdateDragging();
            UpdateClicking();
        }

        private static void UpdatePositions()
        {
            // Update previous mouse positions
            for (int i = LastMousePositions.Length - 1; i >= 2; i--)
            {
                LastMousePositions[i] = LastMousePositions[i - 1];
            }

            // LastMousePosition[0] should always be the current position and LastMousePosition[1] is the most recent prevous position
            LastMousePositions[1] = MousePosition;
            MousePosition =
                new Point(Cursor.Position.X - Window.Instance.Bounds.Location.X - Window.Instance.Cursor.Size.Width / 4,
                    Cursor.Position.Y - Window.Instance.Bounds.Location.Y - Window.Instance.Cursor.Size.Height);

            LastMousePositions[0] = MousePosition;
        }

        public static Vector2 ComputeAverageDirection(Point[] points)
        {
            if (points == null)
                throw new ArgumentException("Cannot compute Average Direction because points is null.");

            int sumX = 0;
            int sumY = 0;
            for (int i = 0; i < points.Length - 1; i++)
            {
                sumX += points[i].X - points[i + 1].X;
                sumY += points[i].Y - points[i + 1].Y;
            }

            sumX /= (points.Length);
            sumY /= (points.Length);

            return new Vector2(sumX, sumY);
        }

        private static void UpdateDragging()
        {
            // Check if the left mouse button is pressed
            if (Control.MouseButtons == MouseButtons.Left)
            {
                // If dragging has not started, record the initial position
                if (!Dragging)
                {
                    DragStart = Cursor.Position; // Store the original position (screen coordinates)
                    DragEnd = Point.Empty;
                    Dragging = true;
                    Console.WriteLine($"Dragging started at: {DragStart}");
                }

            }
            else if (Dragging) // If the left mouse button is released, stop dragging
            {
                Dragging = false;
                DragEnd = Cursor.Position;
                Console.WriteLine($"Dragging ended at: {Cursor.Position}");
            }

            if (Dragging)
                DragOffset = new Vector2(DragStart.X - DragStart.X, DragStart.Y - DragStart.Y);
            else
                DragOffset = new Vector2();

        }

        private static void UpdateClicking()
        {
            if (Control.MouseButtons == MouseButtons.Left)
                OnLeftClick?.Invoke(MousePosition);
            if (Control.MouseButtons == MouseButtons.Right)
                OnRightClick?.Invoke(MousePosition);
            if (Control.MouseButtons == MouseButtons.Middle)
                OnMiddleClick?.Invoke(MousePosition);
        }
    }
}