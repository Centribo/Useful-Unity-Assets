using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Centribo.Common.Editor {
	public class ReserializeSelectedAssets {

		[MenuItem("Tools/Reserialize Selected Asset(s)")]
		[MenuItem("Assets/Reserialize Selected Asset(s)")]
		static void ReserializeSelection() {
			Object obj = Selection.activeObject;
			string[] assetGUIDs = Selection.assetGUIDs;
			List<string> paths = new List<string>();
			foreach (string guid in assetGUIDs) {
				string path = AssetDatabase.GUIDToAssetPath(guid);
				if (path == null || string.IsNullOrEmpty(path)) continue;
				paths.Add(path);
			}

			if (paths.Count > 0) {
				AssetDatabase.ForceReserializeAssets(paths.ToArray());
				Debug.Log($"Successfully reserialized {paths.Count} assets");
			} else {
				Debug.LogError($"Unable to reserialize any selected assets");
			}
		}
	}
}