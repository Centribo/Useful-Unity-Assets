using UnityEngine;

namespace Centribo.Common {
	public static class GameObjectExtensions {
		/// <summary>
		/// Checks whether <paramref name="gameObject"/> belongs to <paramref name="layerMask"/>
		/// </summary>
		public static bool IsOnLayerMask(this GameObject gameObject, LayerMask layerMask) {
			return layerMask == (layerMask | (1 << gameObject.layer));
		}
	}
}