using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FirstPersonController : MonoBehaviour {

	//PUBLIC
	public Transform BodyTransform;
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

	//PRIVATE
	private const string HORIZONTAL_INPUT_AXIS = "Mouse X";
	private const string VERTICAL_INPUT_AXIS = "Mouse Y";
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

	// Update is called once per frame
	void Update() {
		LookUpdate();
		CursorLockUpdate();
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

	private void GetComponentReferences() {
		playerCamera = GetComponent<Camera>();
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
