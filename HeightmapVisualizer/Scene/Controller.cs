﻿using HeightmapVisualizer.Controls;
using HeightmapVisualizer.UI;
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer.Scene
{
    internal class Controller
    {

        private Vector3 KeyInput = new Vector3();

        public void Init()
        {
            // Enable key preview so the form receives key events
            Window.Instance.KeyPreview = true; // TODO CHANGE ALL THIS TO BE GLOBAL INSTEAD OF PER CONTROLLER

            // Subscribe to the KeyDown and KeyUp events
            Window.Instance.KeyDown += OnKeyDown;
            Window.Instance.KeyUp += OnKeyUp;
        }

        public void Update(Transform objectTransform)
        {
            Pan(objectTransform);
            Move(objectTransform);
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
            if (MouseHandler.Dragging)
            {
                var vector2 = MouseHandler.DragOffset;

                var window = Window.Instance;
                var cam = window.Scene.Camera;


                var originOfClick = MouseHandler.DragStart.ToVector2();
                var relativeMouseOffset = originOfClick - vector2;

                var anglePerPixel = cam.Fov / window.ScreenSize;

                var angle = relativeMouseOffset * anglePerPixel - (cam.Fov / 2);

                // Sensitivity
                float sensitivity = 1f;

                // Apply sensitivity scaling to direction
                angle *= sensitivity;

                var yaw = Quaternion.CreateFromAxisAngle(Vector3.Up, angle.x); // Yaw
                var pitch = Quaternion.CreateFromAxisAngle(Vector3.Right, angle.y); // Pitch

                objectTransform.Rotation = pitch * yaw;
            }
        }
    }
}
