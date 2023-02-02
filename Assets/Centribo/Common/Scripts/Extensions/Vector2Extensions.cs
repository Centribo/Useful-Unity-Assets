using UnityEngine;

namespace Centribo.Common.Extensions {
	public static class Vector2Extensions {

		/// <summary>
		/// Returns a random value between this vector's x and y value (inclusive).
		/// Uses UnityEngine.Random.Range for randomness.
		/// </summary>
		public static float RandomValue(this Vector2 vector) {
			return Random.Range(vector.x, vector.y);
		}

		/// <summary>
		/// Returns a inverse lerp of the x and y components of a given vector. The result is clamped to be [0, 1]
		/// </summary>
		public static float InverseLerp(this Vector2 vector, float value) {
			return Mathf.InverseLerp(vector.x, vector.y, value);
		}

		/// <summary>
		/// Returns a lerp between the x and y components of a given vector and t value.
		/// </summary>
		public static float Lerp(this Vector2 vector, float value) {
			return Mathf.Lerp(vector.x, vector.y, value);
		}

		/// <summary>
		/// Converts this vector to a <see cref="Centribo.Common.CompassDirection"/>
		/// </summary>
		public static CompassDirection ToCompassDirection(this Vector2 direction) {
			return CompassDirectionExtensions.VectorToCompassDirection(direction);
		}
	}
}
