
using HeightmapVisualizer.src.Factories;
using HeightmapVisualizer.src.Scene;

namespace HeightmapVisualizer.src.Components.Collision
{
	internal abstract partial class CollisionComponent : Component
	{
		public bool IsDebug { get; private set; }
		public CollisionComponent SetDebug(bool isDebug)
		{
			IsDebug = isDebug;
			return this;
		}

		private protected MeshComponent DebugMesh { get; private set; }
		private void UpdateDebugOutlines() => DebugMesh.SetFaces(IsDebug ?
					Cuboid.CreateFromTwoPoints(ColliderMinCorner, ColliderMaxCorner) :
					Array.Empty<MeshComponent.Face>());


		public override void Init(Gameobject gameobject)
		{
			base.Init(gameobject);

			DebugMesh = new MeshComponent(Cuboid.CreateFromTwoPoints(ColliderMinCorner, ColliderMaxCorner))
				.SetWireframe(true)
				.SetColor(Color.LightGreen);

			Gameobject.AddComponent(DebugMesh);
		}

		public override void Update()
		{
			UpdateDebugOutlines();
		}
	}
}
