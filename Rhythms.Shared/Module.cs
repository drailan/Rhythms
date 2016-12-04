using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using Rhythms.Shared.Data;
using Rhythms.Shared.Interfaces;

namespace Rhythms.Shared
{
	public class Module
	{
		public void Initialize()
		{
			SimpleIoc.Default.Register<IDataProvider>(() => new DataProvider());
		}
	}
}
