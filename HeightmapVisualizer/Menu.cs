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
            PositionText = new UI.Button("Position", new Vector2(-1, 0.95f), new Vector2(0.2f, 0.05f));
            EulerRotationText = new UI.Button("Euler Angles", new Vector2(-1, 0.9f), new Vector2(0.2f, 0.05f));
        }

        public void Update(Camera cam)
        {
            PositionText.SetText($"Position:{cam.Transform.Position}");
            EulerRotationText.SetText($"Euler Angles:{Quaternion.ToPitchYawRoll(cam.Transform.Rotation)}");
        }
    }
}
