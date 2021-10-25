using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Centribo.Common {
	public static class DrawGizmosExtensions {

		// Adapted from: https://gist.github.com/jeffvella/fc7e66dd3f871785bb6ebb6d98ca1b2f
		// by Jeffery Vella (https://github.com/jeffvella)
		public static void DrawStringLabel(string text, Vector3 worldPos, Color? textColor = null, Color? backColor = null) {
			UnityEditor.Handles.BeginGUI();
			var restoreTextColor = GUI.color;
			var restoreBackColor = GUI.backgroundColor;

			GUI.color = textColor ?? Color.white;
			GUI.backgroundColor = backColor ?? Color.black;

			var view = UnityEditor.SceneView.currentDrawingSceneView;
			if (view != null && view.camera != null) {
				Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);
				if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0) {
					GUI.color = restoreTextColor;
					UnityEditor.Handles.EndGUI();
					return;
				}
				Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
				var r = new Rect(screenPos.x - size.x / 2, -screenPos.y + view.position.height + 4, size.x, size.y);
				GUI.Box(r, text, UnityEditor.EditorStyles.numberField);
				GUI.Label(r, text);
				GUI.color = restoreTextColor;
				GUI.backgroundColor = restoreBackColor;
			}
			UnityEditor.Handles.EndGUI();
		}
	}
}