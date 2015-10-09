using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {

	private bool spinning;
	private float currentSpeed = 0;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
	}

	void OnMouseUpAsButton() {
		currentSpeed = 1000f;
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up, currentSpeed * Time.deltaTime);
		if (currentSpeed > 0) {
			currentSpeed -= 10f;
		}
	}
}
