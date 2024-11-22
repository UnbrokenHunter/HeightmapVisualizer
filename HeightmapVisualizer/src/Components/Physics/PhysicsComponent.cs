
using HeightmapVisualizer.src.Components.Collision;
using HeightmapVisualizer.src.Components.Physics.Collision;
using HeightmapVisualizer.src.Components.Physics.Movement;
using HeightmapVisualizer.src.Scene;
using System.Numerics;

namespace HeightmapVisualizer.src.Components.Physics
{
	internal class PhysicsComponent : Component
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

        public float Restitution { get; private set; }
        public PhysicsComponent SetRestitution(float restitution)
        {
            Restitution = restitution;
            return this;
        }

        #endregion

        #region Modules

        public CollisionPhysicsModule Collision { get; private set; }
        public PhysicsComponent SetCollision(CollisionPhysicsModule collision)
        {
            Collision = collision;
            return this;
        }

        public MovementPhysicsModule Movement { get; private set; }
        public PhysicsComponent SetMovement(MovementPhysicsModule movement)
        {
            Movement = movement;
            return this;
        }

        #endregion

        public PhysicsComponent()
		{
			// Defaults - Will be overridden if desired
            Collision = new KineticCollisionPhysicsModule();
			Movement = new KineticMovementPhysicsModule();
        }

		public override void Init(Gameobject gameobject)
		{
			base.Init(gameobject);
			Gameobject.OnCollision += CollisionTrigger;
		}

        private void CollisionTrigger(CollisionComponent other) => Collision.Collision(this, other);

        public override void Update()
		{
			base.Update();

            Movement.Movement(this);
		}
    }
}
