
using HeightmapVisualizer.Units;
using HeightmapVisualizer.Utilities;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace HeightmapVisualizer.Controls
{
    internal static class MouseHandler
    {
        public static Vector2 ToNDC(Vector2 point)
        {
            // Convert from screen space (pixels) to NDC (-1 to 1)
            float ndcX = 2.0f * (point.x / Window.Instance.ClientSize.X) - 1.0f;
            float ndcY = 1.0f - 2.0f * (point.y / Window.Instance.ClientSize.Y);  // Invert Y axis

            return new Vector2(ndcX, ndcY);
        }
        public static Vector2 FromNDC(Vector2 ndcPoint)
        {
            // Convert from NDC (-1 to 1) back to Client Screen Space (pixels)
            float screenX = ndcPoint.x * Window.Instance.ClientSize.X;
            float screenY = ndcPoint.y * Window.Instance.ClientSize.Y;  // Invert Y axis

            return new Vector2(screenX, screenY);
        }

        public static Vector2 MousePosition { get; private set; }

        public static Vector2[] LastMousePositions = new Vector2[5];

        public static Vector2 MouseTrend => ComputeAverageDirection(LastMousePositions);


        public static Vector2 DragStart { get; private set; }
        public static Vector2 DragEnd { get; private set; }
        public static bool Dragging { get; private set; }
        public static Vector2 DragOffset { get; private set; }


        public delegate void OnClick(Vector2 point);
        public static event OnClick? OnLeftClick;
        public static event OnClick? OnRightClick;
        public static event OnClick? OnMiddleClick;

        public static void Debug(FrameEventArgs args)
        {
            // 1. Draw the connected lines (equivalent to g.DrawLines using LineStrip)
            Vector2[] lineVertices = LastMousePositions; ;  // Assuming LastMousePositions is a list of Vector2
            GLUtilities.DrawLineStrip(lineVertices, Color.Red);

            // 2. Draw the filled circle at the mouse position (equivalent to g.DrawPie)
            //GLUtilities.DrawCircle(MousePosition, 15, Color.Black);

            // 3. Draw the trend line (equivalent to g.DrawLine)
            Vector2 trend = ComputeAverageDirection(LastMousePositions);  // Assuming this method exists
            Vector2[] trendLineVertices = new Vector2[]
            {
                MousePosition,
                MousePosition + trend
            };
            GLUtilities.DrawLine(trendLineVertices, Color.DarkBlue);
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

            // Get mouse position relative to the window
            MousePosition = ToNDC(new Vector2((int)Window.Instance.MouseState.Position.X, (int)Window.Instance.MouseState.Position.Y));

            LastMousePositions[0] = MousePosition;
        }

        public static Vector2 ComputeAverageDirection(Vector2[] points)
        {
            if (points == null)
                throw new ArgumentException("Cannot compute Average Direction because points is null.");

            float sumX = 0;
            float sumY = 0;
            for (int i = 0; i < points.Length - 1; i++)
            {
                sumX += points[i].x - points[i + 1].x;
                sumY += points[i].y - points[i + 1].y;
            }

            sumX /= (points.Length);
            sumY /= (points.Length);

            return new Vector2(sumX, sumY);
        }

        private static void UpdateDragging()
        {
            // Check if the left mouse button is pressed
            if (Window.Instance.MouseState.IsButtonDown(MouseButton.Left))
            {
                // If dragging has not started, record the initial position
                if (!Dragging)
                {
                    DragStart = MousePosition;
                    DragEnd = new Vector2();
                    Dragging = true;
                    Console.WriteLine($"Dragging started at: {DragStart}");
                }

            }
            else if (Dragging) // If the left mouse button is released, stop dragging
            {
                Dragging = false;
                DragEnd = MousePosition;
                Console.WriteLine($"Dragging ended at: {Window.Instance.MouseState.Position}");
            }

            if (Dragging)
                DragOffset = DragStart - MousePosition;
            else
                DragOffset = new Vector2();

        }

        private static void UpdateClicking()
        {
            if (Window.Instance.MouseState.IsButtonDown(MouseButton.Left))
                OnLeftClick?.Invoke(MousePosition);
            if (Window.Instance.MouseState.IsButtonDown(MouseButton.Right))
                OnRightClick?.Invoke(MousePosition);
            if (Window.Instance.MouseState.IsButtonDown(MouseButton.Middle))
                OnMiddleClick?.Invoke(MousePosition);
        }
    }
}