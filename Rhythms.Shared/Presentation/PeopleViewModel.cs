using GalaSoft.MvvmLight;
using Rhythms.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Rhythms.Win.ViewModel;

using Rhythms.Shared.Extensions;

using ParentedEntryItem = Presentation.ParentedItem<Rhythms.Shared.Entities.Entry, Rhythms.Shared.Presentation.PeopleViewModel>;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using GalaSoft.MvvmLight.Ioc;
using Rhythms.Shared.Interfaces;
using System.Threading.Tasks;
using Rhythms.Shared.Business;

namespace Rhythms.Shared.Presentation
{
	public class PeopleViewModel : ViewModelBase
	{
		private RangeEnabledObservableCollection<ParentedEntryItem> _people;
		private IDataProvider _dataProvider;

		private Entry _item;

		public RangeEnabledObservableCollection<ParentedEntryItem> People
		{
			get { return _people; }
			set { Set(() => People, ref _people, value); }
		}

		public PeopleViewModel()
		{
			People = new RangeEnabledObservableCollection<ParentedEntryItem>();

			_dataProvider = SimpleIoc.Default.GetInstance<IDataProvider>();

			Task.Run(async () =>
			{
				var peopleData = await _dataProvider.GetPeople();

				var people = peopleData.People
					.Select(p => p.ToEntry())
					.Select(e => this.CreateParented(e));

				People.InsertRange(people);
			});
		}

		public ICommand View => new RelayCommand<Entry>(entry =>
		{
			ViewModelLocator.Main.FirstDate = entry.FirstBirthDate;

			if (entry.SecondBirthDate != null && entry.SecondBirthDate != DateTime.MinValue)
			{
				ViewModelLocator.Main.SecondDate = entry.SecondBirthDate;
			}
			else
			{
				ViewModelLocator.Main.SecondDate = DateTime.MinValue;
			}
		});

		public ICommand Delete => new RelayCommand<Entry>(async entry =>
		{
			var person = People.Where(p => p.Item.Equals(entry)).FirstOrDefault();
			People.Remove(person);

			await SavePeople();
		});

		public ICommand Edit => new RelayCommand<Entry>(entry =>
		{
			var newEntry = new Entry(entry.FirstName, entry.SecondName, entry.FirstBirthDate, entry.SecondBirthDate);
			_item = newEntry;

			entry.IsEditing = true;
		});

		public ICommand Save => new RelayCommand<Entry>(async entry =>
		{
			var person = People
				.Where(p => p.Item.Equals(entry))
				.FirstOrDefault();

			person = this.CreateParented(entry);

			_item = null;

			entry.IsEditing = false;

			await SavePeople();
		});

		public ICommand Discard => new RelayCommand<Entry>(entry =>
		{
			var oldEntry = _item;

			entry.FirstBirthDate = oldEntry.FirstBirthDate;
			entry.SecondBirthDate = oldEntry.SecondBirthDate;
			entry.FirstName = oldEntry.FirstName;
			entry.SecondName = oldEntry.SecondName;

			entry.IsEditing = false;
		});

		private async Task SavePeople()
		{
			var peopleData = People.Select(p => p.Item.ToEntryData()).ToArray();

			await _dataProvider.SavePeople(peopleData);
		}
	}
}
