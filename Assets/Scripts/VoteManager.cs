using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class VoteManager : MonoBehaviour {

	public GameEngine engine;
	public GameObject votePanel;
	public GameObject errorPanel;
	public Button submitButton;
	public AnimationPanel animPanel;
	public InputField upVotes;
	public InputField downVotes;
	public Text report;

	//	Submit button clicked
	public void submitClicked () {
		//	If no report, no vote, do the vote
		if (report.text.Length == 0) {
			//	Error checking
			int up = 0, down = 0;
			bool upFlag = int.TryParse(upVotes.text, out up);
			bool downFlag = int.TryParse(downVotes.text, out down);
			if (!upFlag || !downFlag || (up+down) != engine.allPlayers.Count) {
				//	Prep and display the error screen
				Text err = errorPanel.transform.FindChild("ErrorText").GetComponent<Text>();
				err.text =
					(!upFlag || !downFlag) ?
					"Error: Please fill out both fields" :
					"Error: Votes must total " + engine.allPlayers.Count
				;
				
				errorPanel.SetActive(true);
				return;
			}

			//	Display the report, change text color based on result
			Color reportC = report.color;
			Text text = submitButton.transform.GetChild(0).GetComponent<Text>();
			Color textC = text.color;
			if (up > down) {
				report.text = "Congratulations! Things are back on track!";
				reportC = textC = Color.green;
				text.text = "Yay!";
			}
			else if (up == down) {
				report.text = "You all don't know how to feel but that's okay!";
				reportC = textC = Color.blue;
				text.text = "OK";
			}
			else {
				report.text = "Too bad! Better luck next time.";
				reportC = textC = Color.red;
				text.text = "Bummer...";
			}
			report.color = reportC;
			text.color = textC;
		}
		else {
			//	Votes processed, close panel
			this.discardVotePanel();
		}
	}

	public void discardErrorScreen () {
		errorPanel.SetActive(false);
	}
	public void discardVotePanel () {
		votePanel.SetActive(false);
		animPanel.discardPanel();

		report.text = "";
		upVotes.text = "";
		downVotes.text = "";
		submitButton.transform.GetChild(0).GetComponent<Text>().text = "Submit";
		submitButton.transform.GetChild(0).GetComponent<Text>().color = Color.blue;

		//	Enable player
		engine.myPlayer.GetComponent<FirstPersonController>().enabled = true;
	}

	//	Open the vote panel
	public void openVotePanel () {
		//	Disable player
		engine.myPlayer.GetComponent<FirstPersonController>().enabled = false;

		animPanel.beginAnimation(Screen.currentResolution.width - 300, Screen.currentResolution.height - 200, 0.5f);
		Invoke("actuallyActivateVotePanel", animPanel.animationTime);
	}
	private void actuallyActivateVotePanel() {
		votePanel.SetActive(true);
	}
}
