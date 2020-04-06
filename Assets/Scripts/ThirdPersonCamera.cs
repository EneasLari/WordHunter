using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {


	public Transform lookAt;

	public float distance = 10.0f;
	private float currentX = 0.0f;
	private float currentY = 30.0f;
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 dir = new Vector3 (0, 0, -distance);
		Quaternion rotation = Quaternion.Euler (currentY, currentX, 0);
		transform.position = lookAt.position + rotation * dir;
	}
}
