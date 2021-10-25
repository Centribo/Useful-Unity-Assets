using System;
using System.Collections.Generic;

namespace Centribo.Common {
	public static class StringExtensions {
		/// <summary>
		/// Shorthand for string.Format()
		/// </summary>
		/// <example>
		/// <code>
		/// int x, y;
		/// string coords = "({0}, {1})".Format(x, y);
		/// </code>
		/// </example>
		/// <param name="s">The string to be formatted</param>
		/// <param name="args">The arguments to be passed into string.Format()</param>
		/// <returns>The formatted string</returns>
		public static string Format(this string s, params object[] args) {
			return string.Format(s, args);
		}
	}
}