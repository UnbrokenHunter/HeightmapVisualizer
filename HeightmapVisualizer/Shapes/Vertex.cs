using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HeightmapVisualizer.Shapes
{
    internal class Vertex
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public List<Edge> Edges = new();

        public Vertex(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vertex(Vector3 position)
        {
            X = position.X;
            Y = position.Y;
            Z = position.Z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }
    }
}
