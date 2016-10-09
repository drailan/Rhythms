using Rhytms.Entities.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rhytms.Entities.Shared
{
	public class GraphState
	{
		public IGraph Parent { get; set; }
		public DateTime Date { get; set; }
		public GraphInnerState[] States { get; set; }

		public GraphState(IGraph p, GraphInnerState[] s)
		{
			Parent = p;
			States = s;
		}
	}
}
