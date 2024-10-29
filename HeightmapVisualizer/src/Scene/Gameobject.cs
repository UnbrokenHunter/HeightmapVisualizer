﻿using HeightmapVisualizer.src.Components;
using HeightmapVisualizer.src.Utilities;
using System.Numerics;

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

        internal Gameobject(Vector3 position) : this(new Transform(position, Quaternion.Identity)) { }
        internal Gameobject(Vector3 position, Vector3 rotation) : this(new Transform(position, rotation.CreateQuaternionFromYawPitchRoll())) { }
        internal Gameobject(Vector3 position, Quaternion rotation) : this(new Transform(position, rotation)) { }

        public Gameobject AddComponent(IComponent component)
        {
            component.Init(this);
            Components.Add(component);
            return this;
        }

        public int TryGetComponents<T>(out IComponent[] result)
        {
            result = Components.FindAll(comp => comp.GetType() == typeof(T)).ToArray();
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
