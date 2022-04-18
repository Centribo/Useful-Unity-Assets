using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Centribo.Common {
	/// <summary>
	/// Class for a singleton that is held in a ScriptableObject
	/// Be sure to store the actual ScriptableObject file in the Resources folder.
	/// </summary>
	public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObjectSingleton<T> {
		private static T instance;
		public static T Instance {
			get {
				if (instance == null) {
					T[] assets = Resources.LoadAll<T>("");
					if (assets == null || assets.Length < 1) {
						throw new System.Exception($"Unable to find any {typeof(T).FullName} in the resources folder.");
					} else if (assets.Length > 1) {
						Debug.LogError($"Found more than one {typeof(T).FullName} in the resources folder.");
					}

					instance = assets[0];
				}

				return instance;
			}
		}
	}
}