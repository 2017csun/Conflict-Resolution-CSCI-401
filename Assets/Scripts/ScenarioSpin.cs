using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class ScenarioSpin : NetworkBehaviour {
	
	private bool spinning = false;
	private float currentSpeed = 0;
	public int currentRoll = 0;
	private List<int> previousSpun;
//	public NetworkView nView;

	// Use this for initialization
	void Start () {
//		nView = GetComponent<NetworkView>();
//		nView.observed = this;
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
		previousSpun = new List<int>();
	}

	void DecreaseSpeed() {
		if (currentSpeed > 10 && currentSpeed < 20) {
			currentSpeed = 0f;
		} else {
			currentSpeed -= 10f;
		}
		if (currentSpeed == 0) {
			CancelInvoke();
			spinning = false;
		}
	}

	int calculateAngle(int number) {
		return (25 + (number * 45));
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("space") && GameEngine.allowScenarioSpin) {
			if (!spinning) {
				GameEngine.allowScenarioSpin = false;
				if(previousSpun.Count == 8) {
					previousSpun.Clear();
				}
				int roll = Random.Range (0, 8);
				while(previousSpun.Contains(roll)) {
					Debug.Log ("Rerolling cause same, got " + roll);
					roll = Random.Range (0, 8);
				}
				previousSpun.Add(roll);
				GameEngine.setScenario(roll);
				int equivAngle;
				if (currentRoll > roll) {
					equivAngle = (calculateAngle (roll) + 360) - calculateAngle (currentRoll);
				} else {
					equivAngle = calculateAngle (roll) - calculateAngle (currentRoll);
				}
				int rotations = Random.Range (2, 5);
//				Debug.Log ("Init: " + currentRoll);
//				Debug.Log ("Curr: " + roll);
//				Debug.Log ("Angle: " + equivAngle);
//				Debug.Log ("Rotations: " + rotations);
				currentSpeed = (-10 + Mathf.Sqrt (100f + 160f*(360f*(float)(rotations)+(float)(equivAngle))))/2f;
				currentRoll = roll;
				spinning = true;
				InvokeRepeating ("DecreaseSpeed", 0.5f, 0.5f);
			}
		}
		transform.Rotate (Vector3.up, currentSpeed * Time.deltaTime);
	}
}
