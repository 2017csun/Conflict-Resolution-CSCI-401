using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour {

	// change these two values accordingly
	const int PLANNING_TIME = 10;
	const int ROLEPLAYING_TIME = 10;

	// info cards
	public Text competingText;
	public Text collaboratingText;
	public Text compromosingText;
	public Text avoidingText;
	public Text accommodatingText;

	// variables for timer
	private int timeRemaining = PLANNING_TIME;
	public Text timerText;
	public AudioSource timerAlarm;
	bool donePlanning = false;
	bool doneRolePlaying = false;
	
	void Start () {
		// info card initialization
		competingText = competingText.GetComponent<Text> ();
		competingText.supportRichText = true;
		competingText.text = "<size=25><b>Competing</b></size>\n" +
			"You are very competitive, serious, and defensive.\n" +
			"However, it may be too extreme that you start affecting your relationship" +
			"with others as well as hindering communication and negotiation.";

		collaboratingText = collaboratingText.GetComponent<Text> ();
		collaboratingText.supportRichText = true;
		collaboratingText.text = "<size=25><b>Collaborating</b></size>";


		compromosingText = compromosingText.GetComponent<Text> ();
		compromosingText.supportRichText = true;
		compromosingText.text = "<size=25><b>Compromosing</b></size>";

		accommodatingText = accommodatingText.GetComponent<Text> ();
		accommodatingText.supportRichText = true;
		accommodatingText.text = "<size=25><b>Accommodating</b></size>";

		avoidingText = avoidingText.GetComponent<Text> ();
		avoidingText.supportRichText = true;
		avoidingText.text = "<size=25><b>Avoiding</b></size>";


		// timer initialization
		timerText = timerText.GetComponent<Text> ();
		int numMin = timeRemaining / 60;
		int numSec = timeRemaining % 60;
		timerText.text = "0" + numMin + ":" + numSec;
		InvokeRepeating("CountDown", (float)1.0, (float)1.0);
	}
	// do not need to use Update
	void Update () {

	}

	// function to count down the timer
	void CountDown() {
		// count down here
		if (timeRemaining > 0) {
			timeRemaining--;
			int numMin = timeRemaining / 60;
			int numSec = timeRemaining % 60;
			if (numSec > 9) {
				timerText.text = "0" + numMin + ":" + numSec;
			} else {
				timerText.text = "0" + numMin + ":0" + numSec;
			}
		} else if (timeRemaining == 0 && !donePlanning && !doneRolePlaying) {
			// planning has ended
			timerAlarm.Play ();
			donePlanning = true;
			timeRemaining = ROLEPLAYING_TIME;

			competingText.enabled = false;
			collaboratingText.enabled = false;
			compromosingText.enabled = false;
			avoidingText.enabled = false;
			accommodatingText.enabled = false;

		} else if (timeRemaining == 0 && donePlanning && !doneRolePlaying) {
			// roleplaying has ended
			timerAlarm.Play ();
			doneRolePlaying = true;
			timeRemaining = 0;
		}
	}
}