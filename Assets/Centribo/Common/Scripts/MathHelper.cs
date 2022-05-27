namespace Centribo.Common {
	public static class MathHelper {
		/// <summary>
		/// Maps a given <paramref name="inValue"/> from [<paramref name="inStart"/>, <paramref name="inEnd"/>] to [<paramref name="outStart"/>, <paramref name="outEnd"/>].
		/// There is an optional easing function parameter. See: <see cref="Centribo.Common.EasingFunction"/>.
		/// </summary>
		/// <param name="inValue">The input value</param>
		/// <param name="inStart">Inclusive input minimum</param>
		/// <param name="inEnd">Inclusive input maximum</param>
		/// <param name="outStart">Inclusive output minimum</param>
		/// <param name="outEnd">Inclusive output maximum</param>
		/// <param name="easingFunction">What easing function to use; Linear is used by default. See: <see cref="Centribo.Common.EasingFunction"/>.</param>
		public static float RangeMap(float inValue, float inStart, float inEnd, float outStart, float outEnd, EasingFunction.Ease easingFunction = EasingFunction.Ease.Linear) {
			EasingFunction.EasingDelegate easeDelegate = EasingFunction.GetEasingFunction(easingFunction);
			return RangeMap(inValue, inStart, inEnd, outStart, outEnd, easeDelegate);
		}

		/// <summary>
		/// Maps a given <paramref name="inValue"/> from [<paramref name="inStart"/>, <paramref name="inEnd"/>] to [<paramref name="outStart"/>, <paramref name="outEnd"/>].
		/// There is an optional easing function parameter. See: <see cref="Centribo.Common.EasingFunction"/>.
		/// </summary>
		/// <param name="inValue">The input value</param>
		/// <param name="inStart">Inclusive input minimum</param>
		/// <param name="inEnd">Inclusive input maximum</param>
		/// <param name="outStart">Inclusive output minimum</param>
		/// <param name="outEnd">Inclusive output maximum</param>
		/// <param name="easingFunction">What easing function delegate to use. See: <see cref="Centribo.Common.EasingFunction"/>.</param>
		public static float RangeMap(float inValue, float inStart, float inEnd, float outStart, float outEnd, EasingFunction.EasingDelegate easingFunction) {
			float output = inValue - inStart;
			output = output / (inEnd - inStart); // [0,1]

			if (easingFunction != null) {
				output = easingFunction(0, 1, output);
			}

			output = output * (outEnd - outStart); // [0, outRange]
			output = output + outStart; // [outStart, outEnd]
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