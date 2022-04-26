using System.Collections.Generic;
using UnityEngine;

namespace Centribo.Common.Extensions {
	public static class SpriteSliceDataExtensions {

		/// <summary>
		/// Generates a list of <see cref="Centribo.Common.SpriteSliceData"/> with the slice data for each
		/// sprite contained in this list.
		/// </summary>
		public static List<SpriteSliceData> GenerateSliceData(this List<Sprite> sprites) {
			List<SpriteSliceData> sliceData = new List<SpriteSliceData>();

			foreach (Sprite sprite in sprites) {
				if (sprite == null) continue;
				sliceData.Add(new SpriteSliceData(sprite));
			}

			return sliceData;
		}

		/// <summary>
		/// Generates a mapping from original sprites to sliced sprites from a given texture.
		/// Will return null if fatal error occurs, invalid slicings will be skipped.
		/// </summary>
		public static Dictionary<Sprite, Sprite> GenerateSpriteSwapLookup(this List<SpriteSliceData> sliceData, Texture2D texture) {
			if (sliceData == null) return null;
			if (texture == null) return null;

			Dictionary<Sprite, Sprite> lookup = new Dictionary<Sprite, Sprite>();
			foreach (SpriteSliceData slice in sliceData) {
				Sprite slicedSprite = slice.TrySlice(texture);
				if (slicedSprite == null) { Debug.LogWarning($"Unable to slice sprite: {slice.OriginalSprite} on {texture}"); continue; }
				lookup.Add(slice.OriginalSprite, slicedSprite);
			}

			return lookup;
		}
	}
}