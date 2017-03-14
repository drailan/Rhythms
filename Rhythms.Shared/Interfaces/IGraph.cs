using Rhythms.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rhythms.Shared.Interfaces
{
	public interface IGraph
	{
#if XAMARIN_ADNROID
		List<Point> GetPoints();

		int GetColor();

		string GetName();
#endif
		void GenerateGraph(int totalDays, int scale, DateTime selectedDate);

		IEnumerable<GraphInnerState> GetCurrentStates(int todalDays, DateTime selectedDate);

		List<Point> Points { get; set; }

		GraphInnerState GetCurrentState(int totalDays, int day, DateTime selectedDate);
	}
}
