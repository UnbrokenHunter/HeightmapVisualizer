using HeightmapVisualizer.src.Scene;
using System.Numerics;

namespace HeightmapVisualizer.src.Components.Collision
{
    internal class MeshAABBCollisionComponent : CollisionComponent
    {
        private MeshComponent meshComponent;

        public override void Init(Gameobject gameobject)
        {
            base.Init(gameobject);

            // Either there are no mesh components, somehow, or there is only the debug one
            if (Gameobject.TryGetComponents(out MeshComponent[] meshes) < 2)
                throw new Exception("No mesh on game object. A Mesh is required in order to use a mesh collider");

            foreach (MeshComponent mesh in meshes)
            {
                if (!mesh.Equals(DebugMesh))
                {
                    meshComponent = mesh;
                    break;
                }
            }
        }

        public override CollisionComponent SetCollider(dynamic[] mesh)
        {
            if (mesh.Length != 1)
                throw new ArgumentException("There should only be one parameter");

            if (mesh[0] is MeshComponent)
                meshComponent = mesh[0];
            else throw new ArgumentException("Set Collider Expects a Mesh Component");

            return this;
        }

        private protected override void ColliderCalculation(List<CollisionComponent> colliders)
        {
            var bounds = meshComponent.GetAxisAlignedBounds();
            ColliderMaxCorner = bounds.Item1;
            ColliderMinCorner = bounds.Item2;

            Gameobject.TryGetComponents(out MeshComponent[] meshes);

            foreach (var collision in colliders)
            {
                if (collision.Equals(this)) continue;

                //if (AABBIntersect(collision))
                    //Console.WriteLine("Colliding" + GetHashCode() + " " + collision.GetHashCode());
            }
        }
    }
}
