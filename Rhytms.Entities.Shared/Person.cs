using System;
using System.Collections.Generic;
using System.Text;

namespace Rhytms.Entities.Shared
{
	public class Person
	{
		public string Name { get; set; }
		public DateTime BirthDate { get; set; }

		public Person(string name, DateTime bDay)
		{
			Name = name;
			BirthDate = bDay;
		}
	}
}
