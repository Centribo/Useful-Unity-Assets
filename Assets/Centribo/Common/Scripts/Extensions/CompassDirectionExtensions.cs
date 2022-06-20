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
		/// Returns a byte representing a bitmask for adjacency.
		/// </summary>
		public static int ToBitMask(this CompassDirection direction) {
			switch (direction) {
				case CompassDirection.North: return 0b00000001;
				case CompassDirection.Northeast: return 0b00000010;
				case CompassDirection.East: return 0b00000100;
				case CompassDirection.Southeast: return 0b00001000;
				case CompassDirection.South: return 0b00010000;
				case CompassDirection.Southwest: return 0b00100000;
				case CompassDirection.West: return 0b01000000;
				case CompassDirection.Northwest: return 0b10000000;
			}

			return 0b00000000;
		}

		/// <summary>
		/// Returns a bit mask representing multiple cardinal directions.
		/// (Bitwise or of the result of <see cref="ToBitMask"/>)
		/// </summary>
		public static int ToBitMask(params CompassDirection[] directions) {
			int result = 0;

			foreach (CompassDirection direction in directions) {
				result |= direction.ToBitMask();
			}

			return result;
		}
	}
}
