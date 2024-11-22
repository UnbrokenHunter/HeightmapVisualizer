using HeightmapVisualizer.src.Factories;
using HeightmapVisualizer.src.Scene;
using System.Numerics;

namespace HeightmapVisualizer.src.Components.Collision
{
    internal abstract partial class CollisionComponent : Component
    {
		public CollisionComponent()
        {
            ColliderMinCorner = -Vector3.One / 2;
            ColliderMaxCorner = Vector3.One / 2;
        }

		internal abstract void ColliderCalculation();

		#region Properties

        public Vector3 ColliderMinCorner { get; private protected set; }
        public Vector3 ColliderMaxCorner { get; private protected set; }
        public abstract CollisionComponent SetCollider(dynamic[] args);
            
		#endregion

		#region Logic 

		internal void Collide(CollisionInfo collision)
        {
            Gameobject.OnCollision?.Invoke(collision);
		}

        internal static bool AABBIntersect(CollisionComponent a, CollisionComponent b)
        {
            var posA = a.Gameobject.Transform.Position;
            var minA = posA + a.ColliderMinCorner;
            var maxA = posA + a.ColliderMaxCorner;

            var posB = b.Gameobject.Transform.Position;
            var minB = posB + b.ColliderMinCorner;
            var maxB = posB + b.ColliderMaxCorner;

            return 
                minA.X <= maxB.X && maxA.X >= minB.X && // X-axis overlap
                minA.Y <= maxB.Y && maxA.Y >= minB.Y && // Y-axis overlap
                minA.Z <= maxB.Z && maxA.Z >= minB.Z    // Z-axis overlap
            ;
        }

		#endregion
	}
}
