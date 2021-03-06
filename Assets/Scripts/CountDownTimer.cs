﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour {

	public Canvas timerMenu;

	// change these two values accordingly
	const int PLANNING_TIME = 30; // in number of seconds
	const int ROLEPLAYING_TIME = 20; // in number of seconds

	public Button competingButton;
	public Button compromisingButton;
	public Button collaboratingButton;
	public Button avoidingButton;
	public Button accommodatingButton;
	public Button continueButton;

	public Canvas competingInfoCanvas;
	public Canvas compromisingInfoCanvas;
	public Canvas collaboratingInfoCanvas;
	public Canvas avoidingInfoCanvas;
	public Canvas accommodatingInfoCanvas;

	public Canvas instructionCanvas;
	public Text instructionText;

	public Canvas scenarioCanvas;
	public Text scenarioText;

	// variables for timer
	private int timeRemaining;
	public Text timerText;
	public AudioSource timerAlarm;
	bool donePlanning = false;
	bool doneRolePlaying = false;
	bool timeStarted = false;
	
	void Start () {
		timerMenu = timerMenu.GetComponent<Canvas> ();
		// buttons for intentions
		competingButton = competingButton.GetComponent<Button> ();
		compromisingButton = compromisingButton.GetComponent<Button> ();
		collaboratingButton = collaboratingButton.GetComponent<Button> ();
		avoidingButton = avoidingButton.GetComponent<Button> ();
		accommodatingButton = accommodatingButton.GetComponent<Button> ();

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

		scenarioCanvas = scenarioCanvas.GetComponent<Canvas> ();
		scenarioText = scenarioText.GetComponent<Text> ();
		scenarioCanvas.enabled = false;

		instructionCanvas = instructionCanvas.GetComponent<Canvas> ();
		instructionText = instructionText.GetComponent<Text> ();
		instructionCanvas.enabled = false;

		continueButton = continueButton.GetComponent<Button> ();

		timerMenu.enabled = false;

		// for testing, comment the line for final version
		//StartTimer ();
	}

	// do not need to use Update
	void Update () {

	}

	public void StartTimer() {
		// start buttons
		competingButton.gameObject.SetActive (true);
		compromisingButton.gameObject.SetActive (true);
		collaboratingButton.gameObject.SetActive (true);
		avoidingButton.gameObject.SetActive (true);
		accommodatingButton.gameObject.SetActive (true);
		
        // start the instruction text
		instructionText.text = "Click on your intention below to learn more about them!";
		instructionCanvas.enabled = true;

        /* instruction text locataion bug fix; it doesn't happen anymore but maybe in the future
        var instructionCanvasPos = instructionCanvas.GetComponent<RectTransform> ();
        instructionCanvasPos.localPosition = new Vector3 (0,145,0);
		Screen.SetResolution (Screen.currentResolution.width-1,
		                      Screen.currentResolution.height,
		                      false);
		Screen.SetResolution (Screen.currentResolution.width+1,
		                      Screen.currentResolution.height,
		                      false);
        */

        // start the scenario text
        scenarioText.text = GameEngine.getScenarioTitle () + "\n"
			+ GameEngine.getScenario ();
		scenarioCanvas.enabled = true;

        // disable continu until timer is done
        continueButton.gameObject.SetActive(false);

        // start the timer variables
        donePlanning = false;
		doneRolePlaying = false;
		timeRemaining = PLANNING_TIME;
		timerText.text = "02:00";
		timerMenu.enabled = true;
		if (!timeStarted) {
			InvokeRepeating ("CountDown", (float)1.0, (float)1.0);
			timeStarted = true;
		}
	}

	// function to count down the timer
	void CountDown() {
        // count down here
        timerText.color = Color.black;
        if (timeRemaining > 0) {
			timeRemaining--;
			int numMin = timeRemaining / 60;
			int numSec = timeRemaining % 60;

			if (numSec > 9) {
				timerText.text = "0" + numMin + ":" + numSec;
			} else {
				timerText.text = "0" + numMin + ":0" + numSec;
			}

            if (timeRemaining < 10) timerText.color = Color.red;
            else timerText.color = Color.black;

        } else if (timeRemaining == 0 && !donePlanning && !doneRolePlaying) {
			// planning has ended
            // disable everything except scenario and instruction
			competingButton.gameObject.SetActive(false);
			compromisingButton.gameObject.SetActive(false);
			collaboratingButton.gameObject.SetActive(false);
			avoidingButton.gameObject.SetActive(false);
			accommodatingButton.gameObject.SetActive(false);

			competingInfoCanvas.enabled = false;
			compromisingInfoCanvas.enabled = false;
			collaboratingInfoCanvas.enabled = false;
			avoidingInfoCanvas.enabled = false;
			accommodatingInfoCanvas.enabled = false;
			instructionText.text = "Role-play the scenario with your partner!";

			timerAlarm.Play ();
			donePlanning = true;
			timeRemaining = ROLEPLAYING_TIME;
		} else if (timeRemaining == 0 && donePlanning && !doneRolePlaying) {
			// roleplaying has ended
            // disable everything and enable continue button
			timerAlarm.Play ();
			doneRolePlaying = true;
			timeRemaining = 0;
			scenarioCanvas.enabled = false;
			instructionCanvas.enabled = false;
			continueButton.gameObject.SetActive (true);
			continueButton.enabled = true;
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

	// press ok to disable all intention information pop ups
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