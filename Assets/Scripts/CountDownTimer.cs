using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour {

	public Canvas timerMenu;

	// change these two values accordingly
	const int PLANNING_TIME = 15; // in number of seconds
	const int ROLEPLAYING_TIME = 8; // in number of seconds

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
	/*
	public Text competingText;
	public Text collaboratingText;
	public Text compromosingText;
	public Text avoidingText;
	public Text accommodatingText;
	*/

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
		

		/*
		timerText = timerText.GetComponent<Text> ();
		int numMin = timeRemaining / 60;
		int numSec = timeRemaining % 60;
		timerText.text = "0" + numMin + ":" + numSec;
		*/
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
		
		instructionText.text = "Click on your intention below to learn more about them!";
		instructionCanvas.enabled = true;
		scenarioText.text = GameEngine.getScenarioTitle () + "\n"
			+ GameEngine.getScenario ();
		scenarioCanvas.enabled = true;
		donePlanning = false;
		doneRolePlaying = false;
		continueButton.gameObject.SetActive (false);
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
		if (timeRemaining > 0) {
			timeRemaining--;
			int numMin = timeRemaining / 60;
			int numSec = timeRemaining % 60;

			if (numSec > 9) {
				timerText.text = "0" + numMin + ":" + numSec;
			} else {
				timerText.text = "0" + numMin + ":0" + numSec;
				timerText.color = Color.red;
			}

		} else if (timeRemaining == 0 && !donePlanning && !doneRolePlaying) {
			// planning has ended
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

			// disable all the buttons


		} else if (timeRemaining == 0 && donePlanning && !doneRolePlaying) {
			// roleplaying has ended
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