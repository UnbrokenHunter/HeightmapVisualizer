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
            ColliderMinCorner = -Vector3.One / 2;
            ColliderMaxCorner = Vector3.One / 2;
        }

        public override void Init(Gameobject gameobject)
        {
            base.Init(gameobject);

            DebugMesh = new MeshComponent(Cuboid.CreateFromTwoPoints(ColliderMinCorner, ColliderMaxCorner))
                .SetWireframe(true)
                .SetColor(Color.LightGreen);

            Gameobject.AddComponent(DebugMesh);
        }

        public override void Update()
        {
            ColliderCalculation(IDManager.GetObjectsByType<CollisionComponent>());

            UpdateDebugOutlines();
        }

        private protected abstract void ColliderCalculation(List<CollisionComponent> colliders);

        #region Properties

        public Vector3 ColliderMinCorner { get; private protected set; }
        public Vector3 ColliderMaxCorner { get; private protected set; }
        public abstract CollisionComponent SetCollider(dynamic[] args);

        public bool IsDebug { get; private set; }
        public CollisionComponent SetDebug(bool isDebug)
        {
            IsDebug = isDebug;
            return this;
        }

        private protected MeshComponent DebugMesh { get; private set; }
        private void UpdateDebugOutlines() => DebugMesh.SetFaces(IsDebug ?
                    Cuboid.CreateFromTwoPoints(ColliderMinCorner, ColliderMaxCorner) :
                    Array.Empty<MeshComponent.Face>());

        #endregion

        #region Logic 

        private protected bool AABBIntersect(CollisionComponent b)
        {
            var posA = Gameobject.Transform.Position;
            var minA = posA + ColliderMinCorner;
            var maxA = posA + ColliderMaxCorner;

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
