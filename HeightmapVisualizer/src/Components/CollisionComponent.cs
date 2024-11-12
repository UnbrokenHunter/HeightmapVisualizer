
using HeightmapVisualizer.src.Factories;
using HeightmapVisualizer.src.Scene;
using System.Drawing;
using System.Numerics;

namespace HeightmapVisualizer.src.Components
{
	internal class CollisionComponent : Component
	{
		private Vector3 ColliderSize = new Vector3(1, 1, 1);

        #region Debug

        private bool CreateDebugOnInit = false;
        private Guid IsDebug { get; set; }
        public Guid GetDebug() => IsDebug;
        public CollisionComponent SetDebug(bool isDebug) 
		{ 
			// Is now debug, but wasnt before
			if (isDebug && IsDebug == Guid.Empty)
			{
				if (Gameobject != null)
                    CreateDebugOutline();
                else
					CreateDebugOnInit = true;

            }

			if (!isDebug)
			{
				Gameobject.RemoveComponent((Component)IDManager.GetObjectById(IsDebug));
				IsDebug = Guid.Empty;
			}

			return this; 
		}

        public override void Init(Gameobject gameobject)
        {
            base.Init(gameobject);

			if (CreateDebugOnInit)
            {
                CreateDebugOutline();
            }
        }

        private void CreateDebugOutline()
        {
            var component = new MeshComponent(Cuboid.CreateCentered(ColliderSize)).SetWireframe(true).SetColor(Color.LightGreen);
            Gameobject.AddComponent(component);
            IsDebug = component.ID;
        }

        private void UpdateDebugOutlines()
        {
            if (IsDebug != Guid.Empty)
            {
                var debug = (MeshComponent)IDManager.GetObjectById(IsDebug);
                debug.SetFaces(Cuboid.CreateCentered(ColliderSize));
            }
        }

        #endregion 

        public override void Update()
        {
            UpdateDebugOutlines();

            var collisions = IDManager.GetObjectsByType<CollisionComponent>().Cast<CollisionComponent>().ToList();
        
            foreach (var collision in collisions)
            {
                if (collision.Equals(this)) continue;
                if (Intersect(this, collision))
                    Console.WriteLine("Colliding" + this.GetHashCode() + " " + collision.GetHashCode());
            }
        }

        private static bool Intersect(CollisionComponent a, CollisionComponent b)
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
