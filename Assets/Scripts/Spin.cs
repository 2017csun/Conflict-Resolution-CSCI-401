using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {

	private bool spinning;
	private float initialSpeed = 1000f;
	private float finalSpeed = 0f;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
	}

	void OnMouseUpAsButton() {
		spinning = true;
	}

	// Update is called once per frame
	void Update () {
		if (spinning == true) {
			//transform.Rotate (Vector3.up, speed);
			currentSpeed = Mathf.Lerp(initialSpeed, finalSpeed, Time.deltaTime);
			transform.Rotate (Vector3.up, currentSpeed);
		}

		if (speed == 0) {
			spinning = false;
		}
	}
}
