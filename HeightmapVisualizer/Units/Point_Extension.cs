
namespace HeightmapVisualizer.Units
{
    public static class Point_Extension
    {
        public static Vector2 ToVector2(this Point p)
        {
            return new Vector2 (p.X, p.Y);
        } 
    }
}
