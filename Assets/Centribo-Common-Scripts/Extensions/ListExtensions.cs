using System;
using System.Collections.Generic;

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
	public static T RandomItem<T>(this IList<T> list) {
		return list[UnityEngine.Random.Range(0, list.Count)];
	}
}
