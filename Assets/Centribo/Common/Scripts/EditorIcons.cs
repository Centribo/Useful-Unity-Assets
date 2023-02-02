using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Centribo.Common.Editor {
	public static class EditorIcons {
		public static readonly string Label_Grey = "sv_label_0";
		public static readonly string Label_Blue = "sv_label_1";
		public static readonly string Label_Teal = "sv_label_2";
		public static readonly string Label_Green = "sv_label_3";
		public static readonly string Label_Yellow = "sv_label_4";
		public static readonly string Label_Orange = "sv_label_5";
		public static readonly string Label_Red = "sv_label_6";
		public static readonly string Label_Pink = "sv_label_7";
		public static readonly string Dot_Grey = "sv_icon_dot0_pix16_gizmo";
		public static readonly string Dot_Blue = "sv_icon_dot1_pix16_gizmo";
		public static readonly string Dot_Teal = "sv_icon_dot2_pix16_gizmo";
		public static readonly string Dot_Green = "sv_icon_dot3_pix16_gizmo";
		public static readonly string Dot_Yellow = "sv_icon_dot4_pix16_gizmo";
		public static readonly string Dot_Orange = "sv_icon_dot5_pix16_gizmo";
		public static readonly string Dot_Red = "sv_icon_dot6_pix16_gizmo";
		public static readonly string Dot_Pink = "sv_icon_dot7_pix16_gizmo";
		public static readonly string Diamond_Grey = "sv_icon_dot8_pix16_gizmo";
		public static readonly string Diamond_Blue = "sv_icon_dot9_pix16_gizmo";
		public static readonly string Diamond_Teal = "sv_icon_dot10_pix16_gizmo";
		public static readonly string Diamond_Green = "sv_icon_dot11_pix16_gizmo";
		public static readonly string Diamond_Yellow = "sv_icon_dot12_pix16_gizmo";
		public static readonly string Diamond_Orange = "sv_icon_dot13_pix16_gizmo";
		public static readonly string Diamond_Red = "sv_icon_dot14_pix16_gizmo";
		public static readonly string Diamond_Pink = "sv_icon_dot15_pix16_gizmo";

		public static void SetIcon(this UnityEngine.Object obj, string iconName) {
#if UNITY_EDITOR
			GUIContent icon = UnityEditor.EditorGUIUtility.IconContent(iconName);
			if (icon == null) return;
			UnityEditor.EditorGUIUtility.SetIconForObject(obj, icon.image as Texture2D);
#endif
		}

		public static void SetIcon(this UnityEngine.GameObject obj, string iconName) {
			SetIcon(obj as UnityEngine.Object, iconName);
		}
	}
}
