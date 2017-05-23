using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathExtensions : MonoBehaviour {
	public static Vector2 Rotate(Vector2 v, float degrees) {
		float radians = degrees * Mathf.Deg2Rad;
		float sin = Mathf.Sin(radians);
		float cos = Mathf.Cos(radians);

		float tx = v.x;
		float ty = v.y;

		return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
	}
}
