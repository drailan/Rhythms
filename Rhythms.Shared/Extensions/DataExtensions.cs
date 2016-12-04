using System;
using System.Collections.Generic;
using System.Text;
using Rhythms.Shared.Data;
using Rhythms.Shared.Entities;

namespace Rhythms.Shared.Extensions
{
	public static class DataExtensions
	{
		public static Entry ToEntry(this EntryData data)
		{
			return new Entry(data.FirstName, data.SecondName, data.FirstBirthDate, data.SecondBirthDate);
		}

		public static EntryData ToEntryData(this Entry entry)
		{
			return new EntryData()
			{
				FirstBirthDate = entry.FirstBirthDate,
				FirstName = entry.FirstName,
				SecondBirthDate  = entry.SecondBirthDate,
				SecondName = entry.SecondName
			};
		}
	}
}
