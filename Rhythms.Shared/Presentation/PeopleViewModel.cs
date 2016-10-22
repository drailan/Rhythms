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

namespace Rhythms.Shared.Presentation
{
	public class PeopleViewModel : ViewModelBase
	{
		private ObservableCollection<ParentedEntryItem> _people;

		public ObservableCollection<ParentedEntryItem> People
		{
			get { return _people; }
			set { Set(() => People, ref _people, value); }
		}

		public PeopleViewModel()
		{
			var p1 = new Entry("D2", new DateTime(1988, 9, 22));
			var p2 = new Entry("D1", new DateTime(1987, 6, 10));
			var p3 = new Entry("D1", "D2", new DateTime(1987, 6, 10), new DateTime(1988, 9, 22));

			People = new ObservableCollection<ParentedEntryItem>();

			People.Add(this.CreateParented(p1));
			People.Add(this.CreateParented(p2));
			People.Add(this.CreateParented(p3));
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

		public ICommand Delete => new RelayCommand<Entry>(entry =>
		{
			var person = People.Where(p => p.Item.Equals(entry)).FirstOrDefault();
			People.Remove(person);
		});

		public ICommand Edit => new RelayCommand<Entry>(entry =>
		{

		});
	}
}
