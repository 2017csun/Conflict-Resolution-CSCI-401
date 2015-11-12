﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour {

	public Canvas timerMenu;

	// change these two values accordingly
	const int PLANNING_TIME = 5; // in number of seconds
	const int ROLEPLAYING_TIME = 8; // in number of seconds

	public Button competingButton;
	public Button compromisingButton;
	public Button collaboratingButton;
	public Button avoidingButton;
	public Button accommodatingButton;

	public Canvas competingInfoCanvas;
	public Canvas compromisingInfoCanvas;
	public Canvas collaboratingInfoCanvas;
	public Canvas avoidingInfoCanvas;
	public Canvas accommodatingInfoCanvas;
	public Canvas instructionCanvas;
	/*
	public Text competingText;
	public Text collaboratingText;
	public Text compromosingText;
	public Text avoidingText;
	public Text accommodatingText;
	*/

	// variables for timer
	private int timeRemaining = PLANNING_TIME;
	public Text timerText;
	public AudioSource timerAlarm;
	bool donePlanning = false;
	bool doneRolePlaying = false;
	
	void Start () {
		timerMenu = timerMenu.GetComponent<Canvas> ();
		//timerMenu.enabled = false;
		// buttons for intentions
		competingButton = competingButton.GetComponent<Button> ();
		competingButton.GetComponentInChildren<Text> ().text = "Competing";
		competingButton.GetComponentInChildren<Text> ().fontSize = 25;

		compromisingButton = compromisingButton.GetComponent<Button> ();
		compromisingButton.GetComponentInChildren<Text> ().text = "Compromising";
		compromisingButton.GetComponentInChildren<Text> ().fontSize = 25;

		collaboratingButton = collaboratingButton.GetComponent<Button> ();
		collaboratingButton.GetComponentInChildren<Text> ().text = "Collaborating";
		collaboratingButton.GetComponentInChildren<Text> ().fontSize = 25;

		avoidingButton = avoidingButton.GetComponent<Button> ();
		avoidingButton.GetComponentInChildren<Text> ().text = "Avoiding";
		avoidingButton.GetComponentInChildren<Text> ().fontSize = 25;

		accommodatingButton = accommodatingButton.GetComponent<Button> ();
		accommodatingButton.GetComponentInChildren<Text> ().text = "Accommodating";
		accommodatingButton.GetComponentInChildren<Text> ().fontSize = 25;

		competingButton.enabled = true;
		compromisingButton.enabled = true;
		collaboratingButton.enabled = true;
		avoidingButton.enabled = true;
		accommodatingButton.enabled = true;

		// intention popups
		competingInfoCanvas = competingInfoCanvas.GetComponent<Canvas> ();
		competingInfoCanvas.enabled = false;

		compromisingInfoCanvas = compromisingInfoCanvas.GetComponent<Canvas> ();
		compromisingInfoCanvas.enabled = false;

		collaboratingInfoCanvas = collaboratingInfoCanvas.GetComponent<Canvas> ();
		collaboratingInfoCanvas.enabled = false;

		avoidingInfoCanvas = avoidingInfoCanvas.GetComponent<Canvas> ();
		avoidingInfoCanvas.enabled = false;

		accommodatingInfoCanvas = accommodatingInfoCanvas.GetComponent<Canvas> ();
		accommodatingInfoCanvas.enabled = false;

		instructionCanvas = instructionCanvas.GetComponent<Canvas> ();
		instructionCanvas.enabled = true;


		/*
		// info card initialization
		competingText = competingText.GetComponent<Text> ();
		competingText.supportRichText = true;

		competingText.text = "<size=25><b>Competing</b></size>\n" +
			"You are very competitive, serious, and defensive. " +
			"However, it may be too extreme that you start affecting your relationship " +
			"with others as well as hindering communication and negotiation.";

		collaboratingText = collaboratingText.GetComponent<Text> ();
		collaboratingText.supportRichText = true;
		collaboratingText.text = "<size=25><b>Collaborating</b></size>\n" +
			"You seek to create positive relationships through solving problems for " +
			"not just yourself but others by communicating. It requires a lot of " +
			"effort especially when it involves new or sensitive ideas.";


		compromosingText = compromosingText.GetComponent<Text> ();
		compromosingText.supportRichText = true;
		compromosingText.text = "<size=25><b>Compromosing</b></size>\n" +
			"Your goal is to make sure both parties feel fair and satisfied enough. " +
			"The conclusion could be a lack of understanding and quality, which could also " +
			"lead to frustration because the problem may not be fully resolved.";

		accommodatingText = accommodatingText.GetComponent<Text> ();
		accommodatingText.supportRichText = true;
		accommodatingText.text = "<size=25><b>Accommodating</b></size>\n" +
			"You help others and do them favors, thereby developing your relationship with them. " +
			"By always agreeing, you lose motivation and respect as others do not see you as a " +
			"challenge. You may be too self-sacrificing.";

		avoidingText = avoidingText.GetComponent<Text> ();
		avoidingText.supportRichText = true;
		avoidingText.text = "<size=25><b>Avoiding</b></size>\n" +
			"You avoid problems to reduce distraction and stress. While this approach does " +
			"not mean to cause problems, it leads to a lack of communication, negative work " +
			"environment, and delay on projects.";
		*/


		// timer initialization
		/*
		timerText = timerText.GetComponent<Text> ();
		int numMin = timeRemaining / 60;
		int numSec = timeRemaining % 60;
		timerText.text = "0" + numMin + ":" + numSec;
		*/
		timerText.text = "02:00";
		//comment/uncomment this to start timer testing
		InvokeRepeating("CountDown", (float)1.0, (float)1.0);
	}
	// do not need to use Update
	void Update () {

	}

	public void StartTimer() {
		InvokeRepeating ("CountDown", (float)1.0, (float)1.0);
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
			instructionCanvas.enabled = false;
			competingButton.gameObject.SetActive(false);
			compromisingButton.gameObject.SetActive(false);
			collaboratingButton.gameObject.SetActive(false);
			avoidingButton.gameObject.SetActive(false);
			accommodatingButton.gameObject.SetActive(false);

			timerAlarm.Play ();
			donePlanning = true;
			timeRemaining = ROLEPLAYING_TIME;

			// disable all the buttons


		} else if (timeRemaining == 0 && donePlanning && !doneRolePlaying) {
			// roleplaying has ended
			timerAlarm.Play ();
			doneRolePlaying = true;
			timeRemaining = 0;
			timerMenu.enabled = false;


		}
	}

	public void CompetingPressed() {
		competingInfoCanvas.enabled = true;
		compromisingInfoCanvas.enabled = false;
		collaboratingInfoCanvas.enabled = false;
		avoidingInfoCanvas.enabled = false;
		accommodatingInfoCanvas.enabled = false;
	}

	public void CompromisingPressed() {
		competingInfoCanvas.enabled = false;
		compromisingInfoCanvas.enabled = true;
		collaboratingInfoCanvas.enabled = false;
		avoidingInfoCanvas.enabled = false;
		accommodatingInfoCanvas.enabled = false;
	}

	public void CollaboratingPressed() {
		competingInfoCanvas.enabled = false;
		compromisingInfoCanvas.enabled = false;
		collaboratingInfoCanvas.enabled = true;
		avoidingInfoCanvas.enabled = false;
		accommodatingInfoCanvas.enabled = false;
	}

	public void AvoidingPressed() {
		competingInfoCanvas.enabled = false;
		compromisingInfoCanvas.enabled = false;
		collaboratingInfoCanvas.enabled = false;
		avoidingInfoCanvas.enabled = true;
		accommodatingInfoCanvas.enabled = false;
	}

	public void AccommodatingPressed() {
		competingInfoCanvas.enabled = false;
		compromisingInfoCanvas.enabled = false;
		collaboratingInfoCanvas.enabled = false;
		avoidingInfoCanvas.enabled = false;
		accommodatingInfoCanvas.enabled = true;
	}

	// may not need this function.
	public void OKPressed() {
		competingButton.enabled = true;
		compromisingButton.enabled = true;
		collaboratingButton.enabled = true;
		avoidingButton.enabled = true;
		accommodatingButton.enabled = true;

		competingInfoCanvas.enabled = false;
		compromisingInfoCanvas.enabled = false;
		collaboratingInfoCanvas.enabled = false;
		avoidingInfoCanvas.enabled = false;
		accommodatingInfoCanvas.enabled = false;
	}
}