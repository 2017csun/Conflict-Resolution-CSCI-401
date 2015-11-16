using UnityEngine;
using System.Collections;

public class AnimateRotateCamera : MonoBehaviour {

    private Quaternion startRotation, endRotation, currRotation;
	private Vector3 posToMoveTo, startPos, currPos;
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
			currPos = Vector3.Lerp(startPos, posToMoveTo, currTime / rotateTime);
            Camera.main.transform.rotation = currRotation;
			this.gameObject.transform.position = currPos;
        }
	}

	public void beginRotation (Quaternion to, float time, Vector3 pos) {
        rotateTime = time;
        currTime = 0;
        endRotation = to;
        startRotation = Camera.main.transform.rotation;
		startPos = this.gameObject.transform.position;
		currPos = startPos;
        currRotation = startRotation;
		posToMoveTo = new Vector3(pos.x, startPos.y, pos.z);
        isRotating = true;
    }
}
