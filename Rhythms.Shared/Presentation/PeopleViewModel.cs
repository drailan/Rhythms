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
			var p2 = new Entry("D1", new DateTime(1986, 6, 10));
			var p3 = new Entry("D1", "D2", new DateTime(1986, 6, 10), new DateTime(1988, 9, 22));

			People = new ObservableCollection<ParentedEntryItem>();

			People.Add(this.CreateParented(p1));
			People.Add(this.CreateParented(p2));
			People.Add(this.CreateParented(p3));
		}

		public ICommand View => new RelayCommand<Entry>((entry) =>
		{
			ViewModelLocator.Main.FirstDate = entry.FirstBirthDate;
		});
	}
}
