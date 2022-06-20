using System.Collections.Generic;
using UnityEngine;

namespace Centribo.Common.Extensions {
	public static class Vector3IntExtensions {
		/// <summary>
		/// Return the Manhattan distance between two points.
		/// <see href="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
		/// </summary>
		public static int ManhattanDistance(this Vector3Int a, Vector3Int b) {
			return
				Mathf.Abs(a.x - b.x) +
				Mathf.Abs(a.y - b.y) +
				Mathf.Abs(a.z - b.z);
		}

		/// <summary>
		/// Converts a Vector3Int to a Vector2, by dropping the z position.
		/// </summary>
		public static Vector2 ToVector2(this Vector3Int vector) {
			return new Vector2(vector.x, vector.y);
		}

		/// <summary>
		/// Returns an enumerator for positions that a cell overlaps with, given a size.
		/// The given cell acts as the bottom-left cell of a rectangle.
		/// </summary>
		public static IEnumerable<Vector3Int> OverlappingCells(this Vector3Int bottomLeftCell, Vector2Int size) {
			Vector3Int cell = Vector3Int.zero;

			for (int x = 0; x < size.x; x++) {
				for (int y = 0; y < size.y; y++) {
					cell.x = bottomLeftCell.x + x;
					cell.y = bottomLeftCell.y + y;
					cell.z = bottomLeftCell.z;
					yield return cell;
				}
			}
		}

		/// <summary>
		/// Returns a list of adjacent positions that are cardinally or ordinally adjacent in a 2D context.
		/// </summary>
		public static List<Vector3Int> GetCompassNeighbours(this Vector3Int origin) {
			List<Vector3Int> neighbours = new List<Vector3Int>(){
				origin + Vector3Int.up, // North
				origin + Vector3Int.up + Vector3Int.right, // Northeast
				origin + Vector3Int.right, // East
				origin + Vector3Int.down + Vector3Int.right, // Southeast
				origin + Vector3Int.down, // South
				origin + Vector3Int.down + Vector3Int.left, // Southwest
				origin + Vector3Int.left, // West
				origin + Vector3Int.up + Vector3Int.left // Northwest
			};

			return neighbours;
		}

		/// <summary>
		/// Returns a list of adjacent positions that are ordinally adjacent in a 2D context.
		/// </summary>
		public static List<Vector3Int> GetOrdinalNeighbours(this Vector3Int origin) {
			List<Vector3Int> neighbours = new List<Vector3Int>(){
				origin + Vector3Int.up + Vector3Int.right, // Northeast
				origin + Vector3Int.down + Vector3Int.right, // Southeast
				origin + Vector3Int.down + Vector3Int.left, // Southwest
				origin + Vector3Int.up + Vector3Int.left // Northwest
			};

			return neighbours;
		}

		/// <summary>
		/// Returns a list of adjacent positions that are cardinally adjacent in a 2D context.
		/// </summary>
		public static List<Vector3Int> GetCardinalNeighbours(this Vector3Int origin) {
			List<Vector3Int> neighbours = new List<Vector3Int>(){
				origin + Vector3Int.up, // North
				origin + Vector3Int.right, // East
				origin + Vector3Int.down, // South
				origin + Vector3Int.left, // West
			};

			return neighbours;
		}
	}
}