using UnityEngine;

namespace Centribo.Common {
	public static class Vector2IntExtensions {
		/// <summary>
		/// Produces a random value given a vector, where the x component represents the range minimum,
		/// and the y component represents the range maximum. This range is inclusive.
		/// </summary>
		public static int RandomValue(this Vector2Int vector) {
			return (int) Random.Range(vector.x, vector.y);
		}
	}
}
