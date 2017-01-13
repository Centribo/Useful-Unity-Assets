using UnityEngine;
using System.Collections;

public class MouseRotate : MonoBehaviour {

	float arcBallRadius = 0.9f;
	//Vector2 arcBallCenter;
	bool isDragging = false;

	Quaternion beforeQuaternion = Quaternion.identity;
	Quaternion currentQuaternion = Quaternion.identity;
	Vector3 beforeArcPoint = Vector3.zero;
	Vector3 currentArcPoint = Vector3.zero;

	// Use this for initialization
	void Start () {
		//arcBallCenter = new Vector2(Screen.width/2.0f, Screen.height/2.0f);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			OnStartDrag((int)Input.mousePosition.x, (int)Input.mousePosition.y);
		}

		OnMoveDrag((int)Input.mousePosition.x, (int)Input.mousePosition.y);
		
		if(Input.GetMouseButtonUp(0)){
			OnEndDrag();
		}

		transform.rotation = Quaternion.Inverse(currentQuaternion);
	}

	void OnStartDrag(int x, int y){
		isDragging = true;
		beforeArcPoint = ProjectOntoArcBall(x, y);
	}

	void OnEndDrag(){
		isDragging = false;
		beforeQuaternion = currentQuaternion;
	}

	void OnMoveDrag(int x, int y){
		if(isDragging){
			currentArcPoint = ProjectOntoArcBall(x, y);
			currentQuaternion = beforeQuaternion * ArcPointsToQuaternion(beforeArcPoint, currentArcPoint);
		}
	}

	Quaternion ArcPointsToQuaternion(Vector3  vFrom, Vector3  VTO){
		float fDot = Vector3.Dot (vFrom, VTO);
		Vector3 vPart = Vector3.Cross (vFrom, VTO);
		
		return new Quaternion(vPart.x, vPart.y, vPart.z, fDot);
	}

	Vector3 ProjectOntoArcBall(int x, int y){
		float xResult = (x - (float)Screen.width/2.0f)  / (arcBallRadius * (float)Screen.width/2.0f);
		float yResult = (y - (float)Screen.height/2.0f) / (arcBallRadius * (float)Screen.height/2.0f);
		float zResult = 0.0f;

		float magnitude = xResult*xResult + yResult*yResult;
		if(magnitude > 1.0f){
			float scale = 1.0f / Mathf.Sqrt(magnitude);
			xResult *= scale;
			yResult *= scale;
		} else {
			zResult = Mathf.Sqrt(1.0f - magnitude);
		}
	
		return new Vector3(xResult, yResult, zResult);
	}
}
