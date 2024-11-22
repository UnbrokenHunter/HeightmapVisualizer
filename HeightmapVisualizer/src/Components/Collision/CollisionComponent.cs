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

		internal static Vector3 CalculateCollisionNormal(CollisionComponent a, CollisionComponent b)
		{
			// Positions and corners of the colliders
			var posA = a.Gameobject.Transform.Position;
			var minA = posA + a.ColliderMinCorner;
			var maxA = posA + a.ColliderMaxCorner;

			var posB = b.Gameobject.Transform.Position;
			var minB = posB + b.ColliderMinCorner;
			var maxB = posB + b.ColliderMaxCorner;

			// Compute centers of the colliders
			var centerA = (minA + maxA) / 2;
			var centerB = (minB + maxB) / 2;

			// Compute overlap direction
			Vector3 overlap = centerB - centerA;

			// Determine which axis has the smallest penetration depth
			Vector3 penetrationDepth = new Vector3(
				MathF.Min(maxA.X - minB.X, maxB.X - minA.X),
				MathF.Min(maxA.Y - minB.Y, maxB.Y - minA.Y),
				MathF.Min(maxA.Z - minB.Z, maxB.Z - minA.Z)
			);

			// Find the smallest penetration axis
			float minDepth = MathF.Min(penetrationDepth.X, MathF.Min(penetrationDepth.Y, penetrationDepth.Z));

			Vector3 normal;
			if (minDepth == penetrationDepth.X)
				normal = new Vector3(MathF.Sign(overlap.X), 0, 0); // X-axis normal
			else if (minDepth == penetrationDepth.Y)
				normal = new Vector3(0, MathF.Sign(overlap.Y), 0); // Y-axis normal
			else
				normal = new Vector3(0, 0, MathF.Sign(overlap.Z)); // Z-axis normal

			return Vector3.Normalize(normal); // Normalize to make it a unit vector
		}

		#endregion
	}
}
