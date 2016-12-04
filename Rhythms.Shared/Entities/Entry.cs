using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace Rhythms.Shared.Entities
{
	public class Entry : ObservableObject
	{
		private string _firstName;

		public string FirstName
		{
			get { return _firstName; }
			set { Set(() => FirstName, ref _firstName, value); }
		}

		public string _secondName;

		public string SecondName
		{
			get { return _secondName; }
			set { Set(() => SecondName, ref _secondName, value); }
		}

		private DateTime _firstBirthDate;
		public DateTime FirstBirthDate
		{
			get { return _firstBirthDate; }
			set { Set(() => FirstBirthDate, ref _firstBirthDate, value); }
		}

		private DateTime _secondBirthDate;
		public DateTime SecondBirthDate
		{
			get { return _secondBirthDate; }
			set { Set(() => SecondBirthDate, ref _secondBirthDate, value); }
		}

		private bool _isEditing;
		public bool IsEditing
		{
			get { return _isEditing; }
			set { Set(() => IsEditing, ref _isEditing, value); }
		}

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
