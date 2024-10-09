using HeightmapVisualizer.Scene;
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer
{
    internal class Menu
    {
        private UI.Button PositionText;
        private UI.Button EulerRotationText;

        public Menu()
        {
            PositionText = new UI.Button("Position", new Vector2(0, 0), new Vector2(400, 60));
            EulerRotationText = new UI.Button("Euler Angles", new Vector2(0, 60), new Vector2(400, 60));
        }

        public void Update(Camera cam)
        {
            PositionText.SetText($"Position:{cam.Transform.Position}");
            EulerRotationText.SetText($"Euler Angles:{Quaternion.ToPitchYawRoll(cam.Transform.Rotation)}");
        }
    }
}
