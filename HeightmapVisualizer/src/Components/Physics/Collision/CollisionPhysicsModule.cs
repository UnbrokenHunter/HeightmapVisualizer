
using HeightmapVisualizer.src.Components.Collision;
using HeightmapVisualizer.src.Scene;
using System.Numerics;

namespace HeightmapVisualizer.src.Components.Physics.Collision
{
    internal abstract class CollisionPhysicsModule
    {
        internal abstract void Collision(CollisionInfo collision);

    }
}
