using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using Rhythms.Shared.Business;
using Rhythms.Shared.Business.Graphs;
using Rhythms.Shared.Extensions;
using Rhythms.Shared.Entities;
using Rhythms.Shared.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace Rhythms.Shared.Presentation
{
	public class MainWindowViewModel : ViewModelBase, IDisposable
	{
		private DateTime _firstDate;
		private DateTime _secondDate;
		private DateTime _selectedDate;

		private Guid _scrollIntoViewTrigger;
		private Guid _saveChartTrigger;

		private int _scale;
		private int _auxScale = 360;
		private bool _isMain;

		private bool _isBusy;

		private int _graphYMax, _graphYMin;
		private DateTime _graphXMax, _graphXMin;

		private ReplaySubject<DateTime> _firstDateSubject, _secondDateSubject;
		private ReplaySubject<int> _scaleSubject;
		private ReplaySubject<bool> _isMainSubject;

		private CompositeDisposable _disposable;

		private RangeEnabledObservableCollection<Point> _todayLine;

		private RangeEnabledObservableCollection<Point> _graph24;
		private RangeEnabledObservableCollection<Point> _graph28;
		private RangeEnabledObservableCollection<Point> _graph33;
		private RangeEnabledObservableCollection<Point> _graph40;
		private RangeEnabledObservableCollection<Point> _graph56;
		private RangeEnabledObservableCollection<Point> _graph92;
		private RangeEnabledObservableCollection<Point> _graph276;

		private RangeEnabledObservableCollection<Point> _graph24_2;
		private RangeEnabledObservableCollection<Point> _graph28_2;
		private RangeEnabledObservableCollection<Point> _graph33_2;
		private RangeEnabledObservableCollection<Point> _graph40_2;
		//private RangeEnabledObservableCollection<Point> _graph56_2;
		//private RangeEnabledObservableCollection<Point> _graph92_2;
		//private RangeEnabledObservableCollection<Point> _graph276_2;

		private DoubleCollection _strokeCollection;

		public DoubleCollection StrokeCollection
		{
			get { return _strokeCollection; }
			set { Set(() => StrokeCollection, ref _strokeCollection, value); }
		}

		private RangeEnabledObservableCollection<GraphState> _graphStates;

		public Guid ScrollIntoViewTrigger
		{
			get { return _scrollIntoViewTrigger; }
			set { Set(() => ScrollIntoViewTrigger, ref _scrollIntoViewTrigger, value); }
		}

		public Guid SaveChartTrigger
		{
			get { return _saveChartTrigger; }
			set { Set(() => SaveChartTrigger, ref _saveChartTrigger, value); }
		}

		public bool IsBusy
		{
			get { return _isBusy; }
			set { Set(() => IsBusy, ref _isBusy, value); }
		}

		public RangeEnabledObservableCollection<GraphState> GraphStates
		{
			get { return _graphStates; }
			set { Set(() => GraphStates, ref _graphStates, value); }
		}

		public int GraphYMax
		{
			get { return _graphYMax; }
			set { Set(() => GraphYMax, ref _graphYMax, value); }
		}

		public DateTime GraphXMin
		{
			get { return _graphXMin; }
			set { Set(() => GraphXMin, ref _graphXMin, value); }
		}

		public DateTime GraphXMax
		{
			get { return _graphXMax; }
			set { Set(() => GraphXMax, ref _graphXMax, value); }
		}

		public int GraphYMin
		{
			get { return _graphYMin; }
			set { Set(() => GraphYMin, ref _graphYMin, value); }
		}

		#region graphs

		public RangeEnabledObservableCollection<Point> Graph24
		{
			get { return _graph24; }
			set { Set(() => Graph24, ref _graph24, value); }
		}

		public RangeEnabledObservableCollection<Point> Graph24_2
		{
			get { return _graph24_2; }
			set { Set(() => Graph24_2, ref _graph24_2, value); }
		}

		public RangeEnabledObservableCollection<Point> Graph28
		{
			get { return _graph28; }
			set { Set(() => Graph28, ref _graph28, value); }
		}

		public RangeEnabledObservableCollection<Point> Graph28_2
		{
			get { return _graph28_2; }
			set { Set(() => Graph28_2, ref _graph28_2, value); }
		}

		public RangeEnabledObservableCollection<Point> Graph33
		{
			get { return _graph33; }
			set { Set(() => Graph33, ref _graph33, value); }
		}

		public RangeEnabledObservableCollection<Point> Graph33_2
		{
			get { return _graph33_2; }
			set { Set(() => Graph33_2, ref _graph33_2, value); }
		}

		public RangeEnabledObservableCollection<Point> Graph40
		{
			get { return _graph40; }
			set { Set(() => Graph40, ref _graph40, value); }
		}

		public RangeEnabledObservableCollection<Point> Graph40_2
		{
			get { return _graph40_2; }
			set { Set(() => Graph40_2, ref _graph40_2, value); }
		}

		public RangeEnabledObservableCollection<Point> Graph56
		{
			get { return _graph56; }
			set { Set(() => Graph56, ref _graph56, value); }
		}

		public RangeEnabledObservableCollection<Point> Graph92
		{
			get { return _graph92; }
			set { Set(() => Graph92, ref _graph92, value); }
		}

		public RangeEnabledObservableCollection<Point> Graph276
		{
			get { return _graph276; }
			set { Set(() => Graph276, ref _graph276, value); }
		}

		#endregion

		public RangeEnabledObservableCollection<Point> TodayLine
		{
			get { return _todayLine; }
			set { Set(() => TodayLine, ref _todayLine, value); }
		}

		public DateTime FirstDate
		{
			get { return _firstDate; }
			set
			{
				if (_firstDate != value)
				{
					Set(() => FirstDate, ref _firstDate, value);
					_firstDateSubject?.OnNext(value);
				}
			}
		}

		public DateTime SecondDate
		{
			get { return _secondDate; }
			set
			{
				if (_secondDate != value)
				{
					Set(() => SecondDate, ref _secondDate, value);
					_secondDateSubject?.OnNext(value);
				}
			}
		}

		public DateTime SelectedDate
		{
			get { return _selectedDate; }
			set
			{
				Set(() => SelectedDate, ref _selectedDate, value);
			}
		}

		public int Scale
		{
			get { return _scale; }
			set
			{
				if (_scale != value)
				{
					Set(() => Scale, ref _scale, value);
					_scaleSubject?.OnNext(value);
				}
			}
		}

		public bool IsMain
		{
			get { return _isMain; }
			set
			{
				if (_isMain != value)
				{
					Set(() => IsMain, ref _isMain, value);
					_isMainSubject?.OnNext(value);
				}
			}
		}

		public MainWindowViewModel()
		{
			InitSubjects();

			ObserveScale(_scaleSubject);
			ObserveDates(_firstDateSubject, _secondDateSubject);
			ObserveIsMain(_isMainSubject);

			FirstDate = DateTime.Today;
			SecondDate = DateTime.Today;
			Scale = 30;

			InitMainBounds();

			IsMain = true;

			StrokeCollection = new DoubleCollection();
			StrokeCollection.Add(1);
			StrokeCollection.Add(1);

			_disposable = new CompositeDisposable();
		}

		private void InitMainBounds()
		{
			GraphYMax = 11;
			GraphYMin = -11;

			GraphXMax = DateTime.Today + new TimeSpan(Scale / 2, 0, 0, 0);
			GraphXMin = DateTime.Today - new TimeSpan(Scale / 2, 0, 0, 0);
		}

		private void GenerateTodayLine()
		{
			TodayLine = new RangeEnabledObservableCollection<Point>();
			TodayLine.Add(new Point(0, GraphYMin, DateTime.Today));
			TodayLine.Add(new Point(0, GraphYMax, DateTime.Today));
		}

		private void InitAuxBounds()
		{
			GraphYMax = 276 / 4 + 1;
			GraphYMin = -276 / 4 - 1;

			GraphXMax = DateTime.Today + new TimeSpan(_auxScale / 2, 0, 0, 0);
			GraphXMin = DateTime.Today - new TimeSpan(_auxScale / 2, 0, 0, 0);
		}

		private void InitSubjects()
		{
			_firstDateSubject = new ReplaySubject<DateTime>(1).DisposeWith(_disposable);
			_secondDateSubject = new ReplaySubject<DateTime>(1).DisposeWith(_disposable);
			_scaleSubject = new ReplaySubject<int>(1).DisposeWith(_disposable);
			_isMainSubject = new ReplaySubject<bool>(1).DisposeWith(_disposable);
		}

		public void ObserveScale(IObservable<int> scale)
		{
			scale.Skip(1).Do(_ => SpawnGraphs()).Subscribe().DisposeWith(_disposable);
		}

		public void ObserveDates(IObservable<DateTime> first, IObservable<DateTime> second)
		{
			first.Skip(1).Do(_ => SpawnGraphs()).Subscribe().DisposeWith(_disposable);
			second.Skip(1).Do(_ => SpawnGraphs()).Subscribe().DisposeWith(_disposable);
		}

		public void ObserveIsMain(IObservable<bool> main)
		{
			main.Skip(1).Do(_ => SpawnGraphs()).Subscribe().DisposeWith(_disposable);
		}

		public override void Cleanup()
		{
			base.Cleanup();

			_disposable.Dispose();
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}

		private void SpawnGraphs()
		{
			IsBusy = true;

			Task.Run(async () =>
			{
				await Task.Delay(TimeSpan.FromSeconds(1));

				var t = Task.Delay(TimeSpan.FromSeconds(1));
				var u = GenerateGraphs(FirstDate, SecondDate);

				await Task.WhenAll(t, u);
			}).ContinueWith(_ => IsBusy = false, TaskScheduler.FromCurrentSynchronizationContext());
		}

		private IEnumerable<IGraph> InnerGenerateGraphs(DateTime birthday)
		{
			var totalDays = DateTime.Today - birthday;

			var g24 = new Graph24();
			g24.GenerateGraph(totalDays.Days, Scale);

			var g28 = new Graph28();
			g28.GenerateGraph(totalDays.Days, Scale);

			var g33 = new Graph33();
			g33.GenerateGraph(totalDays.Days, Scale);

			var g40 = new Graph40();
			g40.GenerateGraph(totalDays.Days, Scale);

			return new List<IGraph>() { g24, g28, g33, g40 };
		}

		private async Task GenerateGraphs(DateTime birthday, DateTime secondbirthday)
		{
			var firstTotalDays = DateTime.Today - birthday;
			var secondTotalDays = DateTime.Today - secondbirthday;

			if (IsMain)
			{
				InitMainBounds();

				var graphs = InnerGenerateGraphs(birthday);

				var statesRange = new ConcurrentDictionary<DateTime, GraphState>();

				Parallel.For(-Scale / 2, Scale / 2, (i) =>
				{
					var firstStates = graphs.GetGraphStates(firstTotalDays.Days, i);

					var date = DateTime.Now.AddDays(i);

					if (secondbirthday != DateTime.MinValue && secondbirthday != DateTime.Today)
					{
						var secondStates = graphs.GetGraphStates(secondTotalDays.Days, i);
						statesRange[date] = new GraphState(date, firstStates, secondStates);
					}
					else
					{
						statesRange[date] = new GraphState(date, firstStates);
					}
				});

				var statesList = statesRange
					.OrderBy(x => x.Key)
					.Select(x => x.Value);

				var t = DispatcherHelper.RunAsync(() =>
				{
					GraphStates = new RangeEnabledObservableCollection<GraphState>();
					GraphStates.InsertRange(statesList);
				});

				if (birthday != DateTime.Today)
				{
					Graph24 = new RangeEnabledObservableCollection<Point>(graphs.ElementAt(0).Points);
					Graph28 = new RangeEnabledObservableCollection<Point>(graphs.ElementAt(1).Points);
					Graph33 = new RangeEnabledObservableCollection<Point>(graphs.ElementAt(2).Points);
					Graph40 = new RangeEnabledObservableCollection<Point>(graphs.ElementAt(3).Points);
				}

				if (secondbirthday != DateTime.Today && secondbirthday != DateTime.MinValue)
				{
					graphs = InnerGenerateGraphs(secondbirthday);

					Graph24_2 = new RangeEnabledObservableCollection<Point>(graphs.ElementAt(0).Points);
					Graph28_2 = new RangeEnabledObservableCollection<Point>(graphs.ElementAt(1).Points);
					Graph33_2 = new RangeEnabledObservableCollection<Point>(graphs.ElementAt(2).Points);
					Graph40_2 = new RangeEnabledObservableCollection<Point>(graphs.ElementAt(3).Points);
				}
				else
				{
					Graph24_2 = null;
					Graph28_2 = null;
					Graph33_2 = null;
					Graph40_2 = null;
				}

				await t;
			}
			else
			{
				InitAuxBounds();

				var g56 = new Graph56();
				g56.GenerateGraph(firstTotalDays.Days, _auxScale);

				var g92 = new Graph92();
				g92.GenerateGraph(firstTotalDays.Days, _auxScale);

				var g276 = new Graph276();
				g276.GenerateGraph(firstTotalDays.Days, _auxScale);

				Graph56 = new RangeEnabledObservableCollection<Point>(g56.Points);
				Graph92 = new RangeEnabledObservableCollection<Point>(g92.Points);
				Graph276 = new RangeEnabledObservableCollection<Point>(g276.Points);
			}

			GenerateTodayLine();
			SelectedDate = DateTime.Today;
			ScrollIntoViewTrigger = Guid.NewGuid();
		}

		public ICommand Export => new RelayCommand(() =>
		{
			SaveChartTrigger = Guid.NewGuid();
		});
	}
}
