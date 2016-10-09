using System;
using System.Collections.Generic;
using System.Text;

namespace Rhytms.Entities.Shared
{
	public class Point
	{
		public float X { get; set; }
		public float Y { get; set; }

		public Point(float x, float y)
		{
			X = x;
			Y = y;
		}
	}
}
