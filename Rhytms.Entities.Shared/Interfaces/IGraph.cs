using System;
using System.Collections.Generic;
using System.Text;

namespace Rhytms.Entities.Shared.Interfaces
{
	public interface IGraph
	{
		List<Point> GetPoints();

		int GetColor();

		string GetName();

		void GenerateGraph(int totalDays, int scale);

		GraphInnerState[] GetCurrentStates(int todalDays);
	}
}
