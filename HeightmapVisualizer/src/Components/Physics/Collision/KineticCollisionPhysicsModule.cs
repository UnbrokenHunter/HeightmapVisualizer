using HeightmapVisualizer.src.Components.Collision;
using System.Numerics;

namespace HeightmapVisualizer.src.Components.Physics.Collision
{
    internal class KineticCollisionPhysicsModule : CollisionPhysicsModule
    {
        internal override void Collision(CollisionInfo collision)
        {
            var e1 = collision.Restitution;
            var e2 = collision.OtherRestitution;

            var m1 = collision.Mass;
            var m2 = collision.OtherMass;

            var v1 = collision.Velocity;
            var v2 = collision.OtherVelocity;

            var combinedMass = m1 + m2;
            var combinedRestitution = (e1 * m1 + e2 * m2) / combinedMass;

            var finalVelocity = ((m1 - combinedRestitution * m2) / combinedMass * v1) + ((1 + combinedRestitution) * m2 / combinedMass * v2);

			// Maybe change to use reflect? Requires the normal tho
			collision.Physics?.SetVelocity(finalVelocity);
        }
    }
}
