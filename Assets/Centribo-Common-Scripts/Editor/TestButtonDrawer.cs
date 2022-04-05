// By: Lotte (https://twitter.com/LotteMakesStuff)
// https://gist.github.com/LotteMakesStuff/dd785ff49b2a5048bb60333a6a125187
// NOTE put in a Editor folder

using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(TestButtonAttribute))]
public class TestButtonDrawer : DecoratorDrawer {
	public override void OnGUI(Rect position) {
		// cast the attribute to make it easier to work with
		var buttonAttribute = (attribute as TestButtonAttribute);

		// check if the button is supposed to be enabled right now
		if (EditorApplication.isPlaying && !buttonAttribute.isActiveAtRuntime)
			GUI.enabled = false;
		if (!EditorApplication.isPlaying && !buttonAttribute.isActiveInEditor)
			GUI.enabled = false;

		// figure out where were drawing the button
		var pos = new Rect(position.x, position.y, position.width, position.height - EditorGUIUtility.standardVerticalSpacing);
		// draw it and if its clicked...
		if (GUI.Button(pos, buttonAttribute.buttonLabel)) {
			// tell the current game object to find and run the method we asked for!
			Selection.activeGameObject.BroadcastMessage(buttonAttribute.methodName);
		}

		// make sure the GUI is enabled when were done!
		GUI.enabled = true;
	}

	public override float GetHeight() {
		return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 2;
	}
}
