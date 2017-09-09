using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {

	//Public variables
	public float minZoom = 15; //Minimum zoom in value
	public float maxZoom = 100; //Maximum zoom out value
	public float zoomBuffer = 5; //Extra "zoom" to add

	//Private variables
		//Camera shake
	Vector3 preShakePos;
	float shakeDecay;
	float shakeIntensity;
		//Camera follow
	public List<GameObject> targets;
	Vector3 target;
	Vector3 velocity;
	float smoothing = 0.3f; //"Smoothness" of camera

	// Use this for initialization
	void Start () {
		target = new Vector3(0, 0, -10);
		velocity = Vector3.zero;
	}

	// Update is called once per frame
	void Update () {
		ApplyCameraShake();
		SetTarget();
		transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothing);
		AdjustZoom();
	}
	
	public void AddTarget(GameObject target){
		targets.Add(target);
	}

	public void RemoveTarget(GameObject target){
		targets.RemoveAll(t => t.GetInstanceID() == target.GetInstanceID());
	}

	public void ShakeCamera(float intensity, float decay){
		preShakePos = transform.position;
		shakeIntensity = intensity;
		shakeDecay = decay;
	}

	void ApplyCameraShake(){
		if(shakeIntensity > 0){
			transform.position = preShakePos + Random.insideUnitSphere * shakeIntensity;
			shakeIntensity -= shakeDecay;
		}
	}

	void SetTarget(){
		Vector3 result = new Vector3(0, 0, 0); //The new position to be set
		int count = 0;
		foreach (GameObject go in targets){ //For all the pertinent objects
				result += go.transform.position; //Add them to the target
				count ++; //Count them
		}

		if(count != 0){
			result /= count; //Average all the positions
		}
		result.z = -10; //For making sure we can see everything
		target = result; //Set the target
	}

	void AdjustZoom(){ //Adjusts the zoom to fit the objects in targets to fit on screen. Assumes targets is already properly filled
		if(targets.Count > 1){ //If we have more than one object to focus on
			//Used to keep track of the max and min positions of the targets
			Vector2 maxPos = targets[0].transform.position;
			Vector2 minPos = targets[0].transform.position;

			foreach(GameObject go in targets){ //For each target
				//Check if their component and larger or smaller than the current
				//	largest/smallest positions, and set accordingly
				if(go.transform.position.x > maxPos.x){
					maxPos.x = go.transform.position.x;
				}
				if(go.transform.position.x < minPos.x){
					minPos.x = go.transform.position.x;
				}
				if(go.transform.position.y > maxPos.y){
					maxPos.y = go.transform.position.y;
				}
				if(go.transform.position.y < minPos.y){
					minPos.y = go.transform.position.y;
				}
			}
			
			//Get the height and width for the bounds of our zoom, plus our zoom buffer
			float deltaX = (maxPos.x - minPos.x) + zoomBuffer;
			float deltaY = (maxPos.y - minPos.y) + zoomBuffer;
			float newZoom = minZoom;

			//Since Camera.orthographicSize = height (in unity units) / 2, check if the width of the bounds is larger than the height of the bounds
			if(deltaX > deltaY){
				float screenRatio = Screen.width/Screen.height; //If it is, we'll need to do some similar triangle calculations
				newZoom = (deltaX/2) / screenRatio;
			} else { //Otherwise
				newZoom = deltaY/2; //Its just the height of our bounds / 2
			}

			//Clamp the new zoom value between our min and max zoom
			newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);
			Camera.main.orthographicSize = newZoom; //And set our zoom!
		}
	} //AdjustZoom
}
