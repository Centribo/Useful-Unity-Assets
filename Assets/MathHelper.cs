using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathHelper : MonoBehaviour {
	public static Vector2 Rotate(Vector2 v, float degrees) {
		float radians = degrees * Mathf.Deg2Rad;
		float sin = Mathf.Sin(radians);
		float cos = Mathf.Cos(radians);

		float tx = v.x;
		float ty = v.y;

		return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
	}
	
	public static float LinearMap(float value, float inputMin, float inputMax, float outputMin, float outputMax){
		float output = Mathf.Clamp(value, inputMin, inputMax);
		
		float inputRange = inputMax - inputMin;
		float outputRange = outputMax - outputMin;

		output = ((output - inputMin) * (outputRange / inputRange)) + outputMin;
		return output;
	}
}