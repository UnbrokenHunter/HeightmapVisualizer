using HeightmapVisualizer.src.Components.Collision;

namespace HeightmapVisualizer.src.Components.Physics
{
	internal class StaticPhysicsComponent : PhysicsComponent
	{
		private protected override void Collision(CollisionComponent other)
		{
		}

		private protected override void UpdateVelocity()
		{
			Gameobject.Transform.Move(Velocity);
		}
	}
}
