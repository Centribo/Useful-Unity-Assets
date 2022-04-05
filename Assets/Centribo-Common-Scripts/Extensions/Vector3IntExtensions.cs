using UnityEngine;

namespace Centribo.Common {
	public static class Vector3IntExtensions {
		/// <summary>
		/// Return the Manhattan distance between two points.
		/// <see href="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
		/// </summary>
		public static int ManhattanDistance(this Vector3Int a, Vector3Int b) {
			return
				Mathf.Abs(a.x - b.x) +
				Mathf.Abs(a.y - b.y) +
				Mathf.Abs(a.z - b.z);
		}

		/// <summary>
		/// Converts a Vector3Int to a Vector2, by dropping the z position.
		/// </summary>
		public static Vector2 ToVector2(this Vector3Int vector){
			return new Vector2(vector.x, vector.y);
		}
	}
}
