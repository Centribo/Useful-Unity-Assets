using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using NaughtyAttributes;
using Centribo.Common.Extensions;

namespace Centribo.Common.Editor.Wizards {
	public class GenerateSpriteSliceDataWizard : ScriptableWizard {
		enum OtherButtonMode { LoadSpritesFromTexture, PreviewSlicingOnTexture }

		[SerializeField] private OtherButtonMode otherButtonMode = OtherButtonMode.LoadSpritesFromTexture;
		[SerializeField] private Texture2D texture;
		[SerializeField, ShowAssetPreview] private List<Sprite> sprites;
		[SerializeField, ShowAssetPreview] private List<Sprite> slicedSpritesPreview;

		protected bool test;

		[MenuItem("Wizards/Generate Sprite Slice Data")]
		public static void CreateWizard() {
			DisplayWizard<GenerateSpriteSliceDataWizard>("Sprite Slicing", "Create slicing data from sprites", "Try to load sprites from texture");
		}

		void OnWizardUpdate() {
			UpdateOtherButton();

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
			SpriteSliceDataContainer asset = ScriptableObject.CreateInstance<SpriteSliceDataContainer>();
			asset.SliceData = sprites.GenerateSpliceData();
			string fileName = texture != null ? texture.name : "Sprite Slice Data";
			fileName = $"{fileName}.asset";
			AssetDatabase.CreateAsset(asset, $"Assets/{fileName}");
			AssetDatabase.SaveAssets();
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = asset;


			// QuestionData asset = ScriptableObject.CreateInstance<QuestionData>();
			// asset.Question = question;
			// string fileName = asset.GenerateAssetName() + ".asset";
			// AssetDatabase.CreateAsset(asset, QuestionPath + "/" + fileName);
			// AssetDatabase.SaveAssets();
			// EditorUtility.FocusProjectWindow();
			// Selection.activeObject = asset;
		}

		void OnWizardOtherButton() {
			switch (otherButtonMode) {
				case OtherButtonMode.LoadSpritesFromTexture:
					string spriteSheetPath = AssetDatabase.GetAssetPath(texture);
					Object[] objs = AssetDatabase.LoadAllAssetsAtPath(spriteSheetPath);
					sprites = new List<Sprite>(objs.OfType<Sprite>());
					OnWizardUpdate();
					test = false;
					break;
				case OtherButtonMode.PreviewSlicingOnTexture:
					List<SpriteSliceData> sliceData = sprites.GenerateSpliceData();
					slicedSpritesPreview = new List<Sprite>();
					foreach (SpriteSliceData slice in sliceData) {
						slicedSpritesPreview.Add(texture.TrySlice(slice));
					}
					break;
			}
		}

		void UpdateOtherButton() {
			switch (otherButtonMode) {
				case OtherButtonMode.LoadSpritesFromTexture: otherButtonName = "Try to load sprites from texture"; break;
				case OtherButtonMode.PreviewSlicingOnTexture: otherButtonName = "Preview slicing on texture"; break;
			}
		}
	}
}