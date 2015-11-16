using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class IntentionsSpin : NetworkBehaviour {
	[SyncVar]
	private bool spinning;
	private float currentSpeed;
	public int currentRoll;
	public GameEngine gameEngine;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
		spinning = false;
		currentSpeed = 0;
		currentRoll = 0;

		GameObject go = GameObject.FindGameObjectWithTag("Engine");
		if (go == null) {
			Debug.LogError("Error: Game Engine object has not been tagged as 'Engine'");
			return;
		}
		
		gameEngine = go.GetComponent<GameEngine>();
	}

	private void DecreaseSpeed() {
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

	private int calculateAngle(int number) {
		return (25 + (number * 72));
	}

	public void spinWheel(int intentionNumber) {
		int equivAngle;
		if (currentRoll > intentionNumber) {
			equivAngle = (calculateAngle (intentionNumber) + 360) - calculateAngle (currentRoll);
		} else {
			equivAngle = calculateAngle (intentionNumber) - calculateAngle (currentRoll);
		}
		int rotations = Random.Range (2, 5);
		currentSpeed = (-10 + Mathf.Sqrt (100f + 160f*(360f*(float)(rotations)+(float)(equivAngle))))/2f;
		currentRoll = intentionNumber;
		spinning = true;
		InvokeRepeating ("DecreaseSpeed", 0.5f, 0.5f);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("return") && !spinning) {
			if (this.isServer && gameEngine.allowP1IntentionSpin) {
				gameEngine.allowP1IntentionSpin = false;
				gameEngine.getIntention(0);
			}
			if(gameEngine.allowP2IntentionSpin) {
				gameEngine.allowP2IntentionSpin = false;
				gameEngine.player2Spun();
			}
		}
		transform.Rotate (Vector3.up, currentSpeed * Time.deltaTime);
	}
}
