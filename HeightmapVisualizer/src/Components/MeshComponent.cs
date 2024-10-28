﻿using HeightmapVisualizer.src.Scene;
using HeightmapVisualizer.src.Utilities;
using System.Numerics;

namespace HeightmapVisualizer.src.Components
{
    public class MeshComponent : IComponent
    {
        private Gameobject? gameobject { get; set; }

        public void Init(Gameobject gameobject)
        {
            this.gameobject = gameobject;
        }

        public void Update() { }

        public MeshComponent(Face[] faces, Color? color = null, bool isWireframe = false)
        {
            // You cannot set black as a default value for some reason
            this.Color = color ?? Color.Black;

            this.IsWireframe = isWireframe;

            // Triangulate the faces and store the resulting triangles
            faces.ToList().ForEach(e => e.Triangulate(this));

            // Adds all the faces to a local list
            faces.ToList().ForEach(e => Faces.Add(e.Points, e));
        }

        public void Render(Graphics g, CameraComponent cam)
        {
            if (gameobject == null)
                return;

            // Draw all the edges in the mesh
            foreach (var tri in Tris)
            { 
                var p1 = cam.ProjectPoint(gameobject.Transform.ToLocalSpace(tri.Value.V1.LocalPosition, true));
                var p2 = cam.ProjectPoint(gameobject.Transform.ToLocalSpace(tri.Value.V2.LocalPosition, true));
                var p3 = cam.ProjectPoint(gameobject.Transform.ToLocalSpace(tri.Value.V3.LocalPosition, true));

                static PointF toPointF(Vector2 v) => new(v.X, v.Y);

                // Atleast one point on screen
                if (p1.Item2 || p2.Item2 || p3.Item2)
                {
                    var p = new PointF[] { 
                        toPointF(p1.Item1), 
                        toPointF(p2.Item1), 
                        toPointF(p3.Item1) 
                    };


                    // Fill Tri
                    if (!IsWireframe)
                        g.FillPolygon(ColorLookup.FindOrGetBrush(tri.Value.Face.Color ?? GetColor()), p);

                    
                    g.DrawPolygon(ColorLookup.FindOrGetPen(tri.Value.Face.Color ?? GetColor()), p);
                }
            }
        }

        #region Properties

        private Color Color { get; set; }
        private bool IsWireframe { get; set; }

        public Color GetColor() => Color;
        public bool GetWireFrame() => IsWireframe;

        public MeshComponent SetColor(Color color) { this.Color = color; return this; }
        public MeshComponent SetWireframe(bool isWireframe) { this.IsWireframe = isWireframe; return this; }

        #endregion

        #region Primitives

        private Dictionary<Vector3[], Face> Faces = new Dictionary<Vector3[], Face>();
        private Dictionary<(Vector3, Vector3, Vector3), Tri> Tris = new();
        private Dictionary<(Vector3, Vector3), Edge> Edges = new();
        private Dictionary<Vector3, Vertex> Vertices = new();

        public class Face : IEquatable<Face>
        {
            private Tri[]? Tris;

            public Vector3[] Points { get; private set; }

            public Color? Color { get; private set; }

            public string? Name { get; private set; }

            public Face(Vector3[] points, Color? color = null, string? name = null)
            {
                this.Points = points;
                this.Color = color;
                this.Name = name;
            }

            public void Triangulate(MeshComponent mesh)
            {
                // Note: This method only works correctly for convex polygons.
                // To handle concave polygons or polygons with holes, a more advanced triangulation algorithm,
                // such as ear clipping or Delaunay triangulation, would be required in the future.

                List<Tri> tris = new();

                int n = Points.Length;

                // Mesh is a Line
                if (n == 2)
                {
                    if (GetOrCreateTri(Points[0], Points[0], Points[1], out var tri1))
                        tris.Add(tri1);
                }

                // Mesh is not a line
                else
                {
                    for (int i = 1; i < n - 1; i++)
                    {
                        if (GetOrCreateTri(Points[0], Points[i], Points[i + 1], out var tri2))
                            tris.Add(tri2);
                    }
                }

                bool GetOrCreateTri(Vector3 v1, Vector3 v2, Vector3 v3, out Tri tri)
                {
                    if (mesh.Tris.TryGetValue((v1, v2, v3), out var existingTri1))
                    {
                        tri = existingTri1;
                        return false;
                    }
                    else if (mesh.Tris.TryGetValue((v1, v3, v2), out var existingTri2))
                    {
                        tri = existingTri2;
                        return false;
                    }
                    else if (mesh.Tris.TryGetValue((v2, v1, v3), out var existingTri3))
                    {
                        tri = existingTri3;
                        return false;
                    }
                    else if (mesh.Tris.TryGetValue((v2, v3, v1), out var existingTri4))
                    {
                        tri = existingTri4;
                        return false;
                    }
                    else if (mesh.Tris.TryGetValue((v3, v1, v2), out var existingTri5))
                    {
                        tri = existingTri5;
                        return false;
                    }
                    else if (mesh.Tris.TryGetValue((v3, v2, v1), out var existingTri6))
                    {
                        tri = existingTri6;
                        return false;
                    }
                    else
                    {
                        tri = new Tri(mesh, v1, v2, v3, this);
                        mesh.Tris[(v1, v2, v3)] = tri;

                        // No matching tri found in list, returns true because a new tri was created
                        return true;
                    }
                }
            }

            #region Overriding Equality

            public override bool Equals(object? obj)
            {
                if (obj != null)
                    return obj is Face face && Equals(face);
                return false;
            }

            public bool Equals(Face? other)
            {
                if (other == null) 
                    return false;

                if (Points.Length != other.Points.Length)
                    return false;

                for (int i = 0; i < Points.Length; i++)
                {
                    if (!Points[i].Equals(other.Points[i]))
                        return false;
                }

                return true;
            }

            public override int GetHashCode()
            {
                int hash = 17;
                foreach (var point in Points)
                {
                    hash = hash * 31 + point.GetHashCode();
                }
                return hash;
            }

            #endregion
        }

        private struct Tri : IEquatable<Tri>
        {
            public Face Face { get; }

            public Edge E1 { get; }
            public Edge E2 { get; }
            public Edge E3 { get; }

            public Vertex V1 { get; }
            public Vertex V2 { get; }
            public Vertex V3 { get; }

            public Tri(MeshComponent mesh, Vector3 p1, Vector3 p2, Vector3 p3, Face face)
            {
                // Nested function to handle the GetOrCreate logic for edges
                Edge GetOrCreateEdge(Vector3 v1, Vector3 v2)
                {
                    if (mesh.Edges.TryGetValue((v1, v2), out var existingEdge1))
                    {
                        return existingEdge1;
                    }
                    else if (mesh.Edges.TryGetValue((v2, v1), out var existingEdge2))
                    {
                        return existingEdge2;
                    }
                    else
                    {
                        var newEdge = new Edge(mesh, v1, v2);
                        mesh.Edges[(v1, v2)] = newEdge;
                        return newEdge;
                    }
                }

                // Assign edges using the nested GetOrCreateEdge method
                E1 = GetOrCreateEdge(p1, p2);
                E2 = GetOrCreateEdge(p2, p3);
                E3 = GetOrCreateEdge(p3, p1);

                // Assign Vertex Values
                V1 = E1.V1;
                V2 = E1.V2;
                V3 = E1.V1.Equals(E2.V1) || E1.V2.Equals(E2.V1) ? E2.V2 : E2.V1;

                Face = face;
            }

            #region Equality

            public readonly bool Equals(Tri other)
            {
                return V1.Equals(other.V1) &&
                       V2.Equals(other.V2) &&
                       V3.Equals(other.V3);
            }

            public override readonly bool Equals(object? obj)
            {
                if (obj != null)
                    return obj is Tri tri && Equals(tri);
                return false;
            }

            public override readonly int GetHashCode()
            {
                // Combine hash codes of the edges. Order matters since it's a triangle.
                int hash1 = V1.GetHashCode();
                int hash2 = V2.GetHashCode();
                int hash3 = V3.GetHashCode();

                return hash1 ^ hash2 ^ hash3; // XOR to combine the hashes
            }

            public override readonly string ToString()
            {
                return $"Triangle: ({E1}, {E2}, {E3})";
            }

            #endregion
        }

        private struct Edge : IEquatable<Edge>
        {
            public Vertex V1 { get; }
            public Vertex V2 { get; }

            public Edge(MeshComponent mesh, Vector3 p1, Vector3 p2)
            {
                // Nested function to handle the GetOrCreate logic for vertices
                Vertex GetOrCreateVertex(Vector3 position)
                {
                    if (mesh.Vertices.TryGetValue(position, out var existingVertex))
                    {
                        return existingVertex;
                    }
                    else
                    {
                        var newVertex = new Vertex(position);
                        mesh.Vertices[position] = newVertex;
                        return newVertex;
                    }
                }

                // Assign vertices using the nested GetOrCreateVertex method
                this.V1 = GetOrCreateVertex(p1);
                this.V2 = GetOrCreateVertex(p2);
            }

            #region Equality

            public readonly bool Equals(Edge other)
            {
                // Edges are equal if they share the same vertices, regardless of the order
                return V1.Equals(other.V1) && V2.Equals(other.V2) ||
                       V1.Equals(other.V2) && V2.Equals(other.V1);
            }

            public override readonly bool Equals(object? obj)
            {
                if (obj != null)
                    return obj is Edge edge && Equals(edge);
                return false;
            }

            public override int GetHashCode()
            {
                // Hash code combines both vertices' positions, order-independent
                int hash1 = V1.GetHashCode();
                int hash2 = V2.GetHashCode();
                return hash1 ^ hash2; // XOR for order-independence
            }

            public override readonly string ToString()
            {
                return $"Edge: ({V1}, {V2})";
            }

            #endregion
        }

        private struct Vertex : IEquatable<Vertex>
        {
            public Vector3 LocalPosition;

            public Vertex(Vector3 position)
            {
                LocalPosition = position;
            }

            #region Equality

            public readonly bool Equals(Vertex other)
            {
                return LocalPosition.Equals(other.LocalPosition);
            }

            public override readonly bool Equals(object? obj)
            {
                if (obj != null)
                    return obj is Vertex vertex && Equals(vertex);
                return false;
            }

            public override readonly int GetHashCode()
            {
                return LocalPosition.GetHashCode();
            }

            public override readonly string ToString()
            {
                return $"Vertex: {LocalPosition}";
            }

            #endregion
        }

        #endregion
    }
}

