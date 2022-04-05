using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Centribo.Common {
	public static class BitMaskExtensions {

		/// <summary>
		/// Returns true if every flag set in <paramref name="toCheck"/> is set to true in <paramref name="flags"/>
		/// </summary>
		public static bool IsSet(this int flags, int toCheck) {
			return (flags & toCheck) != 0;
		}

		/// <summary>
		/// Returns the value of unsetting every flag in <paramref name="toUnset"/> to false on <paramref name="flags"/>.
		/// </summary>
		public static int Unset(this int flags, int toUnset) {
			return flags & (~toUnset);
		}

		/// <summary>
		/// Returns the value of unsetting every flag in <paramref name="toSet"/> to true on <paramref name="flags"/>.
		/// </summary>
		public static int Set(this int flags, int toSet) {
			return flags | toSet;
		}
	}
}