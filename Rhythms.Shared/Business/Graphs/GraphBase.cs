#if XAMARIN_ANDROID
using AG = Android.Graphics;
#endif
using Rhythms.Shared.Entities;
using Rhythms.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rhythms.Shared.Business.Graphs
{
	public abstract class GraphBase : IGraph
	{
		public List<Point> Points { get; set; }

#if XAMARIN_ANDROID
		protected abstract AG.Color Color { get; }
#endif

		protected abstract string Name { get; }

		protected abstract int Period { get; }

		protected GraphBase()
		{
			Points = new List<Point>();
		}

		private static float GetY(int quarter, float dayInPeriod, float daysInQuarter)
		{
			float state = 0.0f;

			switch (quarter)
			{
				case 1:
					state = dayInPeriod;
					break;
				case 2:
					state = daysInQuarter * 2 - dayInPeriod;
					break;
				case 3:
					state = -dayInPeriod + daysInQuarter * 2;
					break;
				case 4:
					state = -daysInQuarter * 4 + dayInPeriod;
					break;
				default:
					break;
			}

			return state;
		}

		public void GenerateGraph(int totalDays, int scale)
		{
			Points = new List<Point>();

			float daysInQuarter = (float)Period / 4;

			for (int day = 0; day <= scale; day++)
			{
				var state = GetState(totalDays, day, scale, daysInQuarter);

				float t = 0;

				if (state < 0.0)
				{
					t = state / (Period / 4);
				}
				else if (state > 0.0)
				{
					t = state / (Period / 4);
				}

				Points.Add(new Point(day, state, DateTime.Today - new TimeSpan(scale / 2 - day, 0, 0, 0)));
			}
		}

		protected static int GetQuarter(double dayInPeriod, double daysInQuarter)
		{
			return dayInPeriod <= daysInQuarter ? 1 :
					dayInPeriod <= daysInQuarter * 2 ? 2 :
							dayInPeriod <= daysInQuarter * 3 ? 3 :
									dayInPeriod <= daysInQuarter * 4 ? 4 : -1;
		}

		private float GetState(int totalDays, int day, int scale, float daysInQuarter)
		{
			var remainder = ((float)(totalDays - scale / 2 + day) / Period) % 1.0f;
			var dayInPeriod = Period * remainder;
			var quarter = GetQuarter(dayInPeriod, daysInQuarter);

			return GetY(quarter, dayInPeriod, daysInQuarter);
		}

		public GraphInnerState GetCurrentState(int totalDays, int day)
		{
			var daysInQuarter = (float)Period / 4;
			float remainder = ((float)(totalDays + day) / Period) % 1.0f;
			float dayInPeriod = Period * remainder;

			int quarter = GetQuarter(dayInPeriod, daysInQuarter);

			float state = GetY(quarter, dayInPeriod, daysInQuarter);

			float t = 0;

			t = state / (Period / 4);

			var gs = new GraphInnerState()
			{
				IsGrowing = quarter == 1 || quarter == 4,
				State = t,
				Date = DateTime.Now.AddDays(day)
			};

			return gs;
		}

		public IEnumerable<GraphInnerState> GetCurrentStates(int totalDays)
		{
			var daysInQuarter = (float)Period / 4;
			var states = new List<GraphInnerState>(3);

			var days = new int[] { -3, -2, -1, 0, 1, 2, 3 };

			foreach (int day in days)
			{
				float remainder = ((float)(totalDays + day) / Period) % 1.0f;
				float dayInPeriod = Period * remainder;

				int quarter = GetQuarter(dayInPeriod, daysInQuarter);

				float state = GetY(quarter, dayInPeriod, daysInQuarter);

				float t = 0;

				t = state / (Period / 4);

				var gs = new GraphInnerState()
				{
					IsGrowing = quarter == 1 || quarter == 4,
					State = t,
					Date = DateTime.Now.AddDays(day)
				};

				states.Add(gs);
			}

			return states;
		}
	}
}
