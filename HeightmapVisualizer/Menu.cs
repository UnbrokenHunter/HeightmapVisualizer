using HeightmapVisualizer.Scene;
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer
{
    internal class Menu
    {
        private UI.Button Pos;
        private UI.Button Quat;
        private UI.Button NQuat;
        private UI.Button Euler;
        private UI.Button fov;

        public Menu()
        {
            Pos = new UI.Button("Position", new Vector2(0, 60), new Vector2(100, 60), (button) => { });

            Quat = new UI.Button("Quaterion", new Vector2(0, 240), new Vector2(100, 60), (button) => { });
            NQuat = new UI.Button("Normalized Quaterion", new Vector2(0, 300), new Vector2(100, 60), (button) => { });
            Euler = new UI.Button("Euler Angles", new Vector2(0, 360), new Vector2(100, 60), (button) => { });

            fov = new UI.Button("FOV", new Vector2(0, 440), new Vector2(100, 60), (button) => { });

        }

        public void Update(Camera cam)
        {
            Pos.text = $"Position:{cam.Transform.Position}";

            Quat.text = $"Quaterion: {cam.Transform.Rotation}";
            NQuat.text = $"Normalized Quaterion: {Quaternion.Normalize(cam.Transform.Rotation)}";
            Euler.text = $"Euler Angles:{Quaternion.ToEulerAngles(cam.Transform.Rotation)}";

            fov.text = $"FOV:{cam.Fov}";
        }
    }
}
