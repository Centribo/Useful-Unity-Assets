using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Centribo.Common {
	[CreateAssetMenu(fileName = "Sprite Slice Data Container", menuName = "Centribo/Common/Sprite Slice Data Container")]
	/// <summary>
	/// Data container that can be used swap sprites on a SpriteRenderer at runtime
	/// </summary>
	public class SpriteSliceDataContainer : ScriptableObject {
		public List<SpriteSliceData> SliceData;
	}
}