using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FirstPersonController : MonoBehaviour {

	//PUBLIC
	[Header("References")]
	public Transform BodyTransform;
	public CapsuleCollider BodyCollider;
	public Rigidbody BodyRigidbody;
	
	[Header("Look")]
	[Range(0, 90)]
	public float MaxVerticalAngle = 90;
	[Range(-90, 0)]
	public float MinVerticalAngle = -90;
	public bool LookSmoothEnabled = false;
	[Range(0, 25)]
	public float LookSmoothSpeed = 10;
	[Range(0, 5)]
	public float HorizontalLookSensitivity = 1;
	[Range(0, 5)]
	public float VerticalLookSensitivity = 1;

	[Header("Movement")]
	public float MoveSpeed = 5;
	public float MaxVelocityChange = 10;
	public float JumpForce = 5;
	public float InteractDistace = 3;
	
	[Header("Input")]
	public string HORIZONTAL_INPUT_AXIS = "Mouse X";
	public string VERTICAL_INPUT_AXIS   = "Mouse Y";
	public string FORWARD_INPUT_AXIS    = "Vertical";
	public string STRAFE_INPUT_AXIS     = "Horizontal";
	public string JUMP_BUTTON           = "Jump";
	public string PRIMARY_BUTTON        = "Fire1";
	public string INTERACT_BUTTON       = "Fire2";
	
	[Header("Debugging")]
	public bool DrawDebugGizmos;

	//PRIVATE
	private bool cursorIsLocked = true;
	private Camera playerCamera;
	private Quaternion targetCameraRotation;
	private Quaternion targetBodyRotation;

	// Use this for initialization
	void Start() {
		GetComponentReferences();
		targetCameraRotation = transform.localRotation;
		targetBodyRotation = BodyTransform.localRotation;
	}

	private void GetComponentReferences() {
		playerCamera = GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update() {
		if(DrawDebugGizmos){
			Debug.DrawRay(transform.position, transform.forward * InteractDistace, Color.green);
			Vector3 start = BodyTransform.position + (Vector3.down * BodyCollider.height / 2) - (Vector3.down * 0.1f);
			Vector3 end = start + (Vector3.down * 0.2f);
			Debug.DrawLine(start, end, Color.red);
		}

		LookUpdate();
		InteractUpdate();
		CursorLockUpdate();
	}

	void FixedUpdate(){
		if(IsOnGround()){
			Vector3 targetVelocity = new Vector3(Input.GetAxis(STRAFE_INPUT_AXIS), 0, Input.GetAxis(FORWARD_INPUT_AXIS));
			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= MoveSpeed;

			Vector3 velocity = BodyRigidbody.velocity;
			Vector3 velocityChange = targetVelocity - velocity;
			velocityChange.x = Mathf.Clamp(velocityChange.x, -MaxVelocityChange, MaxVelocityChange);
			velocityChange.y = 0;
			velocityChange.z = Mathf.Clamp(velocityChange.z, -MaxVelocityChange, MaxVelocityChange);
			BodyRigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

			if (Input.GetButtonDown(JUMP_BUTTON)) {
				BodyRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
			}
		}
	}

	void InteractUpdate(){
		if(Input.GetButtonDown(INTERACT_BUTTON)){
			RaycastHit hit;
			if(Physics.Raycast(transform.position, transform.forward, out hit)){
				if(hit.distance <= InteractDistace){
					Debug.Log(hit.transform.name);
				}
			}
		}
	}

	bool IsOnGround() {
		RaycastHit hit;
		Vector3 start = BodyTransform.position + (Vector3.down * BodyCollider.height / 2) - (Vector3.down * 0.1f);
		Vector3 end = start + (Vector3.down * 0.2f);
		bool didHit = Physics.Linecast(start, end, out hit);
		
		return didHit;
	}

	// Adapted from MouseLook.cs in Standard Assets
	Quaternion ClampRotationAroundXAxis(Quaternion q, float minX, float maxX) {
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
		
		angleX = Mathf.Clamp(angleX, minX, maxX);

		q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}

	private void LookUpdate() {
		if (cursorIsLocked) {
			float h = HorizontalLookSensitivity * Input.GetAxis(HORIZONTAL_INPUT_AXIS);
			float v = VerticalLookSensitivity * Input.GetAxis(VERTICAL_INPUT_AXIS);

			targetCameraRotation *= Quaternion.Euler(-v, 0, 0);
			targetBodyRotation *= Quaternion.Euler(0, h, 0);

			targetCameraRotation = ClampRotationAroundXAxis(targetCameraRotation, MinVerticalAngle, MaxVerticalAngle);

			if (LookSmoothEnabled) {
				BodyTransform.localRotation = Quaternion.Slerp(BodyTransform.localRotation, targetBodyRotation, LookSmoothSpeed * Time.deltaTime);
				transform.localRotation = Quaternion.Slerp(transform.localRotation, targetCameraRotation, LookSmoothSpeed * Time.deltaTime);
			} else {
				BodyTransform.localRotation = targetBodyRotation;
				transform.localRotation = targetCameraRotation;
			}
		}
	}

	private void CursorLockUpdate() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			cursorIsLocked = false;
		} else if (Input.GetMouseButtonUp(0)) {
			cursorIsLocked = true;
		}

		if (cursorIsLocked) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		} else {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}