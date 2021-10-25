using UnityEngine;
using UnityEngine.Tilemaps;

namespace Centribo.Common {
	public static class TilemapExtensions {
		/// <summary>
		/// Get the cell position giving a base position and a facing direction
		/// </summary>
		public static Vector3Int GetFacingCellPosition(this Tilemap tilemap, Vector3 basePosition, Vector3 direction, float range = 1.0f) {
			return tilemap.WorldToCell(basePosition + direction * range);
		}

		/// <summary>
		/// Get the cell position giving a base position and a facing direction
		/// </summary>
		public static Vector3Int GetFacingCellPosition(this Tilemap tilemap, Vector3 basePosition, CompassDirection direction, float range = 1.0f) {
			return tilemap.GetFacingCellPosition(basePosition, direction.ToVector(), range);
		}
	}
}