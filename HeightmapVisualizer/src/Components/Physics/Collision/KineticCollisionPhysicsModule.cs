using HeightmapVisualizer.src.Components.Collision;

namespace HeightmapVisualizer.src.Components.Physics.Collision
{
    internal class KineticCollisionPhysicsModule : CollisionPhysicsModule
    {
        internal override void Collision(PhysicsComponent physics, CollisionComponent other)
        {
            physics.Gameobject.Transform.Move(-physics.Velocity);
        }

    }
}
