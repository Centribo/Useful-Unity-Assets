using System;
using System.Collections.Generic;
using UnityEngine;

namespace Centribo.Common {
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
	}
}