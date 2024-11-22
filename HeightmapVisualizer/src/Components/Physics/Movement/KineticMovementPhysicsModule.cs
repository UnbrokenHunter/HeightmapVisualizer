
namespace HeightmapVisualizer.src.Components.Physics.Movement
{
    internal class KineticMovementPhysicsModule : MovementPhysicsModule
    {
        internal override void Movement(PhysicsComponent physics)
        {
            physics.Gameobject.Transform.Move(physics.Velocity);
        }
    }
}
