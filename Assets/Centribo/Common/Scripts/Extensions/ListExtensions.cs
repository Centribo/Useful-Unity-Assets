using System;
using System.Collections.Generic;
using UnityEngine;

namespace Centribo.Common.Extensions {
	public static class ListExtentions {
		private static System.Random random = new System.Random();

		/// <summary>
		/// Shuffles a list using the Fisher-Yates shuffle algorithm and System.Random (which may not be the most secure/random)
		/// This method uses System.Random for randomization
		/// </summary>
		/// <param name="list">The list to be shuffled</param>
		public static void Shuffle<T>(this IList<T> list) {
			int i = list.Count;
			while (i > 1) {
				i--;
				int j = random.Next(i + 1);
				T value = list[j];
				list[j] = list[i];
				list[i] = value;
			}
		}

		/// <summary>
		/// Gets a random item from the list using a uniform distribution
		/// This method uses UnityEngine.Random for randomization
		/// </summary>
		/// <example><code>
		/// List<int> elements = new List<int> { 1, 2, 3, 4, 5 };
		/// int randomElement = elements.RandomItem();
		/// </code></example>
		/// <param name="list">The list to get a random element from</param>
		/// <returns>The random element</returns>
		public static T RandomElement<T>(this IList<T> list) {
			return list[UnityEngine.Random.Range(0, list.Count)];
		}

		/// <summary>
		/// Tries to access an element at a given index from a list. Returns null if unable to access.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public static T TryGetElement<T>(this List<T> list, int index) where T : class {
			if (list == null) return null;

			if (index < list.Count) {
				return list[index];
			}

			return null;
		}

		/// <summary>
		/// Generates a random queue of a given list of elements
		/// </summary>
		/// <param name="list">The list of elements to turn into a queue</param>
		/// <param name="length">The length of the queue to return. If -1 is given, will return a queue of all the elements.
		/// If a number is given that is greater than the list length, then will return a queue of length of the list</param>
		public static Queue<T> GetRandomQueue<T>(this List<T> list, int length = -1) {
			list.Shuffle();
			if (length == -1 || length >= list.Count) {
				return new Queue<T>(list);
			} else {
				Queue<T> queue = new Queue<T>();
				int i = 0;
				while (queue.Count < length) {
					queue.Enqueue(list[i]);
					i++;
				}

				return queue;
			}
		}

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