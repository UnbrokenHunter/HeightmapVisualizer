namespace HeightmapVisualizer.Units
{
    public class Transform
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        public Vector3 Forward => Quaternion.Rotate(new Vector3(0, 0, 1), Rotation);
        public Vector3 Up => Quaternion.Rotate(new Vector3(0, 1, 0), Rotation);
        public Vector3 Right => Quaternion.Rotate(new Vector3(1, 0, 0), Rotation);

        public Transform() : this(new Vector3(), new Quaternion()) { }

        public Transform(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        public override string ToString()
        {
            return $"{this.GetHashCode()},\n\tPosition: {Position},\n\tRotation: {Rotation},\n\tForward: {Forward}";
        }

        public Vector3 ToWorldSpace(Vector3 v)
        {
            return (Right * v.x) + (Up * v.y) + (Forward * v.z);

        }

        public void Move(Vector3 v)
        {
            Position += ToWorldSpace(v);
        }

    }
}
