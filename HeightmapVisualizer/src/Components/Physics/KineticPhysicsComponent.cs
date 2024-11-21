using HeightmapVisualizer.src.Components.Collision;

namespace HeightmapVisualizer.src.Components.Physics
{
	internal class KineticPhysicsComponent : PhysicsComponent
	{
		private protected override void Collision(CollisionComponent other)
		{
			Gameobject.Transform.Move(-Velocity);
		}

		private protected override void UpdateVelocity()
		{
			Gameobject.Transform.Move(Velocity);
		}
	}
}
