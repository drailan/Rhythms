using GalaSoft.MvvmLight.Threading;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Rhythms.Shared;
using GalaSoft.MvvmLight.Messaging;

namespace Rhythms.Win
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		static MainWindow()
		{
			DispatcherHelper.Initialize();

			var module = new Module();
			module.Initialize();
		}

		protected override void OnClosed(EventArgs e)
		{
			ViewModel.ViewModelLocator.Cleanup();
			base.OnClosed(e);
		}
	}
}
