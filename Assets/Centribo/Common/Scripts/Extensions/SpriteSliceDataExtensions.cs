using System.Collections.Generic;
using UnityEngine;

namespace Centribo.Common.Extensions {
	public static class SpriteSliceDataExtensions {

		/// <summary>
		/// Generates a list of <see cref="Centribo.Common.SpriteSliceData"/> with the slice data for each
		/// sprite contained in this list.
		/// </summary>
		public static List<SpriteSliceData> GenerateSpliceData(this List<Sprite> sprites) {
			List<SpriteSliceData> sliceData = new List<SpriteSliceData>();

			foreach (Sprite sprite in sprites) {
				if (sprite == null) continue;
				sliceData.Add(new SpriteSliceData(sprite));
			}

			return sliceData;
		}

		public static Dictionary<Sprite, Sprite> GenerateSpriteSwapLookup(this List<SpriteSliceData> sliceData, Texture2D texture) {
			if (sliceData == null) return null;
			if (texture == null) return null;

			return null;
		}
	}
}