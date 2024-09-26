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

		public abstract void Init();

		public void UpdateObject()
		{
			if (Controller != null)
			{
				Controller.Update(Transform);
			}

			Update();
		}

		public abstract void Update();

	}
}
