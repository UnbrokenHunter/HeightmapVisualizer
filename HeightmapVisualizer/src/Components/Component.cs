using HeightmapVisualizer.src.Scene;

namespace HeightmapVisualizer.src.Components
{
    internal class Component : IIdentifiable
    {
		public Guid ID { get; private set; }
		public Gameobject Gameobject { get; private set; }

		public virtual void Init(Gameobject gameobject)
		{
			this.ID = Guid.NewGuid();
			this.Gameobject = gameobject;
		}

		public virtual void Update() { }
    }
}
