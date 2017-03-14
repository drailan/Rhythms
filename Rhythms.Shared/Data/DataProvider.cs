using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rhythms.Shared.Interfaces;

namespace Rhythms.Shared.Data
{
	public class DataProvider : IDataProvider
	{
		private string DocumentsPath => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

		public DataProvider()
		{
			Directory.CreateDirectory(Path.Combine(DocumentsPath, "Rhythms"));
		}

		public async Task<PeopleData> GetPeople()
		{
			var res = await Task.Run(() => Deserialize());

			return res;
		}

		public async Task SavePeople(EntryData[] data)
		{
			var peopleData = new PeopleData() { People = data };

			var json = await Task.Run(() => JsonConvert.SerializeObject(peopleData));

			File.WriteAllText(Path.Combine(DocumentsPath, "Rhythms", "Data.json"), json);
		}

		private PeopleData Deserialize()
		{
			var path = Path.Combine(DocumentsPath, "Rhythms", "Data.json");

			if (File.Exists(path))
			{
				var str = File.ReadAllText(Path.Combine(DocumentsPath, "Rhythms", "Data.json"));
				return JsonConvert.DeserializeObject<PeopleData>(str);
			}

			return new PeopleData(new EntryData[0]);
		}
	}
}
