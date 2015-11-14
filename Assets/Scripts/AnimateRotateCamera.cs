using UnityEngine;
using System.Collections;

public class AnimateRotateCamera : MonoBehaviour {

    private Quaternion startRotation, endRotation, currRotation;
    private float rotateTime, currTime;

    private bool isRotating;

	// Use this for initialization
	void Start () {
        isRotating = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isRotating) {
            currTime += Time.deltaTime;
            if (currTime >= rotateTime) {
                isRotating = false;
                return;
            }
            currRotation = Quaternion.Slerp(startRotation, endRotation, currTime / rotateTime);
            Camera.main.transform.rotation = currRotation;
        }
	}

    public void beginRotation (Quaternion to, float time) {
        rotateTime = time;
        currTime = 0;
        endRotation = to;
        startRotation = Camera.main.transform.rotation;
        currRotation = startRotation;
        isRotating = true;
    }
}
