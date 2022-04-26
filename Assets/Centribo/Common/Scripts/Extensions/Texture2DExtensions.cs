using UnityEngine;

namespace Centribo.Common.Extensions {
	public static class Texture2DExtensions {
		/// <summary>
		/// Try to slice this texture using given slice data.
		/// Will return null if unsuccessful.
		/// See: <see cref="Centribo.Common.SpriteSliceData"/> for more information.
		/// </summary>
		public static Sprite TrySlice(this Texture2D texture, SpriteSliceData slicingData) {
			if (slicingData == null) return null;

			return slicingData.TrySlice(texture);
		}
	}
}