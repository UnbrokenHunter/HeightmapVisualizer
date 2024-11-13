
using HeightmapVisualizer.src.Factories;
using HeightmapVisualizer.src.Scene;
using System.Drawing;
using System.Numerics;

namespace HeightmapVisualizer.src.Components
{
	internal class CollisionComponent : Component
	{

        #region Properties

        private Vector3 ColliderSize { get; set; }
        private bool IsDebug { get; set; }

        public Vector3 GetColliderSize() => ColliderSize;
        public CollisionComponent SetColliderSize(Vector3 colliderSize)
        {
            this.ColliderSize = colliderSize;
            return this;
        }

        public bool GetDebug() => IsDebug;
        public CollisionComponent SetDebug(bool isDebug) 
		{ 
            IsDebug = isDebug;
			return this; 
		}

        #endregion

        private MeshComponent DebugMesh { get; set; }
        private void UpdateDebugOutlines() => DebugMesh.SetFaces(IsDebug ?
                    Cuboid.CreateCentered(ColliderSize, ColliderSize / 2) :
                    Array.Empty<MeshComponent.Face>());

        public CollisionComponent()
        {
            this.ColliderSize = Vector3.One;
            this.IsDebug = true;
        }

        public override void Init(Gameobject gameobject)
        {
            base.Init(gameobject);

            DebugMesh = new MeshComponent(Cuboid.CreateCentered(ColliderSize, ColliderSize / 2)).SetWireframe(true).SetColor(Color.LightGreen);
            Gameobject.AddComponent(DebugMesh);
        }

        public override void Update()
        {
            UpdateDebugOutlines();

            var collisions = IDManager.GetObjectsByType<CollisionComponent>();
        
            foreach (var collision in collisions)
            {
                if (collision.Equals(this)) continue;

                if (AABBIntersect(this, collision))
                    Console.WriteLine("Colliding" + this.GetHashCode() + " " + collision.GetHashCode());
            }
        }

        private static bool AABBIntersect(CollisionComponent a, CollisionComponent b)
        {
            var posA = a.Gameobject.Transform.Position;
            var minA = posA - (a.ColliderSize / 2);
            var maxA = posA + (a.ColliderSize / 2);

            var posB = b.Gameobject.Transform.Position;
            var minB = posB - (b.ColliderSize / 2);
            var maxB = posB + (b.ColliderSize / 2);

            return (
                minA.X <= maxB.X && maxA.X >= minB.X && // X-axis overlap
                minA.Y <= maxB.Y && maxA.Y >= minB.Y && // Y-axis overlap
                minA.Z <= maxB.Z && maxA.Z >= minB.Z    // Z-axis overlap
            );
        }
    }
}
