namespace Rhythms.Shared.Data
{
	public class PeopleData
	{
		public PeopleData()
		{
		}

		public PeopleData(EntryData[] people)
		{
			People = people;
		}

		public EntryData[] People { get; set; }
	}
}
