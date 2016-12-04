using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rhythms.Shared.Interfaces;

namespace Rhythms.Shared.Data
{
	public class DataProvider : IDataProvider
	{
		public async Task<PeopleData> GetPeople()
		{
			var res = await Task.Factory.StartNew(Deserialize);

			return res;
		}

		public async Task SavePeople(EntryData[] data)
		{
			var peopleData = new PeopleData() { People = data };

			var json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(peopleData));

			File.WriteAllText("Data/Data.json", json);
		}

		private PeopleData Deserialize()
		{
			var str = File.ReadAllText("Data/Data.json");

			return JsonConvert.DeserializeObject<PeopleData>(str);
		}
	}
}
