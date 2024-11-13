using HeightmapVisualizer.src.Factories;
using HeightmapVisualizer.src.Scene;
using System.Numerics;

namespace HeightmapVisualizer.src.Components.Collision
{
    internal abstract class CollisionComponent : Component
    {
        public CollisionComponent()
        {
            IsDebug = true;
            ColliderSize = Vector3.One;
            ColliderOffset = Vector3.Zero;
        }

        public override void Init(Gameobject gameobject)
        {
            base.Init(gameobject);

            DebugMesh = new MeshComponent(Cuboid.CreateCentered(ColliderSize, ColliderSize / 2 + ColliderOffset))
                .SetWireframe(true)
                .SetColor(Color.LightGreen);

            Gameobject.AddComponent(DebugMesh);
        }

        public override void Update()
        {
            UpdateDebugOutlines();

            ColliderCalculation(IDManager.GetObjectsByType<CollisionComponent>());
        }

        private protected abstract void ColliderCalculation(List<CollisionComponent> colliders);

        #region Properties

        public Vector3 ColliderSize { get; private protected set; }
        public Vector3 ColliderOffset { get; private protected set; }
        public abstract CollisionComponent SetCollider(dynamic[] args);

        public bool IsDebug { get; private set; }
        public CollisionComponent SetDebug(bool isDebug)
        {
            IsDebug = isDebug;
            return this;
        }

        private protected MeshComponent DebugMesh { get; private set; }
        private void UpdateDebugOutlines() => DebugMesh.SetFaces(IsDebug ?
                    Cuboid.CreateCentered(ColliderSize, ColliderSize / 2 + ColliderOffset) :
                    Array.Empty<MeshComponent.Face>());

        #endregion

        #region Logic 

        private protected bool AABBIntersect(CollisionComponent b)
        {
            var posA = Gameobject.Transform.Position + ColliderOffset;
            var minA = posA - ColliderSize / 2;
            var maxA = posA + ColliderSize / 2;

            var posB = b.Gameobject.Transform.Position;
            var minB = posB - b.ColliderSize / 2;
            var maxB = posB + b.ColliderSize / 2;

            return 
                minA.X <= maxB.X && maxA.X >= minB.X && // X-axis overlap
                minA.Y <= maxB.Y && maxA.Y >= minB.Y && // Y-axis overlap
                minA.Z <= maxB.Z && maxA.Z >= minB.Z    // Z-axis overlap
            ;
        }

        #endregion
    }
}
