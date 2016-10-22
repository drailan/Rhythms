using System;
using System.Collections.Generic;
using System.Text;

namespace Rhythms.Shared.Entities
{
	public class Entry
	{
		public string FirstName { get; set; }
		public DateTime FirstBirthDate { get; set; }

		public string SecondName { get; set; }

		public DateTime SecondBirthDate { get; set; }

		public Entry(string name, DateTime bDay)
		{
			FirstName = name;
			FirstBirthDate = bDay;
		}

		public Entry(string firstName, string secondName, DateTime firstDate, DateTime seconDate)
		{
			FirstName = firstName;
			SecondName = secondName;

			FirstBirthDate = firstDate;
			SecondBirthDate = seconDate;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			var other = obj as Entry;

			return FirstName.Equals(other.FirstName, StringComparison.OrdinalIgnoreCase) &&
				(SecondName ?? string.Empty).Equals(other.SecondName ?? string.Empty, StringComparison.OrdinalIgnoreCase) &&
				FirstBirthDate == other.FirstBirthDate && SecondBirthDate == other.SecondBirthDate;
		}

		public override int GetHashCode() => FirstName.GetHashCode() ^ SecondName?.GetHashCode() ?? 0 ^ FirstBirthDate.GetHashCode() ^ SecondBirthDate.GetHashCode();
	}
}
