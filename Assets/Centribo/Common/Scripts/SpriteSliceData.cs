using System;
using NaughtyAttributes;
using UnityEngine;

namespace Centribo.Common {
	[System.Serializable]
	public class SpriteSliceData {
		[ShowAssetPreview] public Sprite OriginalSprite;
		public Rect SpriteRect;
		public Vector2 NormalizedPivot;
		public float PixelsPerUnit;
		public Vector4 Border;
		public SpriteMeshType MeshType;

		public SpriteSliceData(Sprite sprite) {
			OriginalSprite = sprite;
			if (sprite == null) return;

			SpriteRect = sprite.rect;
			Vector2 localizedPivot = sprite.pivot + SpriteRect.position;
			NormalizedPivot = Rect.PointToNormalized(SpriteRect, localizedPivot);
			// Pivot = sprite.pivot;
			PixelsPerUnit = sprite.pixelsPerUnit;
			Border = sprite.border;
			MeshType = sprite.packingMode == SpritePackingMode.Tight ? SpriteMeshType.Tight : SpriteMeshType.FullRect;
		}

		public SpriteSliceData(Sprite sprite, Rect rect, Vector2 normalizedPivot, float pixelsPerUnit) {
			OriginalSprite = sprite;
			SpriteRect = rect;
			NormalizedPivot = normalizedPivot;
			PixelsPerUnit = pixelsPerUnit;
			Border = Vector4.zero;
			MeshType = SpriteMeshType.Tight;
		}

		public SpriteSliceData(Sprite sprite, Rect rect, Vector2 normalizedPivot, float pixelsPerUnit, Vector4 border, SpriteMeshType meshType) : this(sprite, rect, normalizedPivot, pixelsPerUnit) {
			Border = border;
			MeshType = meshType;
		}

		/// <summary>
		/// Try to use this slice data to slice a texture into a sprite.
		/// Will return null if unsuccessful.
		/// </summary>
		public Sprite TrySlice(Texture2D texture) {
			if (texture == null) return null;

			try {
				Sprite sprite = Sprite.Create(texture, SpriteRect, NormalizedPivot, PixelsPerUnit, 0, MeshType, Border);
				return sprite;
			} catch (Exception e) {
				Debug.LogException(e);
				return null;
			}
		}
	}
}