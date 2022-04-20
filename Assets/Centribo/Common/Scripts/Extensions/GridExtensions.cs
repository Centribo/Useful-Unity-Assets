using UnityEngine;
using UnityEngine.Tilemaps;

namespace Centribo.Common.Extensions {
	public static class GridExtensions {
		/// <summary>
		/// Get the cell position giving a base position and a facing direction
		/// </summary>
		public static Vector3Int GetFacingCellPosition(this Grid grid, Vector3 basePosition, Vector3 direction, float range = 1.0f) {
			return grid.WorldToCell(basePosition + direction * range);
		}

		/// <summary>
		/// Get the cell position giving a base position and a facing direction
		/// </summary>
		public static Vector3Int GetFacingCellPosition(this Grid grid, Vector3 basePosition, CompassDirection direction, float range = 1.0f) {
			return grid.GetFacingCellPosition(basePosition, direction.ToVector(), range);
		}
	}
}