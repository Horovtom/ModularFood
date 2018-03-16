using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphMovement : MonoBehaviour {

	public float dragSpeed = 200f;
	private Vector3 dragOrigin;
	private Quaternion original;

	void Start() {
		original = GetComponent<Transform>().rotation;
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetMouseButton(0) && Input.mousePosition.y < Screen.height - 100) {
			Transform t = GetComponent<Transform>();
			t.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * dragSpeed);
		}
		if (Input.GetMouseButtonDown(1)) {
			transform.rotation = original;
		}
	}
}
