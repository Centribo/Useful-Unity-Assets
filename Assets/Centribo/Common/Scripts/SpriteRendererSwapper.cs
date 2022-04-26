using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Centribo.Common.Extensions;
using Centribo.Common.Editor;
using NaughtyAttributes;

namespace Centribo.Common {
	/// <summary>
	/// Controller used to swap the sprite on a sprite renderer at runtime.
	/// Often used with a texture and <see cref="Centribo.Common.SpriteSliceDataContainer"/>.
	/// </summary>
	[RequireComponent(typeof(SpriteRenderer))]
	public class SpriteRendererSwapper : MonoBehaviour {
		public bool ShouldSwapSprites;

		private SpriteRenderer spriteRenderer;
		private Dictionary<Sprite, Sprite> spriteMapping;

		void Awake() {
			spriteRenderer = GetComponent<SpriteRenderer>();
		}

		void LateUpdate() {
			if (!ShouldSwapSprites) return;
			if (spriteMapping == null) return;

			if (spriteMapping.ContainsKey(spriteRenderer.sprite)) {
				spriteRenderer.sprite = spriteMapping[spriteRenderer.sprite];
			}
		}

		public void SetMapping(Dictionary<Sprite, Sprite> mapping) {
			spriteMapping = mapping;
		}

		public void SetMapping(List<SpriteSliceData> sliceData, Texture2D texture) {
			SetMapping(sliceData.GenerateSpriteSwapLookup(texture));
		}

		public void SetMapping(SpriteSliceDataContainer sliceData, Texture2D texture) {
			SetMapping(sliceData.SliceData, texture);
		}
	}
}