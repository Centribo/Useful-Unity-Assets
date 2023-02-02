using System;
using System.Collections.Generic;
using UnityEngine;

namespace Centribo.Common.Extensions {
	public static class ListExtensions {
		/// <summary>
		/// Resizes a list to a specific size
		/// </summary>
		/// <param name="list">List to be resized</param>
		/// <param name="newSize">The new size of the list. If invalid value is given, list is not changed</param>
		/// <param name="defaultValue">The value to be put in the list, if the list will be expanded. Note: if using a ref type (class),
		/// all new elements will point to the same reference!</param>
		public static void Resize<T>(this List<T> list, int newSize, T defaultValue = default(T)) {
			if (newSize < 0) return;
			if (newSize == list.Count) return;

			int size = list.Count;
			if (newSize < size) {
				list.RemoveRange(newSize, size - newSize);
			} else if (newSize > size) {
				if (newSize > list.Capacity) {
					// This bit is purely an optimisation, to avoid multiple automatic capacity changes.
					list.Capacity = newSize;
				}
				list.AddRange(System.Linq.Enumerable.Repeat(defaultValue, newSize - size));
			}
		}
	}
}