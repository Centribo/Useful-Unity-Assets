using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using NaughtyAttributes;
using Centribo.Common.Extensions;

namespace Centribo.Common.Editor.Wizards {
	public class GenerateSpriteSliceDataWizard : ScriptableWizard {
		enum Mode { LoadSpritesFromTexture, PreviewSlicingOnTexture, UpdateExistingSlideData }

		[SerializeField] private Mode mode = Mode.LoadSpritesFromTexture;
		[SerializeField] private Texture2D texture;
		[SerializeField] private SpriteSliceDataContainer sliceDataToUpdate;
		[SerializeField, ShowAssetPreview] private List<Sprite> sprites;
		[SerializeField, ShowAssetPreview] private List<Sprite> slicedSpritesPreview;

		[MenuItem("Wizards/Generate Sprite Slice Data")]
		public static void CreateWizard() {
			DisplayWizard<GenerateSpriteSliceDataWizard>("Sprite Slicing", "Create slicing data from sprites", "Try to load sprites from texture");
		}

		void OnWizardUpdate() {
			UpdateOtherButton();
			UpdateCreateButton();

			if (sprites == null || sprites.Count == 0) {
				errorString = "No sprites set";
				return;
			}

			int count = 0;
			for (int i = 0; i < sprites.Count; i++) {
				if (sprites[i] != null) count++;
			}

			if (count == 0) {
				errorString = "No sprites set";
				return;
			}

			errorString = "";
		}

		void OnWizardCreate() {
			switch (mode) {
				case Mode.LoadSpritesFromTexture:
				case Mode.PreviewSlicingOnTexture:
					SpriteSliceDataContainer asset = ScriptableObject.CreateInstance<SpriteSliceDataContainer>();
					asset.SliceData = sprites.GenerateSliceData();
					string fileName = texture != null ? texture.name : "Sprite Slice Data";
					fileName = $"{fileName}.asset";
					AssetDatabase.CreateAsset(asset, $"Assets/{fileName}");
					AssetDatabase.SaveAssets();
					EditorUtility.FocusProjectWindow();
					Selection.activeObject = asset;
					break;

				case Mode.UpdateExistingSlideData:
					EditorUtility.SetDirty(sliceDataToUpdate);
					sliceDataToUpdate.SliceData = sprites.GenerateSliceData();
					AssetDatabase.SaveAssets();
					EditorUtility.FocusProjectWindow();
					Selection.activeObject = sliceDataToUpdate;
					break;
			}

		}

		void OnWizardOtherButton() {
			switch (mode) {
				case Mode.LoadSpritesFromTexture:
					string spriteSheetPath = AssetDatabase.GetAssetPath(texture);
					Object[] objs = AssetDatabase.LoadAllAssetsAtPath(spriteSheetPath);
					sprites = new List<Sprite>(objs.OfType<Sprite>());
					OnWizardUpdate();
					break;
				case Mode.PreviewSlicingOnTexture:
					List<SpriteSliceData> sliceData = sprites.GenerateSliceData();
					slicedSpritesPreview = new List<Sprite>();
					foreach (SpriteSliceData slice in sliceData) {
						slicedSpritesPreview.Add(texture.TrySlice(slice));
					}
					break;
			}
		}

		void UpdateOtherButton() {
			switch (mode) {
				case Mode.LoadSpritesFromTexture: otherButtonName = "Try to load sprites from texture"; break;
				case Mode.PreviewSlicingOnTexture: otherButtonName = "Preview slicing on texture"; break;
				case Mode.UpdateExistingSlideData: otherButtonName = ""; break;
			}
		}

		void UpdateCreateButton() {
			switch (mode) {
				case Mode.LoadSpritesFromTexture:
				case Mode.PreviewSlicingOnTexture:
					createButtonName = "Create";
					break;
				case Mode.UpdateExistingSlideData:
					createButtonName = "Update";
					break;
			}
		}
	}
}