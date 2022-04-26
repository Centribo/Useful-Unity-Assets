using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Centribo.Common.Examples {
	public class RealtimeSpriteSwappingExampleRunner : MonoBehaviour {
		[SerializeField] SpriteRendererSwapper swapper;
		[SerializeField] SpriteSliceDataContainer sliceData;
		[SerializeField] List<Texture2D> spriteSheets;
		[SerializeField] TextMeshProUGUI toggleButtonTextMesh;

		void Start(){
			foreach(Texture2D texture in spriteSheets){
				Debug.Log(texture.isReadable);
			}
		}

		public void OnToggleSwapButtonClick() {
			swapper.ShouldSwapSprites = !swapper.ShouldSwapSprites;
		}

		public void OnCharacterButtonClick(int index) {
			Texture2D texture = spriteSheets[index];
			swapper.SetMapping(sliceData, texture);
		}

		void Update() {
			toggleButtonTextMesh.text = swapper.ShouldSwapSprites ? "Disable sprite swapping" : "Enable sprite swapping";
		}
	}
}