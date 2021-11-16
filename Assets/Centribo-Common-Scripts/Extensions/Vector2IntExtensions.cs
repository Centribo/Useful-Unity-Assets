using UnityEngine;

namespace Centribo.Common {
	public static class Vector2IntExtensions {
		public static int RandomValue(this Vector2Int vector) {
			return (int) Random.Range(vector.x, vector.y);
		}
	}
}
