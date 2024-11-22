using HeightmapVisualizer.src.Components.Collision;
using System;
using System.Numerics;

namespace HeightmapVisualizer.src.Components.Physics.Collision
{
    internal class KineticCollisionPhysicsModule : CollisionPhysicsModule
    {
        internal override void Collision(CollisionInfo collision)
        {
            // Extract collision properties
            var e1 = collision.Restitution;
            var e2 = collision.OtherRestitution;
            var m1 = collision.Mass;
            var m2 = collision.OtherMass;
            var v1 = collision.Velocity;
            var v2 = collision.OtherVelocity;

            // Combined properties
            var combinedMass = m1 + m2;
            var combinedRestitution = (e1 * m1 + e2 * m2) / combinedMass;

            // Relative velocity
            var relativeVelocity = v1 - v2;

            // Compute the normal and tangential components
            var normal = Vector3.Normalize(collision.ColliderNormal); // Ensure normal is normalized
            var normalVelocity = Vector3.Dot(relativeVelocity, normal) * normal; // Normal component
            var tangentialVelocity = relativeVelocity - normalVelocity;          // Tangential component

            // Handle normal velocity with restitution and mass
            var newNormalVelocity =
                combinedRestitution > 0
                    ? ((m1 - combinedRestitution * m2) / combinedMass * Vector3.Dot(v1, normal) * normal) +
                      ((1 + combinedRestitution) * m2 / combinedMass * Vector3.Dot(v2, normal) * normal)
                    : Vector3.Zero; // Restitution of 0 means objects "stick" in the normal direction

            // Adjust tangential velocity for inelastic collisions
            var tangentialDamping = combinedRestitution > 0 ? 1.0f : 0.5f; // Reduce tangential velocity if restitution is 0
            var newTangentialVelocity = tangentialDamping * tangentialVelocity;

            // Final velocity for the object after collision
            var finalVelocity = newNormalVelocity + newTangentialVelocity;

            Console.WriteLine($"Final Velocity: {finalVelocity}");

            // Update the velocity in the physics system
            collision.Physics?.SetVelocity(finalVelocity);
        }
    }
}
