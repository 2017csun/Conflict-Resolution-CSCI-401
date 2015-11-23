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
	public GameObject errorPanel;
    public FadeScene fadeScene;
    private List<Transform> allCheckpoints;
    private List<string> playerNames;
    private List<int> playersChosenToPlay;
    private int currCheckpoint;
	public GameObject waitingForOtherPlayerPanel;
	[HideInInspector]
	[SyncVar]
	public bool checkpointCleared;
	[HideInInspector]
	[SyncVar]
	public int numPlayersHitCheckpoint;
	[HideInInspector]
	[SyncVar]
	public bool justResetGame;
	
    [HideInInspector]
    public List<PlayerClass> allPlayers;
    [SyncVar(hook = "onUpdateID")]
    [HideInInspector]
    public int currAvailableID = 1;
    private PlayerClass currEditedPlayer;   //  The player currently being modified from user input


	//---------------------------------------------
	// Gui Guide Variables
	//---------------------------------------------
	public GameObject welcomePanel;
    public GameObject scenarioGuidePanel;
    public GameObject intentionGuidePanel;
    private bool hasSeenScenarioGuide = false;
    private bool hasSeenIntentionGuide = false;

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
	int playerIntNum;
	public Text maxMessage;

    //---------------------------------------------
    //	Random player selection variables
    //---------------------------------------------
    [Header("Random Player Selection Variables")]
    [HideInInspector]
    public PlayerClass playerOneClass;
    [HideInInspector]
    public PlayerClass playerTwoClass;
    public GameObject choosePlayersPanel;
    public GameObject playersChosenPanel;
	GameObject placeholderplayer1;
	GameObject placeholderplayer2;
    public Text iconName;
    public Text player1;
    public Text player2;
    public int roundNumber;//not input player variable

    //---------------------------------------------
    //	Wheel spinning variables
    //---------------------------------------------
    [Header("Wheel Spinning Variables")]

	private List<string> iconNames;
    private int currentIcon;
	public bool allowP1IntentionSpin;
	[SyncVar]
	public bool allowP2IntentionSpin;
	public static bool allowScenarioSpin;
	private static string[] scenariosList;
	private static string[] scenariosTitles;
	private static string[] intentionsList;
	private List<int> previousSpunIntentions;
	public GameObject intentionsWheel;
	public GameObject spinWheelPanel;
	public Text spinWheelText;

	//---------------------------------------------
	//	Recap variables
	//---------------------------------------------
	[Header("Recap Screen Variables")]
	[SyncVar]
	private string currScenario;
	[SyncVar]
	private string currScenarioTitle;
	[SyncVar]
	private string player1Intention;
	[SyncVar]
	private string player2Intention;
	[SyncVar]
	private string player1Role;
	[SyncVar]
	private string player2Role;
	private static string player1RoleStatic;
	private static string player2RoleStatic;
	private static string currScenarioStatic;
	private static string currScenarioTitleStatic;
	public GameObject recapPanel;
	public Text player1NameText;
	public Text player2NameText;
	public Text player1RoleText;
	public Text player2RoleText;
	public Text player1IntentionText;
	public Text player2IntentionText;
	public Text scenarioTitleText;
	public Text scenarioText;

	// Planning and Role-playing
	[Header("Vote Variables")]
	public GameObject voteManager;

	[Header("Timer Variables")]
	public Canvas timerMenu;
	public CountDownTimer countDownTimer;
	//---------------------------------------------
	//	Pro/Con variables
	//---------------------------------------------
	[Header("Pro & Con Variables")]

	public List<string> answers;


	public List<string> answers2;

	private string[] intentions;
	private string[] answerListP1;
	private string[] answerListP2;
	private string[] intListPro1;
	private string[] intListCon1;
	private string[] intListPro2;
	private string[] intListCon2;
	public GameObject proConPanel;
	//public List<string> intentList;
	public List<string> intentListPro1;
	public List<string> intentListCon1;
	public List<string> intentListPro2;
	public List<string> intentListCon2;
	[SyncVar]
	private string p1pro1;
	[SyncVar]
	private string p1pro2;
	[SyncVar]
	private string p1pro3;
	[SyncVar]
	private string p1con1;
	[SyncVar]
	private string p1con2;
	[SyncVar]
	private string p1con3;
	[SyncVar]
	private string p2pro1;
	[SyncVar]
	private string p2pro2;
	[SyncVar]
	private string p2pro3;
	[SyncVar]
	private string p2con1;
	[SyncVar]
	private string p2con2;
	[SyncVar]
	private string p2con3;

	[SyncVar]
	private string intention1Long;

	[SyncVar]
	private string intention2Long;

	//[SyncVar]
	private string p1intpro1;
	//[SyncVar]
	private string p1intpro2;
	//[SyncVar]
	private string p1intpro3;
	//[SyncVar]
	private string p1intcon1;
	//[SyncVar]
	private string p1intcon2;
	//[SyncVar]
	private string p1intcon3;
	//[SyncVar]
	private string p2intpro1;
	//[SyncVar]
	private string p2intpro2;
	//[SyncVar]
	private string p2intpro3;
	//[SyncVar]
	private string p2intcon1;
	//[SyncVar]
	private string p2intcon2;
	//[SyncVar]
	private string p2intcon3;
	
	

	[Header("Score Variables")]
	public Text[] player1Answers;
	public Text[] player2Answers;
	public Text[] answerKey1;
	public Text[] answerKey2;
	public Text roundScore;
	public Text totalScore;
	public Text intentScoreText;
	[SyncVar]
	public string intent2;
	public Text intentScoreText2;
	public Text scoreName1;
	public Text scoreName2;
	[SyncVar]
	public int score;
	[SyncVar]
	public int totalscore;
	[SyncVar]
	public string wrongListLong;
	public GameObject scorePanel;
	public GameObject finishRoundButton;
	public GameObject ContinueButton;
	public ProsAndConsList pscript;
	private List<string> wrongList;
	private List<string> wrongList2;
	public GameObject Player1ProCon; // text containing
	public GameObject Player2ProCon;

	void Start () {
		currCheckpoint = 0;

        //  Add all checkpoints
        allCheckpoints = new List<Transform>();
        foreach (Transform child in checkpointLocations.transform) {
            allCheckpoints.Add(child);
        }


		totalscore = 0;
		roundNumber = 0;
		currentIcon = 0;
        allPlayers = new List<PlayerClass>();
		playerNames = new List<string> ();
		iconNames = new List<string> ();
        playersChosenToPlay = new List<int>();
		intentListPro1 = new List<string> ();
		intentListCon1 = new List<string> ();
		intentListPro2 = new List<string> ();
		intentListCon2 = new List<string> ();
		wrongList = new List<string> ();
		wrongList2 = new List<string> ();
		answerListP1 = new string[6];
		answerListP2 = new string[6];
		intListPro1 = new string[3];
		intListCon1 = new string[3];
		intListPro2 = new string[3];
		intListCon2 = new string[3];
		answers = new List<string> ();
		answers2 = new List<string> ();
		instantiateVariables ();
		instantiateScenarios ();
		playerIntNum = 1;
		timerMenu = timerMenu.GetComponent<Canvas> ();
		timerMenu.enabled = false;

        scenarioGuidePanel.SetActive(false);
        intentionGuidePanel.SetActive(false);

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
					myPlayer.GetComponent<FirstPersonController>().enabled = false;
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
	public void deactivateWelcomePanel() {
//		myPlayer.GetComponent<FirstPersonController>().enabled = false;
		welcomePanel.SetActive (false);
		//RETURN HERE
		if(this.isServer) {
			numPlayersHitCheckpoint++;
		} else {
			myPlayer.GetComponent<PlayerNetworking> ().updatePlayer2Hit ();
		}
		if(checkpointCleared) {
			// Do nothing
		} else {
			activateWaitingForOtherPlayerPanel();
			InvokeRepeating ("waitingToContinue", 0.5f, 0.5f);
		}

	}

    public void activateScenarioGuidePanel()
    {
        scenarioGuidePanel.SetActive(true);
    }

    public void deactivateScenarioGuidePanel()
    {
        scenarioGuidePanel.SetActive(false);
    }

    public void activateIntentionGuidePanel()
    {
        deactivateScenarioGuidePanel();
        intentionGuidePanel.SetActive(true);
    }

    public void deactivateIntentionGuidePanel()
    {
        intentionGuidePanel.SetActive(false);
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

        //  Disable player controls
        myPlayer.GetComponent<FirstPersonController>().enabled = false;
	}
	public void deactivateChoosePlayerPanel() {
		choosePlayersPanel.SetActive (false);
	}

	public void activateChosenPanel() {
		playersChosenPanel.SetActive (true);

        //  Check if players have already been chosen
        if (playerOneClass != null && playerTwoClass != null) {
            Debug.Log("Players have already been chosen!");
            this.displayRandomPlayers();
        }
        else {
            //  If client, call ServerChooseRandomPlayers() on the server game engine
            if (this.isServer) {
                ServerChooseRandomPlayers(true);
            }
            else {
                myPlayer.GetComponent<PlayerNetworking>().getRandomPlayersFromServer();
            }
        }

        //  Disable player controls
        myPlayer.GetComponent<FirstPersonController>().enabled = false;
	}
	public void deactivateChosenPanelAndStartRound() {
		playersChosenPanel.SetActive (false);

		//	Set the right layers for each icon
		playerOneClass.playerIcon.layer = LayerMask.NameToLayer("Player1");
		this.fullChangeLayer(playerOneClass.playerIcon.transform, "Player1");
		playerTwoClass.playerIcon.layer = LayerMask.NameToLayer("Player2");
		this.fullChangeLayer(playerTwoClass.playerIcon.transform, "Player2");
		if (this.isServer) {
			Camera.main.cullingMask = Camera.main.cullingMask | 1 << LayerMask.NameToLayer("Player2");
		}
		else {
			Camera.main.cullingMask = Camera.main.cullingMask | 1 << LayerMask.NameToLayer("Player1");
		}

        //  Update the player's body to be the icon
        GameObject myIcon = this.isServer ? playerOneClass.playerIcon : playerTwoClass.playerIcon;
        myPlayer.GetComponent<PlayerNetworking>().updateBodyToIcon(myIcon);

        //  Enable player controls
        myPlayer.GetComponent<FirstPersonController>().enabled = true;
	} 

	public void finishTimer() {
		timerMenu.enabled = false;
		myPlayer.GetComponent<FirstPersonController> ().enabled = true;
	}

	public void activateProConPanel() {
		if (this.isServer) {
//			pscript.populateScrollList (currIntentions [0]);
			pscript.populateScrollList (player1Intention);
			intentScoreText.text = player1Intention;
			//activate in score panel
			//update in score panel the intention
		}
		
		else {
			pscript.populateScrollList (player2Intention);
//			pscript.populateScrollList (currIntentions [0]);
			intentScoreText.text = player2Intention;
			intent2 = intentScoreText.text;
			myPlayer.GetComponent<PlayerNetworking>().setScoreIntent(intent2);
		}
		proConPanel.SetActive (true);

		myPlayer.GetComponent<FirstPersonController>().enabled = false;

	} 
	public void deactivateProConPanel() {
		
		proConPanel.SetActive (false);
        myPlayer.GetComponent<FirstPersonController>().enabled = true;
		
	}

	public void activateWaitingForOtherPlayerPanel () {
		//  Disable player controls
		myPlayer.GetComponent<FirstPersonController>().enabled = false;
		//  Start up the panel animation
		animationPanel.beginAnimation(310, 100, 0.9f);
		//  Wait for animation to finish before displaying UI
		Invoke("actuallyActivateWaitingForOtherPlayerPanel", animationPanel.animationTime);
	}
	
	private void actuallyActivateWaitingForOtherPlayerPanel () {
		//  Display the UI
		waitingForOtherPlayerPanel.SetActive(true);
	}
	
	public void deactivateWaitingForOtherPlayerPanel() {
		waitingForOtherPlayerPanel.SetActive (false);
		animationPanel.discardPanel();
		myPlayer.GetComponent<FirstPersonController>().enabled = true;
	}

	public void activateRecapPanel () {
		player1NameText.text = playerOneClass.playerName;
		player2NameText.text = playerTwoClass.playerName;
		player1RoleText.text = player1Role;
		player2RoleText.text = player2Role;
		player1IntentionText.text = player1Intention;
		player2IntentionText.text = player2Intention;
		scenarioText.text = currScenario;
		scenarioTitleText.text = currScenarioTitle;
		currScenarioTitleStatic = currScenarioTitle;
		currScenarioStatic = currScenario;
		//  Disable player controls
		myPlayer.GetComponent<FirstPersonController>().enabled = false;
		//  Start up the panel animation
		animationPanel.beginAnimation(1200, 550, 0.9f);
		//  Wait for animation to finish before displaying UI
		Invoke("actuallyActivateRecapPanel", animationPanel.animationTime);
	}

	private void actuallyActivateRecapPanel () {
		//  Display the UI
		recapPanel.SetActive(true);
	}

	public void deactivateRecapPanel() {
		recapPanel.SetActive (false);
		animationPanel.discardPanel();
		myPlayer.GetComponent<FirstPersonController>().enabled = true;
	}

	public void activateSpinWheelPanel () {
		if (this.isServer) {
			spinWheelText.text = "Please spin both Scenario and Intention Wheels.";
		} else {
			spinWheelText.text = "Please spin the Intention Wheel.";
		}
		//  Disable player controls
		myPlayer.GetComponent<FirstPersonController>().enabled = false;
		//  Start up the panel animation
		animationPanel.beginAnimation(380, 160, 0.9f);
		//  Wait for animation to finish before displaying UI
		Invoke("actuallyActivateSpinWheelPanel", animationPanel.animationTime);
	}
	
	private void actuallyActivateSpinWheelPanel () {
		//  Display the UI
		spinWheelPanel.SetActive(true);
	}
	
	public void deactivateSpinWheelPanel() {
		spinWheelPanel.SetActive (false);
		animationPanel.discardPanel();
		myPlayer.GetComponent<FirstPersonController>().enabled = true;
	}

	public void activateScorePanel() {
		
		
		scorePanel.SetActive (true);
		Player1ProCon.SetActive (true);
		//checkAnswers ();
		displayScore (); 
		myPlayer.GetComponent<FirstPersonController>().enabled = false;
		
		
	}
	public void activateSecondScorePanel() {

		//Player1ProCon.SetActive (false);
		//Player2ProCon.SetActive (true);


	}
	public void deactivateScorePanel() {
		
		scorePanel.SetActive (false);
		myPlayer.GetComponent<FirstPersonController>().enabled = true;
		
		
	}

    public void nameSave(InputField name) {
        animationPanel.discardPanel();

		if (playerNames.Count < playerNameTextFields.Length) {
            currEditedPlayer = new PlayerClass();
            Debug.Log("Creating player with ID " + currAvailableID);
            currEditedPlayer.playerID = currAvailableID;
            currEditedPlayer.playerName = name.text;
            currAvailableID += 1;
            Debug.Log("ID updated to " + currAvailableID);

            //  Must use command all to update ID from client to server
            if (!this.isServer) {
                myPlayer.GetComponent<PlayerNetworking>().updateCurrAvailableID(currAvailableID);
            }

            Debug.Log("Created a player with ID: " + currEditedPlayer.playerID + " and name: " + currEditedPlayer.playerName);
			
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
        //  Move the previous icon away
        playerIcons[currentIcon].transform.position = new Vector3(0, -20, 0);

        //  Get the next icon
        currentIcon = (currentIcon + 1) % playerIcons.Length;

        //  Skip over icon already owned by another player
        while (playerIcons[currentIcon].transform.parent != null) {//playerIcons
			Debug.Log(playerIcons[currentIcon].name + " already owned!");
            currentIcon = (currentIcon + 1) % playerIcons.Length;
        }

        //position the icon in front of the camera
		playerIcons[currentIcon].transform.position =//playericons
            Camera.main.transform.position + Camera.main.transform.forward * .8f + new Vector3(0, -0.18f, 0);



		spotlight.transform.position =
			playerIcons [currentIcon].transform.position +//playerIcons
            new Vector3 (0, 0.8f, 0) +
            (Camera.main.transform.forward * -1/8f);    //  Move it forward a little so more light hits icon
        spotlight.transform.LookAt(playerIcons[currentIcon].transform);

        //  Take out the (Clone) part of the name
        string name = playerIcons[currentIcon].name;
        int i = name.IndexOf("(Clone)");
        if (i != -1) {
            name = name.Substring(0, i) + name.Substring(i + 7);
        }
        iconName.text = name;
	}

	public void saveIcon() {
        /* If the currentIcon , "iconName" that just got saved is not the iconNames list (which contains the selected iconNames) 
         then add it to the list. Add it also to the GameObject List of actual icons (currentPlayer Icons)*/
		if (!iconNames.Contains (iconName.text)) {
			//	Take out (Clone) from icon name
			string name = iconName.text;
			int cloneIndex = name.IndexOf("(Clone)");
			if (cloneIndex != -1) {
				name = name.Substring(0, cloneIndex) + name.Substring(cloneIndex + 7);
			}
			iconNames.Add (name);

			//	Move the icon away
			playerIcons[currentIcon].transform.position = new Vector3(0, -20, 0);

            currEditedPlayer.playerIcon = Instantiate(playerIcons[currentIcon]);
			currEditedPlayer.playerIconIndex = currentIcon;
            allPlayers.Add(new PlayerClass(currEditedPlayer));
            Debug.Log("Added icon " + currEditedPlayer.playerIcon + " to player #" + currEditedPlayer.playerID);

            //  Update across network
            if (!this.isServer) {
                myPlayer.GetComponent<PlayerNetworking>().sendPlayerToServer(
                    currEditedPlayer.playerName,
                    currentIcon,
                    currEditedPlayer.playerID
                );
            }
            else {
                myPlayer.GetComponent<PlayerNetworking>().receivePlayerFromServer(
                    currEditedPlayer.playerName,
                    currentIcon,
                    currEditedPlayer.playerID
                );
            }

            //  Update the player text fields
            for (int i = 0; i < allPlayers.Count; ++i) {
                PlayerClass play = allPlayers[i];
                /*playerNameTextFields refers to the summaryPanel in the PlayerInput Variables. It updates each block of text to a player
                and its icon number*/
                playerNameTextFields[i].text =
                    "Player " + play.playerID + " " + play.playerName + " - " + iconNames[i];
            }

			/* Turn the panel off. Discard animation. And set the players to false */
			panelIconSelect.SetActive (false);
			animationPanel.discardPanel();
			for (int i = 0; i < playerIcons.Length; i++) {
                //  Move them out of view instead so they can still be found in script
                playerIcons[i].transform.position = new Vector3(0, -20, 0);
			}

			//turn on summaryPlayerInput Panel
			summaryPanel.SetActive (true);
		} 
		else 
		{
			//TODO handle error
			//	Prep and display the error screen
			Text err = errorPanel.transform.FindChild("ErrorText").GetComponent<Text>();
			err.text = "Error: Someone on the other computer has taken this icon.";
			
			errorPanel.SetActive(true);
		}

	}

	public void discardErrorScreen () {
		errorPanel.SetActive(false);
	}

    //  Update icons across network
    public void updatePlayerAcrossNetwork (string playerName, int iconIndex, int playerID) {
        //  Create and add the player
        PlayerClass newPlayer = new PlayerClass(playerName, playerID);
        newPlayer.playerIcon = Instantiate(playerIcons[iconIndex]);
		newPlayer.playerIconIndex = iconIndex;

        Debug.Log("Network creating player #" + playerID + " named " + playerName + " with icon " + newPlayer.playerIcon.name);

		//	Take out (Clone) from icon name
		string name = playerIcons[iconIndex].name;
		int cloneIndex = name.IndexOf("(Clone)");
		if (cloneIndex != -1) {
			name = name.Substring(0, cloneIndex) + name.Substring(cloneIndex + 7);
		}
        iconNames.Add(name);

        allPlayers.Add(newPlayer);

        //  Update the player text fields
        for (int i = 0; i < allPlayers.Count; ++i) {
            PlayerClass play = allPlayers[i];
            /*playerNameTextFields refers to the summaryPanel in the PlayerInput Variables. It updates each block of text to a player
            and its icon number*/
            playerNameTextFields[i].text =
                "Player " + play.playerID + " " + play.playerName + " - " + iconNames[i];
        }
    }
    
	public void donePlayerInput () {
		summaryPanel.SetActive (false);

		//	Re-enable player controls
        myPlayer.GetComponent<FirstPersonController>().enabled = true;
	}

	public void addPlayerInput () {
		playerIntNum++;
		if (playerIntNum <= 8) {
			summaryPanel.SetActive (false);
			//  Start up the panel animation
			animationPanel.beginAnimation (250, 275, 0.9f);
			//  Wait for animation to finish before displaying UI
			Invoke ("actuallyActivateNameInputPanel", animationPanel.animationTime);
		}	else {
				maxMessage.text = "Max Players Reached. Press done to continue";
				maxMessage.fontStyle = FontStyle.Bold;
				maxMessage.fontSize = 30;
				maxMessage.color = Color.red;
				maxMessage.alignment = TextAnchor.MiddleCenter;
				summaryPanel.SetActive (true);
				
				
				
			}




	}

    //  Function that can only be run on server
    [Server]
	public void ServerChooseRandomPlayers (bool displayIcons) {
        //  Don't do this if called by client but players have already been chosen
        if (playerOneClass != null && playerTwoClass != null) {
            return;
        }

        //  Check if all players have been chosen to play
        if (playersChosenToPlay.Count >= allPlayers.Count - 1) {
            //  Clear it out so they can be chosen again
            playersChosenToPlay.Clear();
        }

		/*assigns a random number to a index, assign corresponding text, and adds to list of players chosen*/
        int index = Random.Range(0, allPlayers.Count);
        //  Make sure the index hasn't been picked already
        while (playersChosenToPlay.Contains(index)) {
            index = Random.Range(0, allPlayers.Count);
        }

        playersChosenToPlay.Add(index);
	
		//  Set the player one class variable
        playerOneClass = allPlayers[index];

        int index2 = Random.Range(0, allPlayers.Count);
        //  Loop and reroll for as long as you got the same roll or one that's been picked already
        while (playersChosenToPlay.Contains(index2)) {
            index2 = Random.Range(0, allPlayers.Count);
        }

        playersChosenToPlay.Add(index2);

        //  Set the player two class variable
        playerTwoClass = allPlayers[index2];

        //  Must be separate function so this can be done from client side
        if (displayIcons) {
            //  Don't wanna display the client's icons, just server's
            displayRandomPlayers();
        }
	}

    //  Display the random players that have been chosen
    public void displayRandomPlayers () {
        if (playerOneClass == null && playerTwoClass == null) {
            Debug.LogError("Error: One of the players has not been set!");
            return;
        }

        //  Server is always player one
		if (this.isServer) {
            player1.text = playerOneClass.playerName;
            player2.text = "";

			//also updatename in score panel
			scoreName1.text = playerOneClass.playerName;
			scoreName2.text = playerTwoClass.playerName;

			Debug.Log("My player icon is " + playerOneClass.playerIcon.name);
			playerOneClass.playerIcon.transform.position =
                Camera.main.transform.position + Camera.main.transform.forward * .8f + Camera.main.transform.up * -.3f;
		}
        else {
            player2.text = playerTwoClass.playerName;
            player1.text = "";

			//updatename in score panel
			scoreName2.text = playerOneClass.playerName;
			scoreName1.text = playerTwoClass.playerName;

			Debug.Log("My player icon is " + playerTwoClass.playerIcon.name);
			playerTwoClass.playerIcon.transform.position =
                Camera.main.transform.position + Camera.main.transform.forward * .8f + Camera.main.transform.up * -.3f; 
		}
	}
	
	public void activateVotePanel () {
		voteManager.GetComponent<VoteManager>().openVotePanel();
	}

	public void sendAnswers(List<string> ansList) {
		//answers = new List<string> ();
		if (isServer) {
			for (int i = 0; i < ansList.Count; i++) {
				answerListP1 [i] = ansList [i];
			}
			
			p1pro1 = answerListP1 [0];
			p1pro2 = answerListP1 [1];
			p1pro3 = answerListP1 [2];
			p1con1 = answerListP1 [3];
			p1con2 = answerListP1 [4];
			p1con3 = answerListP1 [5];
			populateAnswerList (answerListP1);
			//updateClientAnswerList(answerListP1);
		} else {
		
			for (int i = 0; i < ansList.Count; i++) {
				answerListP2 [i] = ansList [i];
			}
				
			p2pro1 = answerListP2 [0];
			p2pro2 = answerListP2 [1];
			p2pro3 = answerListP2 [2];
			p2con1 = answerListP2 [3];
			p2con2 = answerListP2 [4];
			p2con3 = answerListP2 [5];

		
			updateClientAnswerList2 (answerListP2);
		}
		/*print (ansList[i]);
			if (this.isServer) {
			answers.Add(ansList[i]);
			}
			else {
				answers2.Add(ansList[i]);
			}
		}

*/
	}
	 public void populateAnswerList(string [] answerList) {

		
		p1pro1 = answerList[0];
		p1pro2 = answerList[1];
		p1pro3 = answerList[2];
		p1con1 = answerList[3];
		p1con2 = answerList[4];
		p1con3 = answerList[5];


	}
	public void checkAnswers() {

		if (this.isServer) {
			if (intentListPro1 == null || intentListPro1.Count == 0 || intentListCon1 == null || intentListCon1.Count == 0) {
				print ("NULL INTENTS");
			} else {
				//if we are on the servers side than we must scroll through the answers and increment score
				for (int i = 0; i < answerListP1.Length; i++) {
					
					if (intentListPro1.Contains (answerListP1 [i]) || intentListCon1.Contains (answerListP1 [i])) {
						score++;
						totalscore = totalscore +1;
						print ("SCORE IS " + score);
						print ("TotalSCORE IS " + totalscore);
					} else {
				
						wrongList.Add (answerListP1 [i] + "1");
						wrongListLong += answerListP1[i] + "1";
					}
				}  
			}
		} else { //if we are not on the servers side 
			if (intentListPro2 == null || intentListPro2.Count == 0 || intentListCon2 == null || intentListCon2.Count == 0) {
				print ("NULL INTENTS");
			} else {
				for (int i = 0; i < answerListP2.Length; i++) {//scroll through the answers and allow score to be 
					//updated
				
					if (intentListPro2.Contains (answerListP2 [i]) || intentListCon2.Contains (answerListP2 [i])) {
						score++;
						totalscore = totalscore + 1;
						myPlayer.GetComponent<PlayerNetworking> ().updateScore (score);
						myPlayer.GetComponent<PlayerNetworking> ().updateTotalScore (totalscore);
						print ("SCORE2 IS " + score);
						print ("TotalSCORE2 IS " + totalscore);
					} else {
							
						wrongList.Add (answerListP2 [i] + "2");
						wrongListLong += answerListP2[i] + "2";
					}
				}
			}
			myPlayer.GetComponent<PlayerNetworking>().updateWrongList(wrongListLong);
		}

		for (int i = 0; i < wrongList.Count; i++) {

			print ( "WRONG LIST" + i + " " + wrongList[i]);

		}
	}


	public void setWrongLong(string wronglist){
		wrongListLong = wronglist; 

	}


	public void displayScore() {
		Debug.Log (score + " is the score");
		if (this.isServer) {
			for (int i = 0; i < 6; i++) {
				print (answerListP1 [i] + " is first answer ");
				//if (!wrongList.Contains (answerListP1 [i] + "1")) {
					if (!wrongListLong.Contains (answerListP1 [i] + "1")) {
					//Color greenish = new Color(0.0,0.6,0.0,1.0);
					player1Answers [i].color = new Color ((float)0.0, (float)0.6, (float)0.0, (float)1.0);//set color to green because the answer is right
				}
				player1Answers [i].text = "-" + answerListP1 [i];

				if (i < 3) {

					answerKey1 [i].text = "-" + intentListPro1 [i];

				}
				if (i >= 3) {
					answerKey1 [i].text = "-" + intentListCon1 [i - 3];
				}
			
			}
			roundScore.text = "Round Score : " + score;
			
			totalScore.text = "Total Score : " + totalscore;
			

		} else {
			if (answerListP2.Length != 0) {
				Debug.Log (" the count for answers two is " + answerListP2.Length);
				
				Debug.Log ("Client Score is " + score);
				roundScore.text = "Round Score : " + score;
				
				totalScore.text = "Total Score : " + totalscore;	
				for (int i = 0; i < 6; i++) {
		
					print (answerListP2 [i] + " is first answer ");
					if (!wrongListLong.Contains (answerListP2 [i] + "2")) {
						//Color greenish = new Color(0.0,0.6,0.0,1.0);
						player1Answers [i].color = new Color ((float)0.0, (float)0.6, (float)0.0, (float)1.0); //set color to green because the answer is right
					}
					player1Answers [i].text = "-" + answerListP2 [i];

					if (i < 3) {
						Debug.Log (intentListPro2.Count + " is intent pros count" );
						Debug.Log ("it also contains " + intentListPro2[i]);
						answerKey1 [i].text = "-" + intentListPro2 [i];
					
					}
					if (i >= 3) {
						answerKey1 [i].text = "-" + intentListCon2 [i - 3];
					}

				}

			}	

		
		}
	
				
	}

	public void displayScore2() {
		ContinueButton.SetActive (false);
		finishRoundButton.SetActive (true);
		if (isServer) {
			scoreName1.text = playerTwoClass.playerName;
			intentScoreText.text = player2Intention;
			for (int i = 0; i < player1Answers.Length; i++ ) {
				player1Answers[i].color = Color.black;
				
			}
				roundScore.text = "Round Score : " + score;
				
				totalScore.text = "Total Score : " + totalscore;	

					

			if (!wrongListLong.Contains (p2pro1 + "2")) {
					
						player1Answers [0].color = new Color ((float)0.0, (float)0.6, (float)0.0, (float)1.0); //set color to green because the answer is right
					}
			if (!wrongListLong.Contains (p2pro2 + "2")) {
				
				player1Answers [1].color = new Color ((float)0.0, (float)0.6, (float)0.0, (float)1.0); //set color to green because the answer is right
			}
			if (!wrongListLong.Contains (p2pro3 + "2")) {
				
				player1Answers [2].color = new Color ((float)0.0, (float)0.6, (float)0.0, (float)1.0); //set color to green because the answer is right
			}
			if (!wrongListLong.Contains (p2con1 + "2")) {
				
				player1Answers [3].color = new Color ((float)0.0, (float)0.6, (float)0.0, (float)1.0); //set color to green because the answer is right
			}
			if (!wrongListLong.Contains (p2con2 + "2")) {
				
				player1Answers [4].color = new Color ((float)0.0, (float)0.6, (float)0.0, (float)1.0); //set color to green because the answer is right
			}
			if (!wrongListLong.Contains (p2con3 + "2")) {
				
				player1Answers [5].color = new Color ((float)0.0, (float)0.6, (float)0.0, (float)1.0); //set color to green because the answer is right
			}


				player1Answers [0].text = "-" + p2pro1;
				player1Answers [1].text = "-" + p2pro2;
				player1Answers [2].text = "-" + p2pro3;
				player1Answers [3].text = "-" + p2con1;
				player1Answers [4].text = "-" + p2con2;
				player1Answers [5].text = "-" + p2con3;




				answerKey1 [0].text = "-" + p2intpro1;
				answerKey1 [1].text = "-" + p2intpro2;
				answerKey1 [2].text = "-" + p2intpro3;
				answerKey1 [3].text = "-" + p2intcon1;
				answerKey1 [4].text = "-" + p2intcon2;
				answerKey1 [5].text = "-" + p2intcon3;
			}	
		else {
			scoreName1.text = playerOneClass.playerName;
			intentScoreText.text = player1Intention;
			roundScore.text = "Round Score : " + score;
			
			totalScore.text = "Total Score : " + totalscore;

			for (int i = 0; i < player1Answers.Length; i++ ) {
				player1Answers[i].color = Color.black;

			}

		
			if (!wrongListLong.Contains (p1pro1 + "1")) {

					player1Answers [0].color = new Color ((float)0.0, (float)0.6, (float)0.0, (float)1.0);//set color to green because the answer is right
			}
			
			if (!wrongListLong.Contains (p1pro2 + "1")) {
			
				player1Answers [1].color = new Color ((float)0.0, (float)0.6, (float)0.0, (float)1.0);//set color to green because the answer is right
			}

			if (!wrongListLong.Contains (p1pro3 + "1" )) {
				
				player1Answers [2].color = new Color ((float)0.0, (float)0.6, (float)0.0, (float)1.0);//set color to green because the answer is right
			}
			if (!wrongList.Contains (p1con1 + "1")) {
				
				player1Answers [3].color = new Color ((float)0.0, (float)0.6, (float)0.0, (float)1.0);//set color to green because the answer is right
			}
			if (!wrongListLong.Contains (p1con2 + "1")) {
				
				player1Answers [4].color = new Color ((float)0.0, (float)0.6, (float)0.0, (float)1.0);//set color to green because the answer is right
			}

			if (!wrongListLong.Contains (p1con3 + "1")) {
				
				player1Answers [5].color = new Color ((float)0.0, (float)0.6, (float)0.0, (float)1.0);//set color to green because the answer is right
			}
				player1Answers [0].text = "-" + p1pro1;
				player1Answers [1].text = "-" + p1pro2;
				player1Answers [2].text = "-" + p1pro3;
				player1Answers [3].text = "-" + p1con1;
				player1Answers [4].text = "-" + p1con2;
				player1Answers [5].text = "-" + p1con3;

		
			print ("Answer Key 1 " + intention1Long);
				string [] p1intentions = new string[6];
		
			char period = char.Parse (".");
			char[] periods = { period, period, period, period, period};
			//periods[0] = char.Parse(
			p1intentions = intention1Long.Split (periods);
			p1intpro1 = p1intentions [0];
			p1intpro2 = p1intentions [1];
			p1intpro3 = p1intentions [2];
			p1intcon1 = p1intentions [3];
			p1intcon2 = p1intentions [4];
			p1intcon3 = p1intentions [5];
					answerKey1 [0].text = "-" + p1intpro1;
					answerKey1 [1].text = "-" + p1intpro2;
					answerKey1 [2].text = "-" + p1intpro3;
					answerKey1 [3].text = "-" + p1intcon1;
					answerKey1 [4].text = "-" + p1intcon2;
					answerKey1 [5].text = "-" + p1intcon3;



				
			
		




		}



}

	public bool checkColor(string intention) {

	if(!wrongList.Contains (intention) ){
			return true;
		}
		else {
			return false;
		}



		



	}
	public void sendIntention(string[] intent){
		print ("IM SENDING INTENTION");
		if (this.isServer) {
			Debug.Log ("The server is sending the intention ");
			for (int i = 0; i < intent.Length; i++) {
				print (intent [i]);

				if (i >= 3) {
					intentListCon1.Add (intent [i]);
					intListCon1[i-3] = intent[i];


				}
				if (i < 3) {

					intentListPro1.Add (intent [i]);
					intListPro1[i] = intent [i];
				}

			}
			//intListPro1[0] = intentListPro1[0];
			//intListPro1[1] = intentListPro1[1];
			//intListPro1[2] = intentListPro1[2];
			//intListCon1[0] = intentListCon1[0];
			//intListCon1[1] = intentListCon1[1];
			//intListCon1[2] = intentListCon1[2];
			p1intpro1 = intListPro1[0];
			print (p1intpro1 + " PLAYER1PRO");
			p1intpro2 = intListPro1[1];
			p1intpro3 = intListPro1[2];
			p1intcon1 = intListCon1[0];
			p1intcon2 = intListCon1[1];
			p1intcon3 = intListCon1[2];
			intention1Long = p1intpro1 + p1intpro2 + p1intpro3 + p1intcon1 + p1intcon2 + p1intcon3;
			updateToPlayer2(intention1Long);



		}
		else {
			Debug.Log ("The client is sending the intention Mon");
			for (int i = 0; i < intent.Length; i++) {
				print (intent [i]);
				//intentions [i] = intent [i];
				
				if (i >= 3) {
					intentListCon2.Add (intent [i]);
					intListCon2[i-3] = intent[i];

				}
				if (i < 3) {
					
					intentListPro2.Add (intent [i]);
					intListPro2[i] = intent[i];
				}
				
			}

			//intListPro2[0] = intentListPro2[0];
			//intListPro2[1] = intentListPro2[1];
			//intListPro2[2] = intentListPro2[2];
			//intListCon2[0] = intentListCon2[0];
			//intListCon2[1] = intentListCon2[1];
			//intListCon2[2] = intentListCon2[2];
			p2intpro1 = intListPro2[0];
			Debug.Log (p2intpro1 + " PLAYER1PRO");
			p2intpro2 = intListPro2[1];
			p2intpro3 = intListPro2[2];
			p2intcon1 = intListCon2[0];
			p2intcon2 = intListCon2[1];
			p2intcon3 = intListCon2[2];

			intention2Long = p2intpro1 + p2intpro2 + p2intpro3 + p2intcon1 + p2intcon2 + p2intcon3;
			myPlayer.GetComponent<PlayerNetworking>().sendcurrentIntentionsToServer(intention2Long);
		}

	}

	public void updateToPlayer2(string intentionLong) {

		if (!isServer) {
			string [] p1intentions = new string[6];
			char period = char.Parse (".");
			char[] periods = { period, period, period, period, period};
			//periods[0] = char.Parse(
			p1intentions = intentionLong.Split (periods);
			p1intpro1 = p1intentions [0];
			p1intpro2 = p1intentions [1];
			p1intpro3 = p1intentions [2];
			p1intcon1 = p1intentions [3];
			p1intcon2 = p1intentions [4];
			p1intcon3 = p1intentions [5];
			//p1pro1

		}

	}
	public void updateScoreIntentText (string intent) {
				intent2 = intent;
		intentScoreText2.text = intent2;


	}
	public void updateTotalScore(int score) {

		totalscore = score;


	}
	public void updateClientAnswerList2(string[] clientAnswers){


		myPlayer.GetComponent<PlayerNetworking>().sendClientProsAndCons2(clientAnswers);


	}

	public void setClientAnswers2(string [] clientAnswers) {




		p2pro1 = clientAnswers [0];
		p2pro2 = clientAnswers [1];
		p2pro3 = clientAnswers [2];
		p2con1 = clientAnswers [3];
		p2con2 = clientAnswers [4];
		p2con3 = clientAnswers [5];




	}
	public void updateScoreToClient(int score) {

		this.score = score;
		if (isServer) {

			Debug.Log ("Score should have updated son " + score);
		}


	}
	
	public void updateIntentionNetworked(string intentionLong) {



			string [] p2intentions = new string[6];
			char period = char.Parse(".");
			char[] periods = { period, period, period, period, period};
			//periods[0] = char.Parse(
			p2intentions = intentionLong.Split(periods);
			p2intpro1 = p2intentions[0];
			p2intpro2 = p2intentions[1];
			p2intpro3 = p2intentions[2];
			p2intcon1 = p2intentions[3];
			p2intcon2 = p2intentions[4];
			p2intcon3 = p2intentions[5];




		
		
		
		
		
	}

	public void getIntention (int playerNumber) {
		if (previousSpunIntentions.Count == 5) {
			previousSpunIntentions.Clear ();
		}
		int intentionNumber = Random.Range (0, 4);
		while (previousSpunIntentions.Contains(intentionNumber)) {
			Debug.Log ("Rerolling cause same, got " + intentionsList[intentionNumber]);
			intentionNumber = Random.Range (0, 4);
		}
		if (playerNumber == 0) {
			player1Intention = intentionsList [intentionNumber];
		} else {
			player2Intention = intentionsList [intentionNumber];
		}
		intentionsWheel.GetComponent<IntentionsSpin> ().spinWheel (intentionNumber);
	}

	public void player2Spun () {
		myPlayer.GetComponent<PlayerNetworking> ().updatePlayer2Spin ();
	}

	public static void setScenario(int scenarioNumber) {
		currScenarioStatic = scenariosList [scenarioNumber];
		currScenarioTitleStatic = scenariosTitles [scenarioNumber];
		setRoles (scenarioNumber);
	}

	static private void setRoles(int scenarioNumber) {
		if(scenarioNumber == 0) {
			int selected = Random.Range(0,1);
			if(selected == 0) {
				player1RoleStatic = "Captain";
				player2RoleStatic = "Cadet";
			} else {
				player2RoleStatic = "Captain";
				player1RoleStatic = "Cadet";
			}
		} else if(scenarioNumber == 1) {
			int selected = Random.Range(0,1);
			if(selected == 0) {
				player1RoleStatic = "Lieutenant of Communications";
				player2RoleStatic = "Lieutenant of Navigation";
			} else {
				player2RoleStatic = "Lieutenant of Communications";
				player1RoleStatic = "Lieutenant of Navigation";
			}
		} else if(scenarioNumber == 2) {
			int selected = Random.Range(0,1);
			if(selected == 0) {
				player1RoleStatic = "Lieutenant Commander of Weapons";
				player2RoleStatic = "Ensign";
			} else {
				player2RoleStatic = "Lieutenant Commander of Weapons";
				player1RoleStatic = "Ensign";
			}
		} else if(scenarioNumber == 3) {
			int selected = Random.Range(0,1);
			if(selected == 0) {
				player1RoleStatic = "Captain";
				player2RoleStatic = "Chief Officer";
			} else {
				player2RoleStatic = "Captain";
				player1RoleStatic = "Chief Officer";
			}
		} else if(scenarioNumber == 4) {
			int selected = Random.Range(0,1);
			if(selected == 0) {
				player1RoleStatic = "Staff Officer of Communication";
				player2RoleStatic = "Staff Officer of Technology";
			} else {
				player2RoleStatic = "Staff Officer of Communication";
				player1RoleStatic = "Staff Officer of Technology";
			}
		} else if(scenarioNumber == 5) {
			int selected = Random.Range(0,1);
			if(selected == 0) {
				player1RoleStatic = "Physician's Assistant";
				player2RoleStatic = "Lead Nurse";
			} else {
				player2RoleStatic = "Physician's Assistant";
				player1RoleStatic = "Lead Nurse";
			}
		} else if(scenarioNumber == 6) {
			int selected = Random.Range(0,1);
			if(selected == 0) {
				player1RoleStatic = "Chief Officer";
				player2RoleStatic = "Captain";
			} else {
				player2RoleStatic = "Chief Officer";
				player1RoleStatic = "Captain";
			}
		} else {
			int selected = Random.Range(0,1);
			if(selected == 0) {
				player1RoleStatic = "Fleet Commander";
				player2RoleStatic = "Captain";
			} else {
				player2RoleStatic = "Fleet Commander";
				player1RoleStatic = "Captain";
			}
		}
	}

	private void instantiateVariables() {
		justResetGame = false;
		player1RoleStatic = "";
		player2RoleStatic = "";
		currScenarioStatic = "";
		currScenarioTitleStatic = "";
		numPlayersHitCheckpoint = 0;
		checkpointCleared = false;
		intentionsList = new string[]{"Competing","Compromising","Avoiding","Accomodating","Collaborating"};
		allowP1IntentionSpin = false;
		allowP1IntentionSpin = false;
		allowScenarioSpin = false;
		previousSpunIntentions = new List<int> ();
	}
		
		private void instantiateScenarios() {
		scenariosList = new string[] {
			"Two weeks ago, the Captain asked the Cadet to prepare a report on the number of enemy invasions. The Manager plans to share the report at the quarterly intergalactic meeting. The Cadet procrastinates doing the work and plans to complete the report the morning of the meeting. When the Cadet reports to work, there is a system failure with the computer and the system will be down all day for repairs. When the Captain asks for the report for the meeting that will occur in an hour, the Cadet does not have the report to give to the Captain. What’s the Captain to do? What is the Cadet’s response when the Captain comes to ask for the report?", 
			"The new Lieutenant of Communications instructs the Lieutenant of Navigation to inform the staff of the new meeting location change from conference room to the flight deck. The Lieutenant of Navigation did not share the information with everyone. Some people show up at the conference room for the meeting and some show up on the flight deck. After the initial confusion, the meeting occurs on the flight deck. This isn’t the first time the Lieutenant of Navigation has made this mistake. After the meeting, the Lieutenant of Communications schedules a meeting with the Lieutenant of Navigation to resolve the miscommunication issue. What will the Lieutenant of Communications say? How will the Lieutenant of Navigation respond?", 
			"The Lieutenant Commander of Weapons in charge of the new satellite design and deployment project is working with the Ensign from Weapons who has the expertise on satellite design but no experience in the field of satellite deployment. The Ensign constantly challenges the Lieutenant Commander’s design plans. The Lieutenant Commander has been patient up to a point, but at the last meeting, erupts out of anger and frustration and shuts down the Ensign in front of the whole team. The Ensign sits in silence and quickly leaves at the end of the meeting. The Lieutenant Commander soon realizes the Ensign’s expertise is needed for the project. How will the Lieutenant Commander resolve the situation? What is the best way for the Ensign to respond to the Lieutenant Commander in light of the Lieutenant Commander actions at the meeting?", 
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
	
	public void updateServerSpin() {
		allowP1IntentionSpin = true;
		allowScenarioSpin = true;
	}
	
	public void updateClientSpin() {
		allowP2IntentionSpin = true;
	}

	public static string getScenarioTitle() {
		return currScenarioTitleStatic;
	}
	
	public static string getScenario() {
		return currScenarioStatic;
	}

	public string getPlayer2Intention() {
		return player2Intention;
	}

	private void updateSyncedPlayerVariables() {
		if (this.isServer) {
			player1Role = player1RoleStatic;
			player2Role = player2RoleStatic;
			currScenario = currScenarioStatic;
			currScenarioTitle = currScenarioTitleStatic;
		} 
	}

	//private void updatePlayer2 
    //  These are for loading new rounds
    public void whiteFadeOut () {
        fadeScene.whiteFadeOut();
    }
    public void whiteFadeIn () {
        fadeScene.fadeIn();
    }
    public void movePlayerToStart () {
        GameObject spawnLoc = GameObject.FindGameObjectWithTag("PlayerSpawn");
        myPlayer.transform.position = spawnLoc.transform.position;
		myPlayer.transform.rotation = spawnLoc.transform.rotation;
    }
    public void reactivatePlayerControls () {
        myPlayer.GetComponent<FirstPersonController>().enabled = true;
    }

	public void resetVars() {
		pscript.resetVars ();
		justResetGame = true;
		currCheckpoint = 1;
		//reset round score 
		score = 0;
		ContinueButton.SetActive (true);
		wrongListLong = "";
		for (int i  = 0; i < answers.Count; i++) {
			answers.Remove (answers[i]);
			answers2.Remove (answers2[i]);
		}
		//clear the procons list
		intentListPro1.Clear ();
		intentListPro2.Clear ();
		intentListCon1.Clear ();
		intentListCon2.Clear ();
		wrongList.Clear ();
		/*for (int i  = 0; i < intentListPro1.Count; i++) {
			intentListPro1.Remove (intentListPro1[i]);
			intentListCon1.Remove (intentListCon1[i]);
			intentListPro2.Remove (intentListPro2[i]);
			intentListCon2.Remove (intentListCon2[i]);
		}
		*/
		/*for (int i = 0; i < wrongList.Count; i++) {
			wrongList.Remove (wrongList[i]);
		}*/
		//	Move away the icons at the players
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player")) {
			Transform child = go.transform.GetChild(1);
			child.SetParent(null);
			child.localScale = new Vector3(0.2f, 0.2f, 0.2f);	//	Reset its size
			child.gameObject.GetComponent<RotateSlowly>().enabled = true;    //  Start the rotating script

			//	Reset layers
			child.gameObject.layer = LayerMask.NameToLayer("Icons");
			this.fullChangeLayer(child, "Icons");

			child.position = new Vector3(0, 20, 0);
		}

		playerOneClass = null;
		playerTwoClass = null;

		for (int i = 0; i < player1Answers.Length; i++ ) {
			player1Answers[i].color = Color.black;
			
		}

	}

	public void bothPlayersHit() {
		checkpointCleared = true;
		deactivateWaitingForOtherPlayerPanel ();
		CancelInvoke ();
	}

	private void resetPlayersHit() {
		if (this.isServer) {
			numPlayersHitCheckpoint = 0;
			Debug.Log("RESETTING TO 0");
			checkpointCleared = false;
		} else {
			myPlayer.GetComponent<PlayerNetworking> ().updateBothPlayersHit ();
		}
	}

	private void waitingToContinue() {
		if (numPlayersHitCheckpoint >= 2) {
			bothPlayersHit();
			if(this.isServer) {
				justResetGame = true;
			} else {
				myPlayer.GetComponent<PlayerNetworking>().updateJustResetGame(true);
			}
			myPlayer.GetComponent<FirstPersonController>().enabled = true;
		}
	}

	private void waitingToChoosePlayer() {
		if (numPlayersHitCheckpoint >= 2) {
			bothPlayersHit();
			this.activateChoosePlayerPanel ();
		}
	}

	private void waitingToRecap() {
		if (numPlayersHitCheckpoint >= 2) {
			bothPlayersHit();
			this.activateRecapPanel ();
		}
	}

	private void waitingForTimer() {
		if (numPlayersHitCheckpoint >= 2) {
			bothPlayersHit();
			timerMenu.enabled = true;
			countDownTimer.StartTimer ();
			myPlayer.GetComponent<FirstPersonController>().enabled = false;
		}
	}

	private void waitingForScore() {
		if (numPlayersHitCheckpoint >= 2) {
			bothPlayersHit();
			this.activateScorePanel();
			finishRoundButton.SetActive(false);
		}
	}

	private void waitingToResetGame() {
		if (numPlayersHitCheckpoint >= 2) {
			bothPlayersHit();
			myPlayer.GetComponent<FirstPersonController>().enabled = false;
			this.activateReset();
		}
	}

	private void activateReset() {
		Invoke ("whiteFadeOut", 2.5f);
		Invoke ("movePlayerToStart", 6);
		Invoke ("whiteFadeIn", 6.5f);
		Invoke ("reactivatePlayerControls", 8);
		resetVars ();
	}

    public void checkpointHit(Vector3 checkpointPos) {
        //  Call appropriate function
		if (currCheckpoint == 0) {
            this.activateNameInputPanel ();
		} 

		if (currCheckpoint == 1) {
			if(justResetGame == true) {
				resetPlayersHit();
				if(this.isServer) {
					justResetGame = false;
				} else {
					myPlayer.GetComponent<PlayerNetworking>().updateJustResetGame(false);
				}
			}
		}
		
		if (currCheckpoint == 2) {
			if(this.isServer) {
				numPlayersHitCheckpoint++;
			} else {
				myPlayer.GetComponent<PlayerNetworking> ().updatePlayer2Hit ();
			}
			if(checkpointCleared) {
				this.activateChoosePlayerPanel ();
			} else {
				activateWaitingForOtherPlayerPanel();
				InvokeRepeating ("waitingToChoosePlayer", 0.5f, 0.5f);
			}
		}

		if (currCheckpoint == 3) {
			if(checkpointCleared) {
				resetPlayersHit();
			}
		}

		if (currCheckpoint == 5) {
			if (this.isServer) {
				updateServerSpin ();
			} else {
				updateClientSpin ();
			}
		}

		if (currCheckpoint == 6) {
            if (!hasSeenScenarioGuide)
            {
                activateScenarioGuidePanel();
                hasSeenScenarioGuide = true;
            }
		}

        if (currCheckpoint == 7)
        {
            if (!hasSeenIntentionGuide)
            {
                activateIntentionGuidePanel();
                hasSeenIntentionGuide = true;
            }
        }

		if (currCheckpoint == 8) {
			if(allowP1IntentionSpin || (allowP2IntentionSpin && !this.isServer)) {
				if(this.isServer) {
					currCheckpoint = currCheckpoint-3;
				} else {
					currCheckpoint = currCheckpoint-2;
				}
				activateSpinWheelPanel();
			}
			updateSyncedPlayerVariables();
		}

		if (currCheckpoint == 13) {
			if(this.isServer) {
				numPlayersHitCheckpoint++;
			} else {
				myPlayer.GetComponent<PlayerNetworking> ().updatePlayer2Hit ();
			}
			if(checkpointCleared) {
				this.activateRecapPanel();
			} else {
				activateWaitingForOtherPlayerPanel();
				InvokeRepeating ("waitingToRecap", 0.5f, 0.5f);
			}
		}

		if (currCheckpoint == 14) {
			if(checkpointCleared) {
				resetPlayersHit();
			}
		}

		if (currCheckpoint == 15) {
			//Planning stuff
			if(this.isServer) {
				numPlayersHitCheckpoint++;
			} else {
				myPlayer.GetComponent<PlayerNetworking> ().updatePlayer2Hit ();
			}
			if(checkpointCleared) {
				timerMenu.enabled = true;
				countDownTimer.StartTimer ();
				myPlayer.GetComponent<FirstPersonController>().enabled = false;
			} else {
				activateWaitingForOtherPlayerPanel();
				InvokeRepeating ("waitingForTimer", 0.1f, 0.1f);
			}
		}

		if (currCheckpoint == 16) {
			if(checkpointCleared) {
				resetPlayersHit();
			}
			if(this.isServer) {
				this.activateVotePanel();
			}
		}

		if (currCheckpoint == 19) {
			this.activateProConPanel();
		}
		if (currCheckpoint == 20) {
			checkAnswers ();
			
		}
		if (currCheckpoint == 24) {
			if(this.isServer) {
				numPlayersHitCheckpoint++;
			} else {
				myPlayer.GetComponent<PlayerNetworking> ().updatePlayer2Hit ();
			}
			if(checkpointCleared) {
				this.activateScorePanel();
			} else {
				activateWaitingForOtherPlayerPanel();
				InvokeRepeating ("waitingForScore", 0.5f, 0.5f);
			}
		}

		if (currCheckpoint == 25) {
			if(checkpointCleared) {
				resetPlayersHit();
			}
		}

		if (currCheckpoint == 26 && !this.isServer) {
			currCheckpoint++;
		}

        if (currCheckpoint == 27 && this.isServer) {
			numPlayersHitCheckpoint++;
			currCheckpoint = 0;
			Debug.Log("Incrementing to " + numPlayersHitCheckpoint);
			myPlayer.GetComponent<FirstPersonController> ().enabled = false;
			myPlayer.GetComponent<AnimateRotateCamera> ().beginRotation (
				Quaternion.LookRotation (Vector3.left),
				2,
				checkpointPos
			);
			if(checkpointCleared) {
				this.activateReset();
			} else {
				activateWaitingForOtherPlayerPanel();
				InvokeRepeating ("waitingToResetGame", 0.5f, 0.5f);
			}
        }

		if (currCheckpoint == 28) {
			currCheckpoint = 0;
			myPlayer.GetComponent<FirstPersonController> ().enabled = false;
			myPlayer.GetComponent<AnimateRotateCamera> ().beginRotation (
				Quaternion.LookRotation (Vector3.left),
				2,
				checkpointPos
			);
			myPlayer.GetComponent<PlayerNetworking> ().updatePlayer2Hit ();
			if(checkpointCleared) {
				this.activateReset();
			} else {
				activateWaitingForOtherPlayerPanel();
				InvokeRepeating ("waitingToResetGame", 0.5f, 0.5f);
			}
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

    private void onUpdateID (int update) {
        string which = this.isServer ? "server" : "client";
        Debug.Log("On " + which + ". Updating ID from " + currAvailableID + " to " + update);
        currAvailableID = update;
    }

    //  Utility function for recursively changing a GameObject's layer
    public static void fullChangeLayer (Transform obj, string layer) {
        foreach (Transform child in obj) {
            child.gameObject.layer = LayerMask.NameToLayer(layer);
            fullChangeLayer(child, layer);
        }
    }

	public void ExitGame() {
		print ("Quitting Game");
		Application.Quit();


	}
}
