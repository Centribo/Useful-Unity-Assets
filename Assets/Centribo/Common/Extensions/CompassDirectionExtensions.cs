using System.Collections.Generic;
using UnityEngine;

namespace Centribo.Common.Extensions {
	public static class CompassDirectionExtensions {
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

		public static CompassDirection VectorToCompassDirection(Vector2 input) {
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

		public static CardinalCompassDirection VectorToCardinalCompassDirection(Vector2 input) {
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

		public static Vector2 ToVector(this CompassDirection direction, float range = 1.0f) {
			Vector2 dir = Vector2.zero;
			switch (direction) {
				case CompassDirection.North: dir = Vector2.up; break;
				case CompassDirection.Northeast: dir = Vector2.up + Vector2.right; break;
				case CompassDirection.East: dir = Vector2.right; break;
				case CompassDirection.Southeast: dir = Vector2.down + Vector2.right; break;
				case CompassDirection.South: dir = Vector2.down; break;
				case CompassDirection.Southwest: dir = Vector2.down + Vector2.left; break;
				case CompassDirection.West: dir = Vector2.left; break;
				case CompassDirection.Northwest: dir = Vector2.up + Vector2.left; break;
			}

			dir = dir.normalized;
			dir *= range;

			return dir;
		}

		/// <summary>
		/// Returns a int repsenting a bitmask for adjacency:
		/// Here is the full table:
		/// - 0000 = 0 = blank
		/// - 0001 = 1 = W
		/// - 0010 = 2 = S
		/// - 0011 = 3 = SW
		/// - 0100 = 4 = E
		/// - 0101 = 5 = EW
		/// - 0110 = 6 = ES
		/// - 0111 = 7 = ESW
		/// - 1000 = 8 = N
		/// - 1001 = 9 = NW
		/// - 1010 = 10 = NS
		/// - 1011 = 11 = NSW
		/// - 1100 = 12 = NE
		/// - 1101 = 13 = NEW
		/// - 1110 = 14 = NES
		/// - 1111 = 15 = NESW
		/// For example if a object is adjacent to other objects to the north and east, we represent that as 1100.
		/// </summary>
		public static int ToBitMask(this CardinalCompassDirection direction) {
			switch (direction) {
				case CardinalCompassDirection.North: return 0b1000;
				case CardinalCompassDirection.East: return 0b0100;
				case CardinalCompassDirection.South: return 0b0010;
				case CardinalCompassDirection.West: default: return 0b0001;
			}
		}

		/// <summary>
		/// Returns a bit mask representing multiple cardinal directions.
		/// (Bitwise or of the result of <see cref="ToBitMask"/>)
		/// </summary>
		public static int ToBitMask(params CardinalCompassDirection[] directions) {
			int result = 0;

			foreach (CardinalCompassDirection direction in directions) {
				result |= direction.ToBitMask();
			}

			return result;
		}
	}
}
