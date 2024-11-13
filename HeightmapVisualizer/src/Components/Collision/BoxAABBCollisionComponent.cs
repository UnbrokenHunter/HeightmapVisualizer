using System.Numerics;

namespace HeightmapVisualizer.src.Components.Collision
{
    internal class BoxAABBCollisionComponent : CollisionComponent
    {
        public override CollisionComponent SetCollider(dynamic vector3)
        {
            ColliderSize = vector3;
            return this;
        }

        public BoxAABBCollisionComponent() : base()
        {
            ColliderSize = Vector3.One;
        }

        private protected override void ColliderCalculation(List<CollisionComponent> colliders)
        {
            foreach (var collision in colliders)
            {
                if (collision.Equals(this)) continue;

                if (AABBIntersect(this, collision))
                    Console.WriteLine("Colliding" + GetHashCode() + " " + collision.GetHashCode());
            }
        }
    }
}
