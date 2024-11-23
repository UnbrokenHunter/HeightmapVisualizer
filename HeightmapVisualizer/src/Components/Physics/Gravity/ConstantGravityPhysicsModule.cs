
using System.Numerics;

namespace HeightmapVisualizer.src.Components.Physics.Gravity
{
    internal class ConstantGravityPhysicsModule : GravityPhysicsModule
    {
        internal Vector3 GravityVector { get; private set; }
        internal ConstantGravityPhysicsModule SetGravityVector(Vector3 gravityVector)
        {
            this.GravityVector = gravityVector;
            return this;
        }

        internal ConstantGravityPhysicsModule() 
        {
            GravityVector = new Vector3 (0, 9.81f, 0);
        }

        internal override void Gravity(PhysicsComponent physics)
        {
            physics.SetVelocity (physics.Velocity + (GravityVector * (float)Gameloop.Instance.DeltaTime));
        }
    }
}
