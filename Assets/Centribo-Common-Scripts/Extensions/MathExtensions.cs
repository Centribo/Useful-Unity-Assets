using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CompassDirection : int {
	North = 0,
	Northeast = 1,
	East = 2,
	Southeast = 3,
	South = 4,
	Southwest = 5,
	West = 6,
	Northwest = 7
}

public enum CardinalCompassDirection : int {
	North = 0,
	East = 2,
	South = 4,
	West = 6
}

public static class MathExtensions {
	/// <summary>
	/// Maps a given value in an input range to a output value in a given output range. The value will be clamped.
	/// </summary>
	/// <param name="value">The value in the input range [<see cref="inputMin"/>, <see cref"inputMax"/>]. This value will be clamped into the range.</param>
	/// <param name="inputMin">The minimum value of the input range</param>
	/// <param name="inputMax">The maximum value of the input range</param>
	/// <param name="outputMin">The minimum value of the output range</param>
	/// <param name="outputMax">the maximum value of the output range</param>
	/// <returns>The linearly mapped value in the range [<see cref="outputMin"/>, <see cref="outputMax"/>]</returns>
	public static float LinearMap(float value, float inputMin, float inputMax, float outputMin, float outputMax) {
		float output = Mathf.Clamp(value, inputMin, inputMax);

		float inputRange = inputMax - inputMin;
		float outputRange = outputMax - outputMin;

		output = ((output - inputMin) * (outputRange / inputRange)) + outputMin;
		return output;
	}

	/// <summary>
	/// Returns the modulo of x when dividing by y
	/// </summary>
	public static int Mod(int x, int y) {
		return (x % y + y) % y;
	}

	static Dictionary<int, CompassDirection> sectorToCompassDirectionLookup = new Dictionary<int, CompassDirection> {
		{0, CompassDirection.East},
		{1, CompassDirection.Northeast},
		{2, CompassDirection.North},
		{3, CompassDirection.Northwest},
		{4, CompassDirection.West},
		{-4, CompassDirection.West},
		{-3, CompassDirection.Southwest},
		{-2, CompassDirection.South},
		{-1, CompassDirection.Southeast}
	};

	public static CompassDirection DirectionVectorToCompassDirection(Vector2 input) {
		input = input.normalized;
		float inputAngle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
		float sectorSize = 360.0f / 8.0f;
		float halfSectorSize = sectorSize / 2.0f;
		float convertedAngle = inputAngle + halfSectorSize;
		int sector = Mathf.FloorToInt(convertedAngle / sectorSize);

		if (sectorToCompassDirectionLookup.ContainsKey(sector)) {
			return sectorToCompassDirectionLookup[sector];
		} else {
			return CompassDirection.East;
		}
	}

	static Dictionary<int, CardinalCompassDirection> sectorToCardinalCompassDirectionLookup = new Dictionary<int, CardinalCompassDirection> {
		{0, CardinalCompassDirection.East},
		{1, CardinalCompassDirection.North},
		{2, CardinalCompassDirection.West},
		{-2, CardinalCompassDirection.West},
		{-1, CardinalCompassDirection.South}
	};

	public static CardinalCompassDirection DirectionVectorToCardinalCompassDirection(Vector2 input) {
		input = input.normalized;
		float inputAngle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
		float sectorSize = 360.0f / 4.0f;
		float halfSectorSize = sectorSize / 2.0f;
		float convertedAngle = inputAngle + halfSectorSize;
		int sector = Mathf.FloorToInt(convertedAngle / sectorSize);

		if (sectorToCardinalCompassDirectionLookup.ContainsKey(sector)) {
			return sectorToCardinalCompassDirectionLookup[sector];
		} else {
			return CardinalCompassDirection.East;
		}
	}
}