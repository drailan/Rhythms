using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Rhythms.Shared.Data;

namespace Rhythms.Shared.Interfaces
{
	public interface IDataProvider
	{
		Task<PeopleData> GetPeople();

		Task SavePeople(EntryData[] people);
	}
}
