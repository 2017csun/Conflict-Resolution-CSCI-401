using UnityEngine;
using System.Collections;

public class Cinematic : MonoBehaviour {

    public GameObject fadeScene;        //  For fade ins and outs
    public int timeBeforeFadeIn;        //  How long before initial fade in

    //  Array of how long before moving to the next waypoint
    private int[] waypointTimes = new int[] {
        5
    };
    private bool triggerWaypointMove;   //  If true, move to the next waypoint
    private int currWaypointIndex;           //  Keeps track of which waypoint is next
    public GameObject[] waypoints;      //  All waypoints

    //  Movement stuff
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    //  Rotation stuff
    public Vector3[] rotationPoints;
    private int currRotationIndex = 0;
    private float rotateSpeed = 1f;
    private bool triggerRotate = false;

	void Start () {
        Camera.main.transform.position = new Vector3(-21.15f, 0.6f, 10.6f);
        Camera.main.transform.rotation = Quaternion.Euler(0, 180, 0);
        triggerWaypointMove = false;
        currWaypointIndex = 0;
        Invoke("fadeSceneIn", timeBeforeFadeIn);
        Invoke("nextWaypointMove", waypointTimes[0]);
	}
	
	void Update () {
        if (triggerWaypointMove) {
            moveToPoint(waypoints[currWaypointIndex].transform.position);

            //  If arrived at the waypoint
            if (Vector3.Distance(this.transform.position, waypoints[currWaypointIndex].transform.position) < 0.2f) {
                ++currWaypointIndex;
                triggerWaypointMove = false;
                velocity = Vector3.zero;

                switch (currWaypointIndex) {
                    case 1:
                        rotateSpeed = 0.5f;
                        Invoke("nextRotationSequence", 0.5f);
                        break;
                }
            }
        }

        if (triggerRotate) {
            rotateTo(rotationPoints[currRotationIndex]);

            //  If arrived at rotation
            if (Quaternion.Angle(this.transform.rotation, Quaternion.Euler(rotationPoints[currRotationIndex])) < 5) {
                ++currRotationIndex;
                triggerRotate = false;

                switch (currRotationIndex) {
                    case 1:
                        rotateSpeed = 1.5f;
                        Invoke("nextRotationSequence", 0.5f);
                        break;
                    case 2:
                        rotateSpeed = 1.5f;
                        Invoke("nextRotationSequence", 0);
                        break;
                    case 3:
                        //  Move to the console
                        Invoke("nextWaypointMove", 0.5f);
                        rotateSpeed = 1.2f;
                        Invoke("nextRotationSequence", 2.0f);
                        break;
                }
            }
        }
	}

    private void moveToPoint (Vector3 point) {
        this.transform.position = Vector3.SmoothDamp(
            this.transform.position,
            point,
            ref velocity,
            smoothTime
        );
    }

    private void rotateTo (Vector3 endRote) {
        this.transform.rotation = Quaternion.Slerp(
            this.transform.rotation,
            Quaternion.Euler(endRote),
            Time.deltaTime * rotateSpeed
        );
    }

    private void nextRotationSequence () {
        triggerRotate = true;
    }
    private void nextWaypointMove () {
        triggerWaypointMove = true;
    }

    private void fadeSceneIn () {
        fadeScene.GetComponent<FadeScene>().fadeIn();
    }
}
