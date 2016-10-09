using System;
using System.Collections.Generic;
using System.Text;

namespace Rhythms.Shared.Entities
{
	public class Point
	{
		public float X { get; set; }
		public float Y { get; set; }
		public DateTime Date { get; set; }

		public Point(float x, float y, DateTime date)
		{
			X = x;
			Y = y;
			Date = date;
		}
	}
}
