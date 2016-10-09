using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;

namespace Rhythms.Shared.Extensions
{
	public static class DisposableExtensions
	{
		public static T DisposeWith<T>(this T disposable, CompositeDisposable composite)
		{
			if (composite != null && disposable != null)
			{
				composite.Add(disposable as IDisposable);
			}

			return disposable;
		}
	}
}
