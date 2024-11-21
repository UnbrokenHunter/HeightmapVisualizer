using HeightmapVisualizer.src.Scene;

namespace HeightmapVisualizer.src.Components
{
    internal class ScriptableComponent : Component
    {
		internal Action<Gameobject>? ScriptableInit { get; private set; }
        internal Action<Gameobject>? ScriptableUpdate { get; private set; }

		public ScriptableComponent(Action<Gameobject>? init = null, Action<Gameobject>? update = null)
        {
            ScriptableInit += init;
            ScriptableUpdate += update;
        }

        public override void Init(Gameobject gameobject)
        {
            base.Init(gameobject);

            if (ScriptableInit != null)
            {
                ScriptableInit(gameobject);
            }
        }

        public override void Update()
        {
            if (ScriptableUpdate != null)
            {
                ScriptableUpdate(Gameobject);
            }
        }
    }
}
