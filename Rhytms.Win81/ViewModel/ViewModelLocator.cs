using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Rhythms.Shared.Presentation;

namespace Rhythms.Win.ViewModel
{
	public class ViewModelLocator
	{
		public ViewModelLocator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			SimpleIoc.Default.Register<MainWindowViewModel>();
			SimpleIoc.Default.Register<PeopleViewModel>();
		}

		public static MainWindowViewModel Main => ServiceLocator.Current.GetInstance<MainWindowViewModel>();

		public static PeopleViewModel People => ServiceLocator.Current.GetInstance<PeopleViewModel>();

		public static void Cleanup()
		{
			ServiceLocator.Current.GetInstance<MainWindowViewModel>().Cleanup();
			ServiceLocator.Current.GetInstance<PeopleViewModel>().Cleanup();
		}
	}
}