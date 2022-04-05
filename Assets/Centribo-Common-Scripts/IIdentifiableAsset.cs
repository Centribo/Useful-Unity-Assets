using System.Collections.Generic;
using Centribo.Common;
using NaughtyAttributes;
using UnityEngine;

namespace Centribo.Common {
	/// <summary>
	/// Interface for assets that can be stored in a <see cref="Centribo.Common.ScriptableObjectAssetDatabase{K, V}"/>
	/// </summary>
	/// <typeparam name="T">The type of the identifier</typeparam>
	public interface IIdentifiableAsset<T> {
		/// <summary>
		/// Returns a unique identifier for this asset
		/// </summary>
		T GetUniqueIdentifier();

	}

	public static class IIdentifiableAssetExtensions {

		/// <summary>
		/// Determines whether two non-null objects are the same asset, using their unique identifiers.
		/// If either object given is null, this function will return false.
		/// </summary>
		public static bool IsSameAsset<T>(this IIdentifiableAsset<T> objectA, IIdentifiableAsset<T> objectB) {
			if (objectA == null || objectB == null) { return false; }
			return objectA.GetUniqueIdentifier().Equals(objectB.GetUniqueIdentifier());
		}
	}
}