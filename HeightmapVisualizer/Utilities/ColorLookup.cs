using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeightmapVisualizer.Utilities
{
	public static class ColorLookup
	{
		private static Dictionary<Color, Brush> brushes = new Dictionary<Color, Brush>();
		public static Brush FindOrGetBrush(Color color)
		{
			brushes.TryGetValue(color, out Brush? value);

			if (value == null)
			{
				value = new SolidBrush(color);
				brushes.Add(color, value);
			}

			return value;
		}

		private static Dictionary<Color, Pen> pens = new Dictionary<Color, Pen>();
		public static Pen FindOrGetPen(Color color)
		{
			pens.TryGetValue(color, out Pen? value);

			if (value == null)
			{
				value = new Pen(color);
				pens.Add(color, value);
			}

			return value;
		}
	}
}
