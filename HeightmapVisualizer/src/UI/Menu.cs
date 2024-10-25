using HeightmapVisualizer.Scene;
using HeightmapVisualizer.src.Utilities;
using System.Numerics;

namespace HeightmapVisualizer.src.UI
{
    internal class Menu
    {
        private Button PositionText;
        private Button EulerRotationText;

        public Menu()
        {
            PositionText = new Button("Position", new Vector2(0, 0), new Vector2(400, 60));
            EulerRotationText = new Button("Euler Angles", new Vector2(0, 60), new Vector2(400, 60));
        }

        public void Update(Camera cam)
        {
            PositionText.SetText($"Position:{cam.Transform.Position}");
            EulerRotationText.SetText($"Euler Angles:{cam.Transform.Rotation.ToYawPitchRoll()}");
        }
    }
}
