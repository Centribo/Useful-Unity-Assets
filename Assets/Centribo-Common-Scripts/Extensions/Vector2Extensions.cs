using UnityEngine;

namespace Centribo.Common {
	public static class Vector2Extensions {

		/// <summary>
		/// Returns a random value between this vector's x and y value (inclusive).
		/// Uses UnityEngine.Random.Range for randomness.
		/// </summary>
		public static float RandomValue(this Vector2 vector) {
			return Random.Range(vector.x, vector.y);
		}

		/// <summary>
		/// Converts this vector to a <see cref="Centribo.Common.CompassDirection"/>
		/// </summary>
		public static CompassDirection ToCompassDirection(this Vector2 direction) {
			return MathExtensions.VectorToCompassDirection(direction);
		}

		/// <summary>
		/// Converts this vector to a <see cref="Centribo.Common.CardinalCompassDirection"/>
		/// </summary>
		public static CardinalCompassDirection ToCardinalCompassDirection(this Vector2 direction) {
			return MathExtensions.VectorToCardinalCompassDirection(direction);
		}
	}
}
