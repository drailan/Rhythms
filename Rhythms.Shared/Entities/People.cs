using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Rhythms.Shared.Entities
{
	public class People : IEnumerable<Entry>
	{
		public List<Entry> _people { get; set; }

		public People(List<Entry> person)
		{
			_people = person;
		}

		public Entry this[int index]
		{
			get { return _people[index]; }
			set { _people.Insert(index, value); }
		}

		public IEnumerator<Entry> GetEnumerator()
		{
			return _people.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
