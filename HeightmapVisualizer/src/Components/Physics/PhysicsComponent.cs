
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

        public CollisionPhysicsModule CollisionModule { get; private set; }
        public PhysicsComponent SetCollisionModule(CollisionPhysicsModule collision)
        {
            CollisionModule = collision;
            return this;
        }

        public MovementPhysicsModule MovementModule { get; private set; }
        public PhysicsComponent SetMovementModule(MovementPhysicsModule movement)
        {
            MovementModule = movement;
            return this;
        }

        #endregion

        public PhysicsComponent()
		{
			// Defaults - Will be overridden if desired
            CollisionModule = new KineticCollisionPhysicsModule();
			MovementModule = new KineticMovementPhysicsModule();
        }

		private void CollisionTrigger(CollisionInfo collision) => CollisionModule.Collision(collision);
		public override void Init(Gameobject gameobject)
		{
			base.Init(gameobject);
			Gameobject.OnCollision += CollisionTrigger;
		}

        public override void Update()
		{
			base.Update();

            MovementModule.Movement(this);
		}
    }
}
