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
		public GraphInnerState[] States { get; set; }

		public GraphState(IGraph p, IEnumerable<GraphInnerState> s)
		{
			Parent = p;
			States = s.ToArray();
		}

		public GraphState(DateTime d, IEnumerable<GraphInnerState> s)
		{
			States = s.ToArray();
			Date = d;
		}
	}
}
