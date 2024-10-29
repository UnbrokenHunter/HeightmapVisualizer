
using HeightmapVisualizer.src.Components;
using System.Numerics;

namespace HeightmapVisualizer.src.Scene
{
    public abstract class CameraBase : IComponent
    {
		public Gameobject? Gameobject { get; set; }

        public float Aspect { get; private set; }
        public Vector2 Fov { get; private set; }
        public float NearClippingPlane { get; private set; }
        public float FarClippingPlane { get; private set; }
        public int Priority { get; private set; }
        public float FocalLength => (float)(Window.Instance.Width / (2 * Math.Tan(Fov.X / 2)));

        public CameraBase(
            float aspect = 16f / 9f,
            float fov = 90f,
            float nearClippingPlane = 0.0001f,
            float farClippingPlane = 100000f,
            int priority = 10)
        {
            this.Aspect = aspect;
            this.Fov = new Vector2(fov, fov / aspect);
            this.NearClippingPlane = nearClippingPlane;
            this.FarClippingPlane = farClippingPlane;
            this.Priority = priority;
        }


        /// <summary>
        /// Project a 3D point to 2D screen space with perspective
        /// </summary>
        /// <param name="point"></param>
        /// <returns>A Tuple with the vector, and whether or not it is on screen.</returns>
        public abstract Tuple<PointF, bool>? ProjectPoint(Vector3 point, Rectangle bounds);

        public void SetPriority(int priority)
        {
            this.Priority = priority;
            Window.Instance.Scene.UpdateSelectedCamera();
        }

		public void Init(Gameobject gameobject)
		{
			this.Gameobject = gameobject;
		}

		public void Update()
		{
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