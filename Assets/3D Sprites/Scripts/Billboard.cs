using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

	public bool IsEnabled = true;
	public Vector3 AdditionalRotaton;
	
	private Quaternion initialRotation;

	void Awake(){
		initialRotation = transform.rotation;
	}

	void Update () {
		if(IsEnabled){
			transform.LookAt(Camera.main.transform.position, -Vector3.up);
			transform.Rotate(AdditionalRotaton);
		} else {
			transform.rotation = initialRotation;
		}
	}
}
