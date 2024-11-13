using HeightmapVisualizer.src.Scene;
using System.Numerics;

namespace HeightmapVisualizer.src.Components.Camera
{
    internal abstract class CameraComponent : Component
    {
        public float Aspect { get; private set; }
        public Vector2 Fov { get; private set; }
        public float NearClippingPlane { get; private set; }
        public float FarClippingPlane { get; private set; }
        public int Priority { get; private set; }
        public float FocalLength => (float)(Window.Instance.Width / (2 * Math.Tan(Fov.X / 2)));

        public CameraComponent(
            float aspect = 16f / 9f,
            float fov = 90f,
            float nearClippingPlane = 0.0001f,
            float farClippingPlane = 100000f,
            int priority = 10)
        {
            Aspect = aspect;
            Fov = new Vector2(fov, fov / aspect);
            NearClippingPlane = nearClippingPlane;
            FarClippingPlane = farClippingPlane;
            Priority = priority;
        }


        /// <summary>
        /// Project a 3D point to 2D screen space with perspective
        /// </summary>
        /// <param name="point"></param>
        /// <returns>A Tuple with the vector, and whether or not it is on screen.</returns>
        public abstract Vector2 ProjectPoint(Vector3 point, Rectangle bounds);

        public void SetPriority(int priority)
        {
            Priority = priority;
            Window.Instance.Scene.UpdateSelectedCamera();
        }
    }
}


//public override Mesh? GetRenderable()
//{
//    var debug = false;
//    if (debug)
//    {
//        var combined = MeshUtility.CombineGeometry(
//            MeshUtility.CombineGeometry(
//                Line.CreateRay(Transform.Position, Transform.Forward, 5, Color.CornflowerBlue),
//                Line.CreateRay(Transform.Position, Transform.Up, 5, Color.Green)
//            ),
//            Line.CreateRay(Transform.Position, Transform.Right, 5, Color.Red)
//        );
//        return combined;
//    }
//    return null;
//}