using System.Collections.Generic;
using Centribo.Common;
using NaughtyAttributes;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Centribo.Common {
	/// <summary>
	/// A <see cref="Centribo.Common.ScriptableObjectSingleton{T}"/> that stores references to assets so that they can retrieved quickly at runtime.
	/// </summary>
	/// <typeparam name="K">The type to use for identifying assets. See <see cref="Centribo.Common.IIdentifiableAsset{T}"/>.</typeparam>
	/// <typeparam name="V">The asset type. Must implement <see cref="Centribo.Common.IIdentifiableAsset{T}"/>.</typeparam>
	public class ScriptableObjectAssetDatabase<K, V> : ScriptableObjectSingleton<ScriptableObjectAssetDatabase<K, V>> where V : UnityEngine.Object, IIdentifiableAsset<K> {
		public List<V> AllAssets;
		private Dictionary<K, V> lookup;

		void Awake() {
			RebuildDatabase();
		}


		public void RebuildDatabase() {
			if (lookup == null) { lookup = new Dictionary<K, V>(); } else { lookup.Clear(); }

			foreach (V asset in AllAssets) {
				if (asset == null) continue;

				if (lookup.ContainsKey(asset.GetUniqueIdentifier())) {
					Debug.LogError($"Error building {name}: Trying to add {typeof(V).FullName} with ID #{asset.GetUniqueIdentifier()} but {lookup[asset.GetUniqueIdentifier()]} already has that ID#. Skipping.");
				} else {
					lookup[asset.GetUniqueIdentifier()] = asset;
				}
			}
		}

		public V GetAsset(K id) {
			if (lookup == null) RebuildDatabase();
			return lookup.ContainsKey(id) ? lookup[id] : null;
		}

		[Button("Validate")]
		public void Validate() {
			Dictionary<K, List<V>> assetsByID = new Dictionary<K, List<V>>();
			foreach (V asset in AllAssets) {
				if (asset == null) continue;

				if (assetsByID.ContainsKey(asset.GetUniqueIdentifier())) {
					assetsByID[asset.GetUniqueIdentifier()].Add(asset);
				} else {
					List<V> list = new List<V>();
					list.Add(asset);
					assetsByID[asset.GetUniqueIdentifier()] = list;
				}
			}

			string errors = "";
			foreach (K id in assetsByID.Keys) {
				if (assetsByID[id].Count > 1) {
					errors += $"{assetsByID[id].Count} objects found with duplicate ID #{id}:\n";
					foreach (V asset in assetsByID[id]) {
						errors += $"\t{asset.name}\n";
					}
				} else {
					// Debug.Log(objectsByID[id][0]);
				}
			}

			if (!string.IsNullOrEmpty(errors)) {
				Debug.LogError(errors);
			} else {
				Debug.Log($"No issues building {name}");
			}
		}

#if UNITY_EDITOR
		[Button("Find ALL assets in project (May take a while...)")]
		public void FindAllAssetsInProject() {
			HashSet<V> objectSet = new HashSet<V>();

			string[] guids = AssetDatabase.FindAssets($"t:{typeof(V).ToString()}");
			foreach (string guid in guids) {
				string prefabPath = AssetDatabase.GUIDToAssetPath(guid);
				V asset = AssetDatabase.LoadAssetAtPath<V>(prefabPath);
				if (asset == null) { continue; }
				objectSet.Add(asset);
			}

			AllAssets.Clear();
			AllAssets.AddRange(objectSet);
		}
#endif
	}
}