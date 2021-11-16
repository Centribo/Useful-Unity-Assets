using UnityEngine;

namespace Centribo.Common {
	public static class Vector2Extensions {
		public static float RandomValue(this Vector2 vector) {
			return Random.Range(vector.x, vector.y);
		}
	}
}
