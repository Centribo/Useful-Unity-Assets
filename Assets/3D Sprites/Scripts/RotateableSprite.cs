using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Billboard))]
[RequireComponent(typeof(Renderer))]
public class RotateableSprite : MonoBehaviour {
	
	[Tooltip("0 Degrees = Facing in the direction of the positive X axis")]
	public float FacingAngle;
	public bool ShowDebugInfo;
	
	public Sprite FrontSprite;
	public Sprite BackSprite;
	public Sprite LeftSprite;
	public Sprite RightSprite;
	public Sprite BackLeftSprite;
	public Sprite BackRightSprite;
	public Sprite FrontLeftSprite;
	public Sprite FrontRightSprite;

	Renderer renderer;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer>();
	}
	
	float mod(float x, float m) {
		return (x%m + m)%m;
	}

	// Update is called once per frame
	void Update () {
		float cameraX = Camera.main.transform.position.x;
		float cameraZ = Camera.main.transform.position.z;
		float viewingAngle = Mathf.Rad2Deg * Mathf.Atan2(cameraZ - transform.position.z, cameraX - transform.position.x) - FacingAngle;

		viewingAngle += 180;
		viewingAngle = mod(viewingAngle, 360);
		viewingAngle -= 180;

		if((viewingAngle > 157.5f && viewingAngle <= 180) || (viewingAngle >= -180 && viewingAngle < -157.5f)){
			SetSprite(BackSprite);
		} else if(viewingAngle <= 22.5f && viewingAngle > -22.5f){
			SetSprite(FrontSprite);
		} else if(viewingAngle <= 67.5f && viewingAngle > 22.5f){
			SetSprite(FrontLeftSprite);
		} else if(viewingAngle <= 112.5f && viewingAngle > 67.5f){
			SetSprite(LeftSprite);
		} else if(viewingAngle <= 157.5f && viewingAngle > 112.5f){
			SetSprite(BackLeftSprite);
		} else if(viewingAngle <= -22.5f && viewingAngle > -67.5f){
			SetSprite(FrontRightSprite);
		} else if(viewingAngle <= -67.5f && viewingAngle > -112.5f){
			SetSprite(RightSprite);
		} else if(viewingAngle <= -112.5f && viewingAngle > -157.5f){
			SetSprite(BackRightSprite);
		} else {
			Debug.LogWarning("Angle not valid: " + viewingAngle);
		}

		if(ShowDebugInfo){
			//  135 = Back left
			//   90 = Left
			//   45 = Front left
			//    0 = Front
			//  -45 = Front right
			//  -90 = Right
			// -135 = Back right
			// -180 = Back
			Debug.Log(viewingAngle);

			//Draw facing line
			Vector3 delta = new Vector3(Mathf.Cos(FacingAngle * Mathf.Deg2Rad), 0, Mathf.Sin(FacingAngle * Mathf.Deg2Rad));
			Debug.DrawLine(transform.position, transform.position + delta, Color.red);

			if((viewingAngle > 157.5f && viewingAngle <= 180) || (viewingAngle >= -180 && viewingAngle < -157.5f)){
				Debug.Log("Back");
			} else if(viewingAngle <= 22.5f && viewingAngle > -22.5f){
				Debug.Log("Front");
			} else if(viewingAngle <= 67.5f && viewingAngle > 22.5f){
				Debug.Log("FrontLeft");
			} else if(viewingAngle <= 112.5f && viewingAngle > 67.5f){
				Debug.Log("Left");
			} else if(viewingAngle <= 157.5f && viewingAngle > 112.5f){
				Debug.Log("BackLeft");
			} else if(viewingAngle <= -22.5f && viewingAngle > -67.5f){
				Debug.Log("FrontRight");
			} else if(viewingAngle <= -67.5f && viewingAngle > -112.5f){
				Debug.Log("Right");
			} else if(viewingAngle <= -112.5f && viewingAngle > -157.5f){
				Debug.Log("BackRight");
			}
		}
	}

	void SetSprite(Sprite sprite){
		float spriteWidth = sprite.rect.width;
		float spriteHeight = sprite.rect.height;
		float spritePPU = sprite.pixelsPerUnit;


		MaterialPropertyBlock props = new MaterialPropertyBlock();
		renderer.GetPropertyBlock(props);
		props.Clear();
		props.SetTexture("_MainTex", sprite.texture);
		renderer.SetPropertyBlock(props);

		transform.localScale = new Vector3(spriteWidth/spritePPU, 1, spriteHeight/spritePPU);
	}
}
