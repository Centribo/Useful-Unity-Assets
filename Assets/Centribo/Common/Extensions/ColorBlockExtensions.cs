using UnityEngine;
using UnityEngine.UI;

namespace Centribo.Common.Extensions {
	[System.Serializable]
	public static class ColorBlockExtensions {

		/// <summary>
		/// Creates a colored version of <see cref="UnityEngine.UI.ColorBlock"/>
		/// </summary>
		public static ColorBlock CreateColoredColorBlock(ColorBlock colorBlock, Color color){
			ColorBlock cb = ColorBlock.defaultColorBlock;
			cb.highlightedColor *= color;
			cb.normalColor *= color;
			cb.selectedColor *= color;
			cb.pressedColor *= color;
			return cb;
		}
	}
}