using System;
using System.Collections.Generic;
using UnityEngine;

public static class CollectionExtensions {

	/// <summary>
	/// Adds a given list of items (given as parameters) to a collection
	/// </summary>
	/// <example><code>
	/// List<int> list = new List<int>();
	/// list.AddRange(3, 4, 6, 10);
	/// </code></example>
	/// <param name="collection">The collection to add elements to</param>
	/// <param name="values">The items/values to add to the collection</param>
	public static void AddRange<T, S>(this ICollection<T> collection, params S[] values) where S : T {
		foreach (S value in values) {
			collection.Add(value);
		}
	}
}
