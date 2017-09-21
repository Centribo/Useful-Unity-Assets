using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class NewCameraController : MonoBehaviour {

	private Camera camera;
	private Vector2 rootPosition; //The main center of the camera position
	private Vector2 positionOffset; //Offset of the root position, see SetPositionOffset
	private Vector2 calculatedPosition;

	void Awake(){
		camera = GetComponent<Camera>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	int i = 0;

	// Update is called once per frame
	void Update () {
		rootPosition += Vector2.left * Time.deltaTime;
		i++;
		SetPositionOffset(Mathf.Deg2Rad * i, 2);

		CalculatePosition();
		transform.position = new Vector3(calculatedPosition.x, calculatedPosition.y, transform.position.z);
	}

	void CalculatePosition(){
		calculatedPosition = rootPosition + positionOffset;
	}

	void SetPositionOffset(Vector2 offset){
		positionOffset = offset;
	}

	void SetPositionOffset(float angle, float magnitude){
		SetPositionOffset(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * magnitude);
	}

}
