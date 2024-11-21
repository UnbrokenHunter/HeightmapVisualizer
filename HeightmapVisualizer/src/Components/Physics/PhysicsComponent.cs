
using HeightmapVisualizer.src.Scene;
using System.Numerics;

namespace HeightmapVisualizer.src.Components.Physics
{
	internal abstract class PhysicsComponent : Component
	{
		#region Properties

		public Vector3 Velocity { get; private set; }
		public PhysicsComponent SetVelocity(Vector3 velocity)
		{
			Velocity = velocity;
			return this;
		}

		public float Mass { get; private set; }
		public PhysicsComponent SetMass(float mass)
		{
			Mass = mass;
			return this;
		}

		#endregion

		public override void Init(Gameobject gameobject)
		{
			base.Init(gameobject);
			Gameobject.OnCollision += Collision;
		}

		public override void Update()
		{
			base.Update();

			UpdateVelocity();
		}

		private protected abstract void UpdateVelocity();
		private protected abstract void Collision(Collision.CollisionComponent other);
	}
}
