using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {

	private bool spinning;
	private float speed = 1000f;

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
			transform.Rotate (Vector3.up, speed);
			speed = speed - 5f;
		}

		if (speed == 0) {
			spinning = false;
		}
	}
}
