using Rhythms.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhythms.Shared.Entities
{
	public class GraphState
	{
		public IGraph Parent { get; set; }
		public DateTime Date { get; set; }
		public GraphInnerState[] FirstStates { get; set; }
		public GraphInnerState[] SecondStates { get; set; }

		public GraphState(IGraph p, IEnumerable<GraphInnerState> s, IEnumerable<GraphInnerState> y = null)
		{
			Parent = p;
			FirstStates = s.ToArray();
			SecondStates = y?.ToArray();
		}

		public GraphState(DateTime d, IEnumerable<GraphInnerState> s, IEnumerable<GraphInnerState> y = null)
		{
			FirstStates = s.ToArray();
			SecondStates = y?.ToArray();
			Date = d;
		}
	}
}
