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
    public AnimationPanel animationPanel;
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
	public List<GameObject> currentPlayerIcons;
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
	private static string[] scenariosList;
	private static string[] scenariosTitles;
	private static string currScenario;
	private static string currScenarioTitle;
	private static string[] intentionsList;
	private static string[] currIntentions;

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
		currentPlayerIcons = new List<GameObject> ();

		currIntentions = new string[2];
		intentionsList = new string[]{"Competing","Compromising","Avoiding","Accomodating","Collaborating"};
		instantiateScenarios ();


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
                if (go != myPlayer) {
                    Physics.IgnoreCollision(
                        go.GetComponent<Collider>(),
                        check.GetComponent<Collider>()
                    );
                }
            }
        }
    }

    public void activateNameInputPanel () {
        //  Disable player controls
        myPlayer.GetComponent<FirstPersonController>().enabled = false;
        //  Start up the panel animation
        animationPanel.beginAnimation(250, 275, 0.9f);
        //  Wait for animation to finish before displaying UI
        Invoke("actuallyActivateNameInputPanel", animationPanel.animationTime);
    }
    private void actuallyActivateNameInputPanel () {
        //  Display the UI
        nameInputPanel.SetActive(true);
    }

    public void deactivateNameInputPanel () {
        //  Hide the UI
		nameInputPanel.SetActive (false);
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
        animationPanel.discardPanel();
		if (playerNames.Count <= playerNameTextFields.Length) {
			
			//TODO: error handling
			
		
			nameInputPanel.SetActive (false);

			name.placeholder.GetComponent<Text> ().text = "Enter Name";

			playerNames.Add (name.text);

	


			playerNameTextFields [playerNames.Count - 1].text = name.text;
		

			name.text = " ";
			panelIconSelect.SetActive (true);
            //  Start up the panel animation
            animationPanel.beginAnimation(500, 600, 0.3f);
            //  Wait for animation to finish before displaying UI
			Invoke("updateIconSelect", animationPanel.animationTime);
		} 
		else {
			summaryPanel.SetActive (true);
			maxMessage.text = "Max Players Reached. Press done to continue";
		}


	}
        
	public void updateIconSelect() {
		//position the icon in front of the camera
        int toDisable = currentIcon;
        currentIcon = (currentIcon + 1) % playerIcons.Length;


        //  Skip over icon already owned by another player
        if (playerIcons[currentIcon].transform.parent != null) {//playerIcons
			Debug.Log(playerIcons[currentIcon].name + " already owned!");
            currentIcon = (currentIcon + 1) % playerIcons.Length;
        }

		playerIcons[currentIcon].transform.position =//playericons
            Camera.main.transform.position + Camera.main.transform.forward * .8f + new Vector3(0, -0.18f, 0);
		spotlight.transform.position =
			playerIcons [currentIcon].transform.position +//playerIcons
            new Vector3 (0, 0.8f, 0) +
            (Camera.main.transform.forward * -1/8f);    //  Move it forward a little so more light hits icon
        spotlight.transform.LookAt(playerIcons[currentIcon].transform);

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
        //GameObject myIcon = playerIcons[currentIcon];
        //myPlayer.GetComponent<PlayerNetworking>().updateBodyToIcon(myIcon);

        //  TODO: Kristen can you comment what exactly is happening here
		if (!iconNames.Contains (iconName.text)) {
			iconNames.Add (iconName.text);
			currentPlayerIcons.Add(playerIcons[currentIcon]);
			for (int i = 0; i < iconNames.Count; i++) {

				playerNameTextFields [i].text = "Player " + (i + 1) + " " + playerNames [i] + " - " + iconNames [i];

			}
			panelIconSelect.SetActive (false);
			for (int i = 0; i < playerIcons.Length; i++) {
                if (playerIcons[i].transform.parent == null) {
                    playerIcons[i].SetActive(false);
                }
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
			print(currentPlayerIcons[index].name);
		player1.text = randomPlayerNames [index];
		//playerIcons [index].transform.localScale += new Vector3 (-.18f, -0.18f, -.18f);
		currentPlayerIcons [index].SetActive (true);
		currentPlayerIcons [index].transform.position = Camera.main.transform.position + Camera.main.transform.right * -.6f + Camera.main.transform.forward * .8f + Camera.main.transform.up * -.3f;
		//playerIcons [index].transform.localScale.Set (.5f, .5f, .5f);
		int index2 = Random.Range (0, randomPlayerNames.Count-1);

		if (index == index2) {
			 if(randomPlayerNames.Count == 2) {
				index2 = index + 1;
			}
			else {
			index2 =(index + 2)  % (randomPlayerNames.Count -1);
			}
		}
		print(player2.text = playerNames[index2]);
		player2.text = playerNames[index2];
		print(currentPlayerIcons[index2].name);
		//playerIcons [index2].transform.localScale += new Vector3 (-.18f, -0.18f, -.18f);
		currentPlayerIcons [index2].SetActive (true);
		currentPlayerIcons [index2].transform.position = Camera.main.transform.position + Camera.main.transform.right * .5f + Camera.main.transform.forward * .8f + Camera.main.transform.up * -.3f;

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

	public static void setIntention(int playerNumber, int intentionNumber) {
		currIntentions [0] = intentionsList [intentionNumber];
		Debug.Log ("Set Player " + playerNumber + " to Intention " + intentionsList [intentionNumber]);
	}

	public static void setScenario(int scenarioNumber) {
		currScenario = scenariosList [scenarioNumber];
		currScenarioTitle = scenariosTitles [scenarioNumber];
		Debug.Log ("Set scenario to " + currScenarioTitle);
	}

	private void instantiateScenarios() {
		scenariosList = new string[] {
			"Two weeks ago, the Captain asked the Cadet to prepare a report on the number of enemy invasions. The Manager plans to share the report at the quarterly intergalactic meeting. The Cadet procrastinates doing the work and plans to complete the report the morning of the meeting. When the Cadet reports to work, there is a system failure with the computer and the system will be down all day for repairs. When the Captain asks for the report for the meeting that will occur in an hour, the Cadet does not have the report to give to the Captain. What’s the Captain to do? What is the Cadet’s response when the Captain comes to ask for the report?", 
			"The new Lieutenant of Communications instructs the Lieutenant of Navigation to inform the staff of the new meeting location change from conference room to the flight deck. The Lieutenant of Navigation did not share the information with everyone. Some people show up at the conference room for the meeting and some show up on the flight deck. After the initial confusion, the meeting occurs on the flight deck. This isn’t the first time the Lieutenant of Navigation has made this mistake. After the meeting, the Lieutenant of Communications schedules a meeting with the Lieutenant of Navigation to resolve the miscommunication issue. What will the Lieutenant of Communications say? How will the Lieutenant of Navigation respond?", 
			"The Lieutenant Commander of Weapons in charge of the new satellite design and deployment project is working with the Ensign from Weapons who has the expertise on satellite design but no experience in the field of satellite deployment. The Ensign constantly challenges the Lieutenant Commander’s design plans. The Lieutenant Commander has been patient up to a point, but at the last meeting, erupts out of anger and frustration and shuts down the Ensign in front of the whole team. The Ensign sits in silence and quickly leaves at the end of the meeting. The Lieutenant Commander soon realizes the Ensign’s expertise is needed for the project. How will the Lieutenant Commander resolve the situation? What is the best way for the Ensign to respond to the Lieutenant Commander in light of the Lieutenant Commander actions at the meeting", 
			"The Captain encourages the Chief Officer to apply for the promotion to First Officer of the ship. The Chief Officer meets the requirements and submits the application according to protocol. Over dinner, the Captain reassures the Chief Officer the promotion belongs to the Chief Officer. A week later, the Chief Officer receives a letter from the Fleet Commander denying the application. The Chief Officer confronts the Captain in the transport room.  What will the Chief Officer say? How will the Captain seek to make things right?", 
			"The team of Staff Officers schedules a meeting to finish the final presentation on new communications software. At an earlier meeting, the roles and responsibilities were assigned and deadlines were established. Everyone agree and committed to the plan. On the day of the meeting, the Staff Officer of Communications is a no show. There is no notice and no materials are submitted for the meeting. The project lead, the Staff Officer of Technology is rightfully upset since the presentation is fewer than six hours away. The Staff Officer of Technology tracks down the Staff Officer of Communications in the officers’ quarters. How will the Staff Officer of Communications respond to not showing up to the meeting? How will the Staff Officer of Technology guide and direct the conversation?", 
			"During the last three team meetings to determine the hiring priorities for medical staff, the Physician’s Assistant talks too much and tends to dominate the discussion which impedes the efficiency of the discussion. The team delegates the lead nurse to speak with the Physician’s Assistant about the productivity of the next meeting. How with the Lead Nurse handle the situation? What will the outcome be for the Physician’s Assistant?", 
			"While working on the bridge, the Chief Officer questions the thoughts and ideas of commands from the Captain. The Chief Officer’s tone is sharp, condescending, and bordering insubordination. The Captain detects the Chief Officer’s defensive tone and believes it may be due to the fact that the Chief Officer recently was passed over promotion. Even though the Chief Officer has addressed the issue with the Captain earlier, it still seems the issue remains unresolved. The Captain asks the Chief Officer to meet with the Captain in the Captain’s office. What will the Captain say to bring resolution to the situation? What is bothering the Chief Officer so much that the issue is not yet resolved? What will it take to finally bring closure to the situation?", 
			"The Fleet Commander calls a meeting with the Captain to discuss the work ethic of the Lieutenant Commander of Communications. There is concern that the Lieutenant Commander is not keeping classified information confidential. The Fleet Commander learns from a Staff member that the Lieutenant Commander of Communications shares classified information with people not connected to the mission. The leak of classified information jeopardizes the Special Forces mission to rescue hostages from the enemy. The Fleet Commander directs the Captain to discuss the breach of confidentiality issue with the Lieutenant Commander. How will the Captain address the allegations with the Lieutenant Commander? What is the Lieutenant Commander’s response to the Fleet Commander’s concern?"
		};
		scenariosTitles = new string[] {
			"The Incomplete Assignment",
			"Meeting Miscommunication",
			"Design, Deployment, and Delegation",
			"Passed Over for Promotion",
			"No Show No Call",
			"Discussion Domination",
			"Unresolved Issues",
			"Confidentiality Breach"
		};
	}

	public static string getScenarioTitle() {
		return currScenarioTitle;
	}

	public static string getScenario() {
		return currScenario;
	}

	public static string[] getIntentions() {
		return currIntentions;
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
