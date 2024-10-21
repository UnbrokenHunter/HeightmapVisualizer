using HeightmapVisualizer.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeightmapVisualizer.Components
{
	internal class ScriptableComponent : IComponent
	{

		internal Action<Gameobject>? ScriptableInit { get; private set; }
		internal Action<Gameobject>? ScriptableUpdate { get; private set; }

		internal Gameobject? gameobject;

		public ScriptableComponent(Action<Gameobject>? init = null, Action<Gameobject>? update = null) 
		{
			ScriptableInit = init;
			ScriptableUpdate = update;
		}

		public void Init(Gameobject gameobject)
		{
			this.gameobject = gameobject;

			if (ScriptableInit != null)
			{
				ScriptableInit(gameobject);
			}
		}

		public void Update()
		{
			if (ScriptableUpdate != null)
			{
				ScriptableUpdate(gameobject);
			}
		}
	}
}
