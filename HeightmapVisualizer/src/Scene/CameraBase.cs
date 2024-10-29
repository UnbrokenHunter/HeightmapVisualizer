﻿
using HeightmapVisualizer.src.Components;
using System.Numerics;

namespace HeightmapVisualizer.src.Scene
{
    public abstract class CameraBase
    {
        public Rectangle Space { get; private set; } // Screen Space
        public float Aspect { get; private set; }
        public Vector2 Fov { get; private set; }
        public float NearClippingPlane { get; private set; }
        public float FarClippingPlane { get; private set; }
        public float FocalLength => (float)(Window.Instance.Width / (2 * Math.Tan(Fov.X / 2)));

        public CameraBase(Rectangle space,
            float aspect = 16f / 9f,
            float fov = 90f,
            float nearClippingPlane = 0.0001f,
            float farClippingPlane = 100000f)
        {
            this.Space = space;
            this.Aspect = aspect;
            this.Fov = new Vector2(fov, fov / aspect);
            this.NearClippingPlane = nearClippingPlane;
            this.FarClippingPlane = farClippingPlane;
        }


        /// <summary>
        /// Project a 3D point to 2D screen space with perspective
        /// </summary>
        /// <param name="point"></param>
        /// <returns>A Tuple with the vector, and whether or not it is on screen.</returns>
        public abstract Tuple<Vector2, bool> ProjectPoint(Vector3 point);

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