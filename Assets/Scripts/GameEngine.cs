using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class GameEngine : MonoBehaviour {

    [HideInInspector]
    public GameObject player;

	//---------------------------------------------
	//	Player input variables
	//---------------------------------------------
	public GameObject[] playerIcons;
	public GameObject[] checkpoints;
	public int roundNumber;//not input player variable
	public Text[] playerNameTextFields;
	public GameObject nameInputPanel;
	public GameObject panelIconSelect;
	public int currentIcon;
	public Text iconName;
	public Text player1;
	public Text player2;
	public GameObject spotlight;
	public GameObject summaryPanel;
	public GameObject choosePlayersPanel;
	public GameObject playersChosenPanel;
	public int checkPointNum;

	[HideInInspector]
	public List<string> playerNames;
	public List<string> randomPlayerNames;

	[HideInInspector]
	public List<string> iconNames;

	// Use this for initialization
	void Start () {
		checkPointNum = 0;
		roundNumber = 1;
		currentIcon = 0;
        player = GameObject.FindGameObjectWithTag("Player");
		playerNames = new List<string> ();
		iconNames = new List<string> ();
		randomPlayerNames = new List<string> ();
	}

    void Update () {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void activateNameInputPanel () {
        //  Display the UI
		nameInputPanel.SetActive (true);

        //  Disable player controls
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void deactivateNameInputPanel () {
        //  Hide the UI
		nameInputPanel.SetActive (false);

        //  Enable player controls
        player.GetComponent<FirstPersonController>().enabled = true;
    }
	public void activateChoosePlayerPanel() {
		choosePlayersPanel.SetActive (true);
		print ("Activate Choose Players Panel");
		roundNumber++;
		//Disable player
		player.GetComponent<FirstPersonController>().enabled = false;

	}
	public void deactivateChoosePlayerPanel() {
		choosePlayersPanel.SetActive (false);




		player.GetComponent<FirstPersonController>().enabled = true;

	}

	public void activateChosenPanel() {
		playersChosenPanel.SetActive (true);
		displayRandomPlayers ();
		player.GetComponent<FirstPersonController>().enabled = false;

	}
	public void deactivateChosenPanel() {


		playersChosenPanel.SetActive (false);
		//playerIcons [index].SetActive (false);
		//playerIcons [index2].SetActive (false);



		player.GetComponent<FirstPersonController>().enabled = true;

	}



	public void nameSave(InputField name) {
		nameInputPanel.SetActive (false);

		name.placeholder.GetComponent<Text> ().text = "Enter Name";
		playerNames.Add (name.text);
		if (playerNames.Count > playerNameTextFields.Length) {

			//TODO: error handling

		} 

		else {
			playerNameTextFields[playerNames.Count - 1].text = name.text;
		}

		name.text = " ";
		panelIconSelect.SetActive (true);
		playerIcons [0].transform.parent.gameObject.SetActive (true);
		updateIconSelect ();	
	}

	public void updateIconSelect() {
		//position the icon in front of the camera
		playerIcons[currentIcon].transform.position = Camera.main.transform.position + Camera.main.transform.forward * .8f;
		spotlight.transform.position = playerIcons [currentIcon].transform.position + new Vector3 (0, 0.5f, 0);
		spotlight.transform.rotation = Quaternion.Euler (90, 0, 0); 
		//disable the previous icon
		int toFalse = currentIcon - 1;
		if (toFalse == -1) {

			toFalse = playerIcons.Length - 1;

		}
		playerIcons[toFalse].SetActive (false);
	
		//enable the current Icon
		playerIcons[currentIcon].SetActive (true);
		iconName.text = playerIcons[currentIcon].name;
		currentIcon++;
		if (currentIcon == playerIcons.Length) {

			currentIcon = 0;

		}
	}

	public void saveIcon() {


		if (!iconNames.Contains (iconName.text)) {
			iconNames.Add (iconName.text);

			for (int i = 0; i < iconNames.Count; i++) {

				playerNameTextFields [i].text = "Player " + (i + 1) + " " + playerNames [i] + " - " + iconNames [i];

			}
			panelIconSelect.SetActive (false);
			for (int i = 0; i < playerIcons.Length; i++) {
				playerIcons [i].SetActive (false);
			}

			summaryPanel.SetActive (true);
		} 
		else 
		{

			//TODO handle error
			print ("already chosen");
		}
	}

	public void donePlayerInput () {
		summaryPanel.SetActive (false);
		//	Re-enable player controls
		//	Remove all UI panels
		//	Spawn next checkpoint at role selection button
			//	Get CheckpointLocations[x]
			//	Instantiate(checkpoint, location)
		player.GetComponent<FirstPersonController>().enabled = true;
	}

	public void addPlayerInput () {
		summaryPanel.SetActive (false);
		nameInputPanel.SetActive (true);
		//	Re-enable player controls
		//	Remove all UI panels
		//	Spawn next checkpoint at role selection button
		//	Get CheckpointLocations[x]
		//	Instantiate(checkpoint, location)

	}


	public void displayRandomPlayers() {
		Random rnd = new Random ();
	

		if (roundNumber == playerNames.Count / 2 || roundNumber == 1) {

			for (int i = 0; i < playerNames.Count; i++) {
				randomPlayerNames.Add(playerNames[i]);
			}
		}


		int index = Random.Range (0, randomPlayerNames.Count - 1);
		print (randomPlayerNames.Count);
			print(playerIcons[index].name);
		player1.text = randomPlayerNames [index];
		playerIcons [index].SetActive (true);
		playerIcons [index].transform.position = Camera.main.transform.position + Camera.main.transform.right * -.5f + Camera.main.transform.forward * .8f + Camera.main.transform.up * -.2f;
		int index2 = Random.Range (0, randomPlayerNames.Count-1);

		if (index == index2) {
			print("YO THE SAMe"); 
			index2 = (index + 2) % (randomPlayerNames.Count -1);
		}
		print(player2.text = playerNames[index2]);
		player2.text = playerNames[index2];
		print(playerIcons[index2].name);
		playerIcons [index2].SetActive (true);
		playerIcons [index2].transform.position = Camera.main.transform.position + Camera.main.transform.right * .5f + Camera.main.transform.forward * .8f + Camera.main.transform.up * -.2f;

		randomPlayerNames.Remove (playerNames [index]);
		randomPlayerNames.Remove (playerNames [index2]);


	}
	public void selectPanel() {
		if (checkPointNum == 0) {

			this.activateNameInputPanel ();


		}

		if (checkPointNum == 1) {


			this.activateChoosePlayerPanel ();
		}
		checkPointNum++;
	}


}
