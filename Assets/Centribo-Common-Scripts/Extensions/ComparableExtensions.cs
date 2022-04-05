using System;
using System.Collections.Generic;

namespace Centribo.Common {
	public static class ComparableExtensions {
		/// <summary>
		/// Checks whether a given item is strictly between the range (<paramref name="lower"/>, <paramref name="upper"/>)
		/// i.e., the ranges are <b>exclusive</b>.
		/// </summary>
		/// <example>
		/// <code>
		/// if(3.IsBetween(1, 5)){ ... }
		/// </code>
		/// </example>
		public static bool IsBetween<T>(this T value, T lower, T upper) where T : IComparable<T> {
			return value.CompareTo(lower) > 0 && value.CompareTo(upper) < 0;
		}

		/// <summary>
		/// Checks whether a given item is strictly within the range [<paramref name="lower"/>, <paramref name="upper"/>]
		/// i.e., the ranges are <b>inclusive</b>.
		/// </summary>
		/// <example>
		/// <code>
		/// if(3.IsWithin(1, 5)){ ... }
		/// </code>
		/// </example>
		public static bool IsWithin<T>(this T value, T lower, T upper) where T : IComparable<T> {
			return value.CompareTo(lower) >= 0 && value.CompareTo(upper) <= 0;
		}
	}
}