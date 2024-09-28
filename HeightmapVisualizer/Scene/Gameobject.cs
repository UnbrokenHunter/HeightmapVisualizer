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
	internal abstract class Gameobject
	{
		public Transform Transform { get; set; }
		public Controller Controller { get; set; }

		public Gameobject() : this(new Transform()) { }

		public Gameobject(Transform transform) 
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

		public virtual Renderable? GetRenderable() { return null; }
	}
}
