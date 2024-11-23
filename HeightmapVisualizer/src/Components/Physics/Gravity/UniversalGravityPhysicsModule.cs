
using HeightmapVisualizer.src.Scene;
using System.Numerics;

namespace HeightmapVisualizer.src.Components.Physics.Gravity
{
    internal class UniversalGravityPhysicsModule : GravityPhysicsModule
    {
        private const float GravitationalConstant = 0.000000000066743f;

        internal override void Gravity(PhysicsComponent physics)
        {
            PhysicsComponent[] objects = IDManager.GetObjectsByType<PhysicsComponent>().ToArray();

            Vector3 force = new();

            foreach (PhysicsComponent obj in objects)
            {
                if (obj.Equals(physics)) continue;

                var direction = Vector3.Normalize(obj.Gameobject.Transform.Position - physics.Gameobject.Transform.Position);
                var distance = Vector3.Distance(physics.Gameobject.Transform.Position, obj.Gameobject.Transform.Position);
                var forceOfGravity = GravitationalConstant * (physics.Mass + obj.Mass) / (distance * distance);
                force += direction * forceOfGravity;
            }

            physics.SetVelocity (physics.Velocity + force);
        }
    }
}
