using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.Shapes;
using HeightmapVisualizer.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeightmapVisualizer.Scene
{
	public abstract class Gameobject
	{
		internal Transform Transform { get; set; }
		internal Controller Controller { get; set; }

		public Gameobject() : this(new Transform()) { }

		internal Gameobject(Transform transform)
		{
			Transform = transform;
		}

		public virtual void Init() 
		{
			if (Controller != null)
			{
				Controller.Init();
			}
		}

		public virtual void Update()
		{
			if (Controller != null)
			{
				Controller.Update(Transform);
			}
		}

		public virtual Mesh? GetRenderable() { return null; }
	}
}
