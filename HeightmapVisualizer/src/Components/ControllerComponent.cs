using HeightmapVisualizer.src.Controls;
using HeightmapVisualizer.src.Scene;
using HeightmapVisualizer.src.Utilities;
using System.Numerics;

namespace HeightmapVisualizer.src.Components
{
    internal class ControllerComponent : Component
    {

        private Vector3 KeyInput = new Vector3();
		public float Speed { get; private set; }
        public ControllerComponent SetSpeed(float speed)
        {
            this.Speed = speed;
            return this;
        }


        public ControllerComponent() => Speed = 0.1f;

        public override void Init(Gameobject gameobject)
        {
            base.Init(gameobject);

            // Enable key preview so the form receives key events
            Window.Instance.KeyPreview = true; // TODO CHANGE ALL THIS TO BE GLOBAL INSTEAD OF PER CONTROLLER

            // Subscribe to the KeyDown and KeyUp events
            Window.Instance.KeyDown += OnKeyDown;
            Window.Instance.KeyUp += OnKeyUp;
        }

        public override void Update()
        {
            Pan(Gameobject.Transform);
            Move(Gameobject.Transform);
        }

        private void Move(Transform objectTransform)
        {
            objectTransform.Move(KeyInput * Speed);
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
