using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class CommonCoroutines {
	/// <summary>
	/// Fades a given sprite renderer from a given start color to a given end color, over a period of time (in seconds)
	/// </summary>
	static public IEnumerator FadeSpriteRenderer(this SpriteRenderer spriteRenderer, Color startColor, Color endColor, float fadeTime) {
		if (spriteRenderer == null) { yield break; }
		spriteRenderer.color = startColor;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime) {
			Color c = Color.Lerp(startColor, endColor, t);
			spriteRenderer.color = c;
			yield return new WaitForEndOfFrame();
		}

		spriteRenderer.color = endColor;
	}

	/// <summary>
	/// Fades a given image from a given start color to a given end color, over a period of time (in seconds)
	/// </summary>
	static public IEnumerator FadeImage(this Image image, Color startColor, Color endColor, float fadeTime) {
		if (image == null) { yield break; }
		image.color = startColor;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime) {
			Color c = Color.Lerp(startColor, endColor, t);
			image.color = c;
			yield return new WaitForEndOfFrame();
		}

		image.color = endColor;
	}

	/// <summary>
	/// Fades a given canvas group from a given start alpha to given end alpha, over a period of time (in seconds)
	/// </summary>
	static public IEnumerator FadeCanvasGroup(this CanvasGroup canvasGroup, float startAlpha, float endAlpha, float fadeTime) {
		if (canvasGroup == null) { yield break; }
		canvasGroup.alpha = startAlpha;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime) {
			canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
			yield return new WaitForEndOfFrame();
		}

		canvasGroup.alpha = endAlpha;
	}
}
