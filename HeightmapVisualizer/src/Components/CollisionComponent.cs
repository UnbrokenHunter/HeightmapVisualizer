
using HeightmapVisualizer.src.Scene;

namespace HeightmapVisualizer.src.Components
{
	internal class CollisionComponent : Component
	{
		public override void Init(Gameobject gameobject)
		{
			base.Init(gameobject);
		}

		public override void Update()
		{
			var collisions = IDManager.GetObjectsByType<CollisionComponent>();
			Console.WriteLine(collisions.Count);
		}
	}
}
