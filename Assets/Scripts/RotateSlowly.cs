using UnityEngine;
using System.Collections;

public class RotateSlowly : MonoBehaviour {

    public float rotationSpeed;

	// Update is called once per frame
	void Update () {
        this.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
	}
}
