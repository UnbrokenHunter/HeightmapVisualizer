using HeightmapVisualizer.src.Scene;

namespace HeightmapVisualizer.src.Components
{
    public interface IComponent
    {
        public void Init(Gameobject gameobject);

        public void Update();
    }
}
