
using HeightmapVisualizer.src.Scene;
using System.Numerics;

namespace HeightmapVisualizer.src.Components.Physics.Gravity
{
    internal class UniversalGravityPhysicsModule : GravityPhysicsModule
    {
        internal float GravitationalConstant { get; private set; }
        internal UniversalGravityPhysicsModule SetGravitationalConstant(float gravitationalConstant)
        {
            this.GravitationalConstant = gravitationalConstant;
            return this;
        }
        internal UniversalGravityPhysicsModule()
        {
            GravitationalConstant = 6.6743e-11f;
        }

        internal override void Gravity(PhysicsComponent physics)
        {
            PhysicsComponent[] objects = IDManager.GetObjectsByType<PhysicsComponent>().ToArray();

            Vector3 force = new();

            foreach (PhysicsComponent obj in objects)
            {
                if (obj.Equals(physics)) continue;

                var direction = Vector3.Normalize(obj.Gameobject.Transform.Position - physics.Gameobject.Transform.Position);
                var distance = Vector3.Distance(physics.Gameobject.Transform.Position, obj.Gameobject.Transform.Position);
                var forceOfGravity = GravitationalConstant * (physics.Mass * obj.Mass) / (MathF.Max(distance * distance, 1e-7f));
                force += direction * forceOfGravity;
            }

            var acceleration = force / physics.Mass;
            physics.SetVelocity(physics.Velocity + acceleration);
        }
    }
}
