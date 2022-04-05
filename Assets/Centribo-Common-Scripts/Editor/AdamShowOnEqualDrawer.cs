using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(AdamShowOnEqualAttribute))]
public class AdamShowOnEqualDrawer : PropertyDrawer {
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		Debug.Log("Running");

		AdamShowOnEqualAttribute equalAttribute = (AdamShowOnEqualAttribute) attribute;
	}
}