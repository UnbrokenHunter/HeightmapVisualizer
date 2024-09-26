using HeightmapVisualizer.Controls;
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer.Scene
{
    internal class Controller
    {
        
        private Vector3 KeyInput = new Vector3();

        public Controller()
        {
            // Enable key preview so the form receives key events
            Window.GetInstance().KeyPreview = true;

            // Subscribe to the KeyDown and KeyUp events
            Window.GetInstance().KeyDown += OnKeyDown;
            Window.GetInstance().KeyUp += OnKeyUp;
        }

        public void Update(Transform objectTransform)
        {
            while (true)
            {
                Pan(objectTransform);
                Move(objectTransform);
            }
        }

        private void Move(Transform objectTransform)
        {
            float movementSpeed = 2f;
			objectTransform.Position += KeyInput * movementSpeed;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    KeyInput.z = 1;
                    break;
                case Keys.A:
                    KeyInput.x = 1;
                    break;
                case Keys.S:
                    KeyInput.z = -1;
                    break;
                case Keys.D:
                    KeyInput.x = -1;
                    break;
                case Keys.Q:
                    KeyInput.y = -1;
                    break;
                case Keys.E:
                    KeyInput.y = 1;
                    break;
                case Keys.Escape:
                    Console.WriteLine("Escape key pressed! Exiting...");
                    Application.Exit();
                    break;
            }
        }

        // Handle key up events (optional)
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    KeyInput.z = 0;
                    break;
                case Keys.A:
                    KeyInput.x = 0;
                    break;
                case Keys.S:
                    KeyInput.z = 0;
                    break;
                case Keys.D:
                    KeyInput.x = 0;
                    break;
                case Keys.Q:
                    KeyInput.y = 0;
                    break;
                case Keys.E:
                    KeyInput.y = 0;
                    break;
            }
        }


        private void Pan(Transform objectTransform)
        {
            DragHandler.UpdateDrag();
            if (DragHandler.IsDragging)
            {
                float x = DragHandler.GetCurrentPosition().X - DragHandler.GetLastPosition().X;
                float y = DragHandler.GetCurrentPosition().Y - DragHandler.GetLastPosition().Y;

				// Sensitivity
				float sensitivity = 0.005f;

				// Convert to positive between 0 and 360
				//direction.X %= (float)Math.Tau;
				//direction.Y %= (float)Math.Tau;

				// Apply sensitivity scaling to direction
				x *= sensitivity;
				y *= sensitivity;

				// Calculate pitch and yaw directly based on direction
				float yaw = x;
                float pitch = y;

                // Apply pitch and yaw adjustments
                var rotation = new Vector3(pitch, yaw, 0f);
				objectTransform.Rotation = Camera.Instance.EulerRotation(rotation);

            }
        }
    }
}
