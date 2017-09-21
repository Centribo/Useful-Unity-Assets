using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour {

	public AnimationCurve animationCurve;
	public float min;
	public float max;
	public bool repeat;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float time = Time.time;
		if(repeat){
			time = time % 1.0f;
		}

		float y = animationCurve.Evaluate(time);
		y = Mathf.Lerp(min, max, y);

		transform.position = new Vector3(0, y, 0);
	}
}
