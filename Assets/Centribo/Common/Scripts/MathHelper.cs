using UnityEngine;

namespace Centribo.Common {
	public static class MathHelper {
		/// <summary>
		/// Maps a given value in an input range to a output value in a given output range. The value will be clamped.
		/// </summary>
		/// <param name="value">The value in the input range [<paramref name="inputMin"/>, <paramref name="inputMax"/>]. This value will be clamped into the range.</param>
		/// <param name="inputMin">The minimum value of the input range</param>
		/// <param name="inputMax">The maximum value of the input range</param>
		/// <param name="outputMin">The minimum value of the output range</param>
		/// <param name="outputMax">the maximum value of the output range</param>
		/// <returns>The linearly mapped value in the range [<paramref name="outputMin"/>, <paramref name="outputMax"/>]</returns>
		public static float LinearMap(float value, float inputMin, float inputMax, float outputMin, float outputMax) {
			float output = Mathf.Clamp(value, inputMin, inputMax);

			float inputRange = inputMax - inputMin;
			float outputRange = outputMax - outputMin;

			output = ((output - inputMin) * (outputRange / inputRange)) + outputMin;
			return output;
		}

		/// <summary>
		/// Returns the modulo of <paramref name="x"/> when dividing by <paramref name="y"/>
		/// </summary>
		public static int Mod(int x, int y) {
			return (x % y + y) % y;
		}
	}
}