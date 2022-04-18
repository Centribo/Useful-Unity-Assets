using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Centribo.Common.Extensions {
	/// <summary>
	/// Set of useful extension methods related to randomness that use UnityEngine.Random for randomization
	/// </summary>
	public static class UnityRandomExtensions {

		/// <summary>
		/// Gets a random item from a list of items and their weights. The given list must be in the form Tuple<int, T> where Item1 are the weights, and Item2 are the items.
		/// </summary>
		/// <example><code>
		/// List<Tuple<int, char>> samples = new List<Tuple<int, char>> {
		/// 		Tuple.Create(3, 'A'),
		/// 		Tuple.Create(2, 'B'),
		/// 		Tuple.Create(9, 'C')
		/// 	};
		/// char sample = samples.GetWeightedRandomElement();
		/// </code></example>
		/// <returns>Returns a random according to the distribution defined the given weights</returns>
		public static T GetWeightedRandomElement<T>(this IList<Tuple<int, T>> tuples) {
			int totalWeight = 0;
			foreach (Tuple<int, T> tuple in tuples) {
				totalWeight += tuple.Item1;
			}

			int r = UnityEngine.Random.Range(0, totalWeight);
			totalWeight = 0;
			foreach (Tuple<int, T> tuple in tuples) {
				totalWeight += tuple.Item1;
				if (r < totalWeight) {
					return tuple.Item2;
				}
			}

			return tuples[0].Item2;
		}
	}
}