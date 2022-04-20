using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Centribo.Common.Extensions {
	public static class EnumExtensions {
		/// <summary>
		/// Gets all items for an enum value.
		/// Taken from: https://stackoverflow.com/questions/105372/how-to-enumerate-an-enum
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static IEnumerable<T> GetAllItems<T>(this Enum value) {
			foreach (object item in Enum.GetValues(typeof(T))) {
				yield return (T) item;
			}
		}

		/// <summary>
		/// Gets all items for an enum type.
		/// Taken from: https://stackoverflow.com/questions/105372/how-to-enumerate-an-enum
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static IEnumerable<T> GetAllItems<T>() where T : struct {
			foreach (object item in Enum.GetValues(typeof(T))) {
				yield return (T) item;
			}
		}

		/// <summary>
		/// Gets all combined items from an enum value.
		/// Taken from: https://stackoverflow.com/questions/105372/how-to-enumerate-an-enum
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		/// <example>
		/// Displays ValueA and ValueB.
		/// <code>
		/// EnumExample dummy = EnumExample.Combi;
		/// foreach (var item in dummy.GetAllSelectedItems<EnumExample>())
		/// {
		///    Console.WriteLine(item);
		/// }
		/// </code>
		/// </example>
		public static IEnumerable<T> GetAllSelectedItems<T>(this Enum value) {
			int valueAsInt = Convert.ToInt32(value, CultureInfo.InvariantCulture);

			foreach (object item in Enum.GetValues(typeof(T))) {
				int itemAsInt = Convert.ToInt32(item, CultureInfo.InvariantCulture);

				if (itemAsInt == (valueAsInt & itemAsInt)) {
					yield return (T) item;
				}
			}
		}

		/// <summary>
		/// Determines whether the enum value contains a specific value.
		/// Taken from: https://stackoverflow.com/questions/105372/how-to-enumerate-an-enum
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="request">The request.</param>
		/// <returns>
		///     <c>true</c> if value contains the specified value; otherwise, <c>false</c>.
		/// </returns>
		/// <example>
		/// <code>
		/// EnumExample dummy = EnumExample.Combi;
		/// if (dummy.Contains<EnumExample>(EnumExample.ValueA))
		/// {
		///     Console.WriteLine("dummy contains EnumExample.ValueA");
		/// }
		/// </code>
		/// </example>
		public static bool Contains<T>(this Enum value, T request) {
			int valueAsInt = Convert.ToInt32(value, CultureInfo.InvariantCulture);
			int requestAsInt = Convert.ToInt32(request, CultureInfo.InvariantCulture);

			if (requestAsInt == (valueAsInt & requestAsInt)) {
				return true;
			}

			return false;
		}
	}
}