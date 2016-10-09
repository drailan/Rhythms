using System;
using System.Collections.Generic;
using System.Text;

namespace Rhythms.Shared.Extensions
{
	public static class StringExtensions
	{
		public static bool IsNullOrEmpty(this string source)
		{
			return string.IsNullOrEmpty(source);
		}
	}
}
