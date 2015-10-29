using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class GameEngine : NetworkBehaviour {

    [HideInInspector]
    public GameObject myPlayer;

    //---------------------------------------------
    //	General game variables
    //---------------------------------------------
    [Header("General Game")]
    public GameObject checkpointFab;
    public GameObject checkpointLocations;
    private List<Transform> allCheckpoints;
    private List<string> playerNames;
    private int currCheckpoint;

	//---------------------------------------------
	//	Player input variables
	//---------------------------------------------
    [Header("Player Input Variables")]
	public GameObject[] playerIconFabs;
    public GameObject[] playerIcons;
	public Text[] playerNameTextFields;
	public GameObject nameInputPanel;
	public GameObject panelIconSelect;
	public GameObject spotlight;
	public GameObject summaryPanel;
	public GameObject buttonAddPlayer;
	public Text maxMessage;

    //---------------------------------------------
    //	Random player selection variables
    //---------------------------------------------
    [Header("Random Player Selection Variables")]
    public GameObject choosePlayersPanel;
    public GameObject playersChosenPanel;
    public Text iconName;
    public Text player1;
    public Text player2;
    public int roundNumber;//not input player variable

    //---------------------------------------------
    //	Wheel spinning variables
    //---------------------------------------------
    [Header("Wheel Spinning Variables")]

    private List<string> randomPlayerNames;
	private List<string> iconNames;
    private int currentIcon;

	//---------------------------------------------
	//	Pro/Con variables
	//---------------------------------------------
	[Header("Pro & Con Variables")]
	private List<string> answers;
	private string[] intentions;
	public GameObject proConPanel;

	void Start () {
		currCheckpoint = 0;

        //  Add all checkpoints
        allCheckpoints = new List<Transform>();
        foreach (Transform child in checkpointLocations.transform) {
            allCheckpoints.Add(child);
        }

		roundNumber = 0;
		currentIcon = -1;
		playerNames = new List<string> ();
		iconNames = new List<string> ();
		randomPlayerNames = new List<string> ();

	
        //  Spawn the first checkpoint
        Instantiate(checkpointFab, allCheckpoints[currCheckpoint].position, Quaternion.identity);

        //  Spawn the icons
        playerIcons = new GameObject[playerIconFabs.Length];
        for (int i = 0; i < playerIconFabs.Length; ++i) {
            GameObject go = Instantiate(playerIconFabs[i], new Vector3(0, -10, 0), Quaternion.identity) as GameObject;
            playerIcons[i] = go;
        }
	}

    void Update () {
        //  Set myPlayer
        if (myPlayer == null) {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player")) {
                if (go.GetComponent<FirstPersonController>().isActiveAndEnabled) {
                    myPlayer = go;
                }
            }
        }

        //  Don't let other players activate our checkpoint
        //      This is a terrible way to do it but it's the easiest to implement 
        GameObject check = GameObject.FindGameObjectWithTag("Checkpoint");
        if (check != null) {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player")) {
                if (!go.GetComponent<FirstPersonController>().isActiveAndEnabled) {
                    Physics.IgnoreCollision(
                        go.GetComponent<Collider>(),
                        check.GetComponent<Collider>()
                    );
                }
            }
        }
    }

    public void activateNameInputPanel () {
        //  Display the UI
		nameInputPanel.SetActive (true);

        //  Disable player controls
        myPlayer.GetComponent<FirstPersonController>().enabled = false;
    }

    public void deactivateNameInputPanel () {
        //  Hide the UI
		nameInputPanel.SetActive (false);

        //  Enable player controls
        myPlayer.GetComponent<FirstPersonController>().enabled = true;
    }
	public void activateChoosePlayerPanel() {
		choosePlayersPanel.SetActive (true);
		print ("Activate Choose Players Panel");
		roundNumber++;
		//Disable player

        //  Disable player controls
        myPlayer.GetComponent<FirstPersonController>().enabled = false;
	}
	public void deactivateChoosePlayerPanel() {
		choosePlayersPanel.SetActive (false);

        //  Enable player controls
        myPlayer.GetComponent<FirstPersonController>().enabled = true;
	}

	public void activateChosenPanel() {
		playersChosenPanel.SetActive (true);
		displayRandomPlayers ();

        //  Disable player controls
        myPlayer.GetComponent<FirstPersonController>().enabled = false;
	}
	public void deactivateChosenPanel() {
		playersChosenPanel.SetActive (false);
		//playerIcons [index].SetActive (false);
		//playerIcons [index2].SetActive (false);

        //  Enable player controls
        myPlayer.GetComponent<FirstPersonController>().enabled = true;
	}
	public void activateProConPanel() {

		proConPanel.SetActive (true);
		myPlayer.GetComponent<FirstPersonController>().enabled = false;

	}
	public void deactivateProConPanel() {
		
		proConPanel.SetActive (false);
        myPlayer.GetComponent<FirstPersonController>().enabled = true;
		
	}
	public void nameSave(InputField name) {
		if (playerNames.Count <= playerNameTextFields.Length) {
			
			//TODO: error handling
			
		
			nameInputPanel.SetActive (false);

			name.placeholder.GetComponent<Text> ().text = "Enter Name";
			playerNames.Add (name.text);
	


			playerNameTextFields [playerNames.Count - 1].text = name.text;
		

			name.text = " ";
			panelIconSelect.SetActive (true);
			playerIcons [0].transform.parent.gameObject.SetActive (true);
			updateIconSelect ();
		} 
		else {
			summaryPanel.SetActive (true);
			maxMessage.text = "Max Players Reached. Press done to continue";


<<<<<<< HEAD

		}
=======
		name.text = " ";
		panelIconSelect.SetActive (true);
		updateIconSelect ();	
>>>>>>> 2c76598559e94767cc286f71a0982ba87de72c55
	}
        
	public void updateIconSelect() {
		//position the icon in front of the camera
        int toDisable = currentIcon;
        currentIcon = (currentIcon + 1) % playerIcons.Length;

        //  Skip over icon already owned by another player
        if (playerIcons[currentIcon].transform.parent != null) {
            Debug.Log(playerIcons[currentIcon].name + " already owned!");
            currentIcon = (currentIcon + 1) % playerIcons.Length;
        }

		playerIcons[currentIcon].transform.position =
            Camera.main.transform.position + Camera.main.transform.forward * .8f + new Vector3(0, -0.18f, 0);
		spotlight.transform.position = playerIcons [currentIcon].transform.position + new Vector3 (0, 0.5f, 0);
		spotlight.transform.rotation = Quaternion.Euler (90, 0, 0); 

		//disable the previous icon
        if (toDisable >= 0) {
            playerIcons[toDisable].SetActive(false);
        }
	
		//enable the current Icon
		playerIcons[currentIcon].SetActive (true);
		iconName.text = playerIcons[currentIcon].name;
		
		if (currentIcon == playerIcons.Length) {

			currentIcon = 0;

		}
	}

	public void saveIcon() {
        //  Update the player's body to be the icon
        GameObject myIcon = playerIcons[currentIcon];
        myPlayer.GetComponent<PlayerNetworking>().updateBodyToIcon(myIcon);

        //  TODO: Kristen can you comment what exactly is happening here
		if (!iconNames.Contains (iconName.text)) {
			iconNames.Add (iconName.text);

			for (int i = 0; i < iconNames.Count; i++) {

				playerNameTextFields [i].text = "Player " + (i + 1) + " " + playerNames [i] + " - " + iconNames [i];

			}
			panelIconSelect.SetActive (false);
			for (int i = 0; i < playerIcons.Length; i++) {
                if (playerIcons[i].transform.parent == null) {
                    playerIcons[i].SetActive(false);
                }
                else {
                    Debug.Log(playerIcons[i].name + " is owned by a player!");
                }
			}

			summaryPanel.SetActive (true);
		} 
		else 
		{
			//TODO handle error
			print ("already chosen");

		}
<<<<<<< HEAD

        //  Update the player's body to be the icon
        GameObject myIcon = playerIcons[currentIcon];
        myIcon.transform.localScale += new Vector3(.2f, .2f, .2f);
        myIcon.GetComponent<RotateSlowly>().enabled = false;    //  Stop the rotating script
        myIcon.transform.parent = player.transform;
        myIcon.transform.localPosition = new Vector3(0, 0, 0);
        myIcon.transform.localRotation = Quaternion.Euler(0, 90, 0);
        //myIcon.SetActive(true);
=======
>>>>>>> 2c76598559e94767cc286f71a0982ba87de72c55
	}

	public void donePlayerInput () {
		summaryPanel.SetActive (false);

		//	Re-enable player controls
        myPlayer.GetComponent<FirstPersonController>().enabled = true;
	}

	public void addPlayerInput () {
		summaryPanel.SetActive (false);
		nameInputPanel.SetActive (true);
	}

	public void displayRandomPlayers() {

	

		if (roundNumber == playerNames.Count / 2 || roundNumber == 1) {

			for (int i = 0; i < playerNames.Count; i++) {
				randomPlayerNames.Add(playerNames[i]);
			}
		}


		int index = Random.Range (0, randomPlayerNames.Count - 1);
		print (randomPlayerNames.Count);
			print(playerIcons[index].name);
		player1.text = randomPlayerNames [index];
		playerIcons [index].transform.localScale = new Vector3 (.5f, .5f, .5f);
		playerIcons [index].SetActive (true);
		playerIcons [index].transform.position = Camera.main.transform.position + Camera.main.transform.right * -.5f + Camera.main.transform.forward * .8f + Camera.main.transform.up * -.2f;
		//playerIcons [index].transform.localScale.Set (.5f, .5f, .5f);
		int index2 = Random.Range (0, randomPlayerNames.Count-1);

		if (index == index2) {
			 
			index2 = (index + 2) % (randomPlayerNames.Count -1);
		}
		print(player2.text = playerNames[index2]);
		player2.text = playerNames[index2];
		print(playerIcons[index2].name);
		playerIcons [index2].transform.localScale = new Vector3 (.5f, .5f, .5f);
		playerIcons [index2].SetActive (true);
		playerIcons [index2].transform.position = Camera.main.transform.position + Camera.main.transform.right * .5f + Camera.main.transform.forward * .8f + Camera.main.transform.up * -.2f;

		randomPlayerNames.Remove (playerNames [index]);
		randomPlayerNames.Remove (playerNames [index2]);


	}
	public void sendAnswers(List<string> ansList) {
		answers = new List<string> ();

		for (int i = 0; i < ansList.Count; i++) {

			answers.Add(ansList[i]);
		}



	}
	public void sendIntention(string[] intent){
		intentions = new string[6];
		for (int i = 0; i < intent.Length; i++) {
			intentions [i] = intent [i];
		}
	}
	
    public void checkpointHit() {
        //  Call appropriate function
		if (currCheckpoint == 0) {
			this.activateNameInputPanel ();
		}

		if (currCheckpoint == 1) {
			this.activateChoosePlayerPanel ();
		}

		if (currCheckpoint == 2) {
			this.activateProConPanel();
		}

        //  Spawn next checkpoint
		currCheckpoint++;
        if (currCheckpoint < allCheckpoints.Count) {
            GameObject check = Instantiate(
                checkpointFab,
                allCheckpoints[currCheckpoint].position,
                Quaternion.identity
            ) as GameObject;
        }
	}
}
