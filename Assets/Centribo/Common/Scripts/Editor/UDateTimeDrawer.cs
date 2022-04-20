using System;
using Centribo.Common;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Centribo.Common.Editor {
	// if we implement this PropertyDrawer then we keep the label next to the text field
#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(UDateTime))]
	public class UDateTimeDrawer : PropertyDrawer {
		// Draw the property inside the given rect
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			// Using BeginProperty / EndProperty on the parent property means that
			// prefab override logic works on the entire property.
			EditorGUI.BeginProperty(position, label, property);

			// Draw label
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			// Don't make child fields be indented
			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			// Calculate rects
			Rect amountRect = new Rect(position.x, position.y, position.width, position.height);

			// Draw fields - passs GUIContent.none to each so they are drawn without labels
			EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("_dateTime"), GUIContent.none);

			// Set indent back to what it was
			EditorGUI.indentLevel = indent;

			EditorGUI.EndProperty();
		}
	}
}
#endif