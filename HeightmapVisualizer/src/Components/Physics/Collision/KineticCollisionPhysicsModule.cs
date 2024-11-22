using HeightmapVisualizer.src.Components.Collision;
using System;
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

            // Relative velocity
            var relativeVelocity = v1 - v2;

            // Compute the normal and tangential components
            var normal = collision.ColliderNormal; // Assume this is already normalized
            var normalVelocity = Vector3.Dot(relativeVelocity, normal) * normal; // Normal component
            var tangentialVelocity = relativeVelocity - normalVelocity;          // Tangential component

            // Apply restitution to the normal component
            var reflectedNormalVelocity = -combinedRestitution * normalVelocity;

            // Combine adjusted normal and tangential velocities
            var finalVelocity = tangentialVelocity + reflectedNormalVelocity;

            Console.WriteLine($"Final Velocity: {finalVelocity}");

            // Update the velocity in the physics system
            collision.Physics?.SetVelocity(finalVelocity);
        }
    }
}
