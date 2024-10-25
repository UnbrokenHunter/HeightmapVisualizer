using HeightmapVisualizer.Controls;
using HeightmapVisualizer.Scene;
using HeightmapVisualizer.UI;
using HeightmapVisualizer.Units;
using HeightmapVisualizer.Utilities;
using System.Numerics;

namespace HeightmapVisualizer.Components
{
    internal class ControllerComponent : IComponent
    {

        private Vector3 KeyInput = new Vector3();

        private Gameobject gameobject;

        public void Init(Gameobject gameobject)
        {
            this.gameobject = gameobject;

            // Enable key preview so the form receives key events
            Window.Instance.KeyPreview = true; // TODO CHANGE ALL THIS TO BE GLOBAL INSTEAD OF PER CONTROLLER

            // Subscribe to the KeyDown and KeyUp events
            Window.Instance.KeyDown += OnKeyDown;
            Window.Instance.KeyUp += OnKeyUp;
        }

        public void Update()
        {
            Pan(gameobject.Transform);
            Move(gameobject.Transform);
        }

        private void Move(Transform objectTransform)
        {
            float movementSpeed = 0.1f;
            objectTransform.Move(KeyInput * movementSpeed);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    KeyInput.Z = 1;
                    break;
                case Keys.A:
                    KeyInput.X = -1;
                    break;
                case Keys.S:
                    KeyInput.Z = -1;
                    break;
                case Keys.D:
                    KeyInput.X = 1;
                    break;
                case Keys.Q:
                    KeyInput.Y = 1;
                    break;
                case Keys.E:
                    KeyInput.Y = -1;
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
                    KeyInput.Z = 0;
                    break;
                case Keys.A:
                    KeyInput.X = 0;
                    break;
                case Keys.S:
                    KeyInput.Z = 0;
                    break;
                case Keys.D:
                    KeyInput.X = 0;
                    break;
                case Keys.Q:
                    KeyInput.Y = 0;
                    break;
                case Keys.E:
                    KeyInput.Y = 0;
                    break;
            }
        }

        private void Pan(Transform objectTransform)
        {
            if (MouseHandler.Dragging)
            {
                // Sensitivity
                float sensitivity = 4f;

                var window = Window.Instance;
                var cam = window.Scene.Camera;

                var relativeMouseOffset = MouseHandler.MouseTrend;
                var anglePerPixel = cam.Fov / window.ScreenSize;


                var angle = relativeMouseOffset * anglePerPixel;

                // Apply sensitivity scaling to direction
                angle *= sensitivity;


                var yaw = Quaternion.CreateFromAxisAngle(Vector3.UnitY, angle.X * 0.01745329f); // Yaw (0.01745329f is pi/180)
                var pitch = Quaternion.CreateFromAxisAngle(Vector3.UnitX, -angle.Y * 0.01745329f); // Pitch

                // Apply Rotation
                var quaternionRotation = Quaternion.Normalize(objectTransform.Rotation * (pitch * yaw));

                // Convert to Euler to remove Roll
                var eulerRotation = quaternionRotation.ToYawPitchRoll();
                eulerRotation.Z = 0;

                // Convert Back
                var rotation = eulerRotation.CreateQuaternionFromYawPitchRoll();

                objectTransform.Rotation = rotation;
            }
        }
    }
}
