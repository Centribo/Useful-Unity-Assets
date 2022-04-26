using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Centribo.Common {
	/// <summary>
	/// Data container that can be used swap sprites on a SpriteRenderer at runtime.
	/// Use <see cref="Centribo.Common.Editor.Wizards.GenerateSpriteSliceDataWizard"/> in the editor to generate this file.
	/// </summary>
	public class SpriteSliceDataContainer : ScriptableObject {
		public List<SpriteSliceData> SliceData;
	}
}