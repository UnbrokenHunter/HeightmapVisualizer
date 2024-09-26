using HeightmapVisualizer.Primitives;
using HeightmapVisualizer.Scene;
using HeightmapVisualizer.Units;

namespace HeightmapVisualizer.Shapes
{
	internal abstract class Shape : Gameobject
	{
		public Shape(Transform transform) : base(transform) { }

		public void DrawPoints()
		{
			throw new NotImplementedException();
		}

		public void DrawLines()
		{
			throw new NotImplementedException();
		}

		public void DrawFaces()
		{
			throw new NotImplementedException();
		}

	}
}
