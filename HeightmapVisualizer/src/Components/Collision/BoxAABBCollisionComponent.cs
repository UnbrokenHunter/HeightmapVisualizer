using System.Numerics;

namespace HeightmapVisualizer.src.Components.Collision
{
    internal class BoxAABBCollisionComponent : CollisionComponent
    {
        public override CollisionComponent SetCollider(dynamic[] vector3s)
        {
            if (vector3s.Length > 2)
                throw new ArgumentException("There should only be one or two parameters");

            if (vector3s[0] is Vector3)
            {
                ColliderMaxCorner = -vector3s[0] / 2;
                ColliderMinCorner =  vector3s[0] / 2;
            }
            else throw new ArgumentException("Set Collider Expects a Vector3");

            if (vector3s[1] is Vector3)
            {
                ColliderMaxCorner += vector3s[1];
                ColliderMinCorner += vector3s[1];
            }
            else if (vector3s.Length == 2) throw new ArgumentException("Set Collider Expects a Vector3");

            return this;
        }

        private protected override void ColliderCalculation(List<CollisionComponent> colliders)
        {
            foreach (var collision in colliders)
            {
                if (collision.Equals(this)) continue;

                if (AABBIntersect(collision))
                    Collide(collision);
            }
        }
    }
}
