using HeightmapVisualizer.src.Components;
using HeightmapVisualizer.src.Utilities;
using System.Numerics;

namespace HeightmapVisualizer.src.Scene
{
    internal class Gameobject
    {
        internal Transform Transform { get; set; }
        internal List<Component> Components { get; private set; }

        internal Gameobject(Transform? transform = null)
        {
            Transform = transform ?? new Transform();

            Components = new List<Component>();
        }

        internal Gameobject(Vector3 position) : this(new Transform(position, Quaternion.Identity)) { }
        internal Gameobject(Vector3 position, Vector3 rotation) : this(new Transform(position, rotation.CreateQuaternionFromYawPitchRoll())) { }
        internal Gameobject(Vector3 position, Quaternion rotation) : this(new Transform(position, rotation)) { }

        public Gameobject AddComponent(Component component)
        {
            component.Init(this);

            IDManager.Register(component);
            
            Components.Add(component);
            return this;
        }

        public Gameobject RemoveComponent(Component component)
        {
            Components.Remove(component);
            return this;
        }

        public int TryGetComponents<T>(out Component[] result)
        {
            List<Component> res = new List<Component>();
			foreach (var component in Components)
			{
				if (component is T) // Checks if component is of type T or derives from T
				{
					res.Add(component);
				}
			}
			result = res.ToArray();
            return result.Length;
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
    }
}
