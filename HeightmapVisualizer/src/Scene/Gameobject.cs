using HeightmapVisualizer.src.Components;
using HeightmapVisualizer.src.Primitives;
using HeightmapVisualizer.src.Utilities;

namespace HeightmapVisualizer.src.Scene
{
    public class Gameobject
    {
        internal Transform Transform { get; set; }
        internal List<IComponent> Components { get; private set; }

        internal Gameobject(Transform? transform = null)
        {
            Transform = transform ?? new Transform();

            Components = new List<IComponent>();
        }

        public void AddComponent(IComponent component)
        {
            component.Init(this);
            Components.Add(component);
        }

        public virtual void Update()
        {
            foreach (var component in Components)
            {
                if (component != null)
                {
                    component.Update();
                }
            }
        }

        public virtual Mesh? GetRenderable() { return null; }
    }
}
