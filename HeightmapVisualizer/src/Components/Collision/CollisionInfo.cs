
using HeightmapVisualizer.src.Components.Physics;
using System.Numerics;

namespace HeightmapVisualizer.src.Components.Collision
{
	internal struct CollisionInfo
	{
		internal readonly CollisionComponent Collider { get; }
		internal readonly Vector3 ColliderNormal { get; }
		internal readonly PhysicsComponent? Physics { get; }
		internal readonly Vector3 Velocity { get; }
		internal readonly float Restitution { get; }
		internal readonly float Mass { get; }

		internal readonly CollisionComponent OtherCollider { get; }
		internal readonly Vector3 OtherColliderNormal { get; }
		internal readonly PhysicsComponent? OtherPhysics { get; }
		internal readonly Vector3 OtherVelocity { get; }
		internal readonly float OtherRestitution { get; }
		internal readonly float OtherMass { get; }

		internal CollisionInfo(CollisionComponent collider, CollisionComponent other) : this()
		{
			this.Collider = collider;
			this.OtherCollider = other;

			this.ColliderNormal = CollisionComponent.CalculateCollisionNormal(collider, other);
			this.OtherColliderNormal = CollisionComponent.CalculateCollisionNormal(other, collider);

			collider.Gameobject.TryGetComponents(out PhysicsComponent[] p1);
			this.Physics = p1[0];

			other.Gameobject.TryGetComponents(out PhysicsComponent[] p2);
			this.OtherPhysics = p2[0];

			if (Physics != null)
			{
				this.Velocity = Physics.Velocity;
				this.Restitution = Physics.Restitution;
				this.Mass = Physics.Mass;
			}

			if (OtherPhysics != null)
			{
				this.OtherVelocity = OtherPhysics.Velocity;
				this.OtherRestitution = OtherPhysics.Restitution;
				this.OtherMass = OtherPhysics.Mass;
			}
		}
	}
}
