using HeightmapVisualizer.src.Components.Collision;
using System.Numerics;

namespace HeightmapVisualizer.src.Components.Physics.Collision
{
    internal class KineticCollisionPhysicsModule : CollisionPhysicsModule
    {
        internal override void Collision(PhysicsComponent physics, CollisionComponent other)
        {
            var e1 = physics.Restitution;
            var e2 = 1.0f;

            var m1 = physics.Mass;
            var m2 = 1.0f;

            var v1 = physics.Velocity;
            var v2 = Vector3.Zero;

            other.Gameobject.TryGetComponents(out PhysicsComponent[] components);
            if (components.Length > 0)
            {
                e2 = components[0].Restitution;
                m2 = components[0].Mass;
                v2 = components[0].Velocity;
            }

            var combinedMass = (m1 + m2);
            var combinedRestitution = (e1 * m1 + e2 * m2) / combinedMass;

            var finalVelocity = ((m1 - combinedRestitution * m2) / combinedMass * v1) + ((1 + combinedRestitution) * m2 / combinedMass * v2);

            // Maybe change to use reflect? Requires the normal tho
            physics.SetVelocity(finalVelocity);
        }

    }
}
