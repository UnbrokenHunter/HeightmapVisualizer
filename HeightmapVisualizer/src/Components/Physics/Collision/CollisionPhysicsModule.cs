
using HeightmapVisualizer.src.Components.Collision;
using HeightmapVisualizer.src.Scene;

namespace HeightmapVisualizer.src.Components.Physics.Collision
{
    internal abstract class CollisionPhysicsModule
    {
        internal abstract void Collision(PhysicsComponent physics, CollisionComponent other);

    }
}
