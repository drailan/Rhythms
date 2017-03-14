using Rhythms.Shared.Entities;
using Rhythms.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhythms.Shared.Extensions
{
	public static class IGraphExtensions
	{
		public static IEnumerable<GraphInnerState> GetGraphStates(this IEnumerable<IGraph> source, int totalDays, int day, DateTime selectedDate)
		{
			return source
				.Select(s => s.GetCurrentState(totalDays, day, selectedDate));
		}
	}
}
