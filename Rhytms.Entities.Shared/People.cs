using System;
using System.Collections.Generic;
using System.Text;

namespace Rhytms.Entities.Shared
{
	public class People
	{
		public Person[] _people { get; set; }

		public People(Person[] person)
		{
			_people = person;
		}

		public Person getAt(int i)
		{
			return _people[i];
		}
	}
}
