// By: Lotte (https://twitter.com/LotteMakesStuff)
// https://gist.github.com/LotteMakesStuff/0de9be35044bab97cbe79b9ced695585
// NOTE DONT put in an editor folder

using UnityEngine;

namespace Centribo.Common.Editor {
	public class MinMaxAttribute : PropertyAttribute {
		public float MinLimit = 0;
		public float MaxLimit = 1;
		public bool ShowEditRange;
		public bool ShowDebugValues;

		public MinMaxAttribute(float min, float max) {
			MinLimit = min;
			MaxLimit = max;
		}
	}
}