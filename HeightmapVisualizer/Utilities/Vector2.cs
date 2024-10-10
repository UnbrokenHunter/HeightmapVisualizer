
namespace HeightmapVisualizer.Units
{
    public class Vector2
    {
        public float x { get; set; }
        public float y { get; set; }

        public Vector2() : this(0, 0) { }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vector2 operator +(Vector2 v1, float x)
        {
            return new Vector2(v1.x + x, v1.y + x);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }

        public static Vector2 operator -(Vector2 v1, float x)
        {
            return new Vector2(v1.x - x, v1.y - x);
        }

        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x * v2.x, v1.y * v2.y);
        }

        public static Vector2 operator *(Vector2 v1, float x)
        {
            return new Vector2(v1.x * x, v1.y * x);
        }

        public static Vector2 operator /(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x / v2.x, v1.y / v2.y);
        }

        public static Vector2 operator /(Vector2 v1, float x)
        {
            return new Vector2(v1.x / x, v1.y / x);
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }

    }
}
