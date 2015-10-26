using UnityEngine;
using System.Collections;

public class RotateSlowly : MonoBehaviour {

    public float rotationSpeed;

	// Update is called once per frame
	void Update () {
        this.transform.RotateAround(this.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
	}
}
