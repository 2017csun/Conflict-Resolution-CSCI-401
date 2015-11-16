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
    public List<PlayerClass> allPlayers;
    [SyncVar(hook = "onUpdateID")]
    [HideInInspector]
    public int currAvailableID = 1;
    private PlayerClass currEditedPlayer;   //  The player currently being modified from user input


	//---------------------------------------------
	// Gui Guide Variables
	//---------------------------------------------
	public GameObject welcomePanel;

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
	public static bool allowP1IntentionSpin;
	public static bool allowP2IntentionSpin;
	public static bool allowScenarioSpin;
	private static string[] scenariosList;
	private static string[] scenariosTitles;
	private static string[] intentionsList;
	

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
	private static string player1IntentionStatic;
	private static string player2IntentionStatic;
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
	public GameObject proConPanel;
	//public List<string> intentList;
	public List<string> intentListPro;
	public List<string> intentListCon;

	[Header("Score Variables")]
	public Text[] player1Answers;
	public Text[] player2Answers;
	public Text[] answerKey1;
	public Text[] answerKey2;
	public Text roundScore;
	public Text totalScore;
	public Text intentScoreText;
	public Text intentScoreText2;
	public Text scoreName1;
	public Text scoreName2;
	[SyncVar]
	public int score;
	public int totalscore;
	public GameObject scorePanel;
	public ProsAndConsList pscript;
	private List<string> wrongList;
	public GameObject Player1ProCon; // text containing
	public GameObject Player2ProCon;

	void Start () {
		currCheckpoint = 0;

        //  Add all checkpoints
        allCheckpoints = new List<Transform>();
        foreach (Transform child in checkpointLocations.transform) {
            allCheckpoints.Add(child);
        }

		roundNumber = 0;
		currentIcon = 0;
        allPlayers = new List<PlayerClass>();
		playerNames = new List<string> ();
		iconNames = new List<string> ();
        playersChosenToPlay = new List<int>();
		intentListPro = new List<string> ();
		intentListCon = new List<string> ();
		wrongList = new List<string> ();
		//intentList = new List<string> ();
		answers = new List<string> ();
		answers2 = new List<string> ();
		instantiateVariables ();
		instantiateScenarios ();

		timerMenu = timerMenu.GetComponent<Canvas> ();
		timerMenu.enabled = false;

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
	public void deactivateWelcomePanel() {

		welcomePanel.SetActive (false);


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
			intentScoreText2.text = player2Intention;
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

	public void activateScorePanel() {
		
		
		scorePanel.SetActive (true);
		Player1ProCon.SetActive (true);
		checkAnswers ();
		displayScore (); 
		myPlayer.GetComponent<FirstPersonController>().enabled = false;
		
		
	}
	public void activateSecondScorePanel() {
		Player1ProCon.SetActive (false);
		Player2ProCon.SetActive (true);


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
		summaryPanel.SetActive (false);
        //  Start up the panel animation
        animationPanel.beginAnimation(250, 275, 0.9f);
        //  Wait for animation to finish before displaying UI
        Invoke("actuallyActivateNameInputPanel", animationPanel.animationTime);
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
			playerOneClass.playerIcon.transform.position =
                Camera.main.transform.position + Camera.main.transform.forward * .8f + Camera.main.transform.up * -.3f;
		}
        else {
            player2.text = playerTwoClass.playerName;
            player1.text = "";

			//updatename in score panel
			scoreName2.text = playerOneClass.playerName;
			scoreName1.text = playerTwoClass.playerName;

			playerTwoClass.playerIcon.transform.position =
                Camera.main.transform.position + Camera.main.transform.forward * .8f + Camera.main.transform.up * -.3f; 
		}
	}
	
	public void activateVotePanel () {
		voteManager.GetComponent<VoteManager>().openVotePanel();
	}

	public void sendAnswers(List<string> ansList) {
		//answers = new List<string> ();

		for (int i = 0; i < ansList.Count; i++) {
			print (ansList[i]);
			if (this.isServer) {
			answers.Add(ansList[i]);
			}
			else {
				answers2.Add(ansList[i]);
			}
		}



	}
	public void checkAnswers() {

		if (intentListPro == null || intentListPro.Count == 0 || intentListCon == null || intentListCon.Count == 0 ) {

			print ("NULL INTENTS");

		}  else {
			if (this.isServer) {
				//if we are on the servers side than we must scroll through the answers and increment score
				for (int i = 0; i < answers.Count; i++) {
					
					if (intentListPro.Contains (answers [i]) || intentListCon.Contains(answers[i])){
						score++;

						print ("SCORE IS " + score);
					}
					else {
				
						wrongList.Add(answers[i]);
					}
				}  
			}else { //if we are not on the servers side 

				for (int i = 0; i < answers2.Count; i++) {//scroll through the answers and allow score to be 
					//updated
				
					if (intentListPro.Contains (answers2 [i]) || intentListCon.Contains(answers2[i])) {
						score++;
						myPlayer.GetComponent<PlayerNetworking>().updateScore(score);
						print ("SCORE2 IS " + score);
					}
						else {
							
							wrongList.Add(answers2[i]);
						}
				}
			}
		
			}



	}


	public void displayScore() {
		Debug.Log (score + " is the score");
		if (this.isServer) {
			for (int i = 0; i < 6; i++) {
				print (answers [i] + " is first answer ");
				if (!wrongList.Contains (answers [i])) {
					//Color greenish = new Color(0.0,0.6,0.0,1.0);
					player1Answers [i].color = new Color ((float)0.0, (float)0.6, (float)0.0, (float)1.0);//set color to green because the answer is right
				}
				player1Answers [i].text = "-" + answers [i];

				if (i < 3) {

					answerKey1 [i].text = "-" + intentListPro [i];

				}
				if (i >= 3) {
					answerKey1 [i].text = "-" + intentListCon [i - 3];
				}
			
			}
			roundScore.text = "Round Score : " + score;
			
			totalScore.text = "Total Score : " + score;
			

		} else {
			if (answers2.Count != 0) {
				Debug.Log (" the count for answers two is " + answers2.Count);

				for (int i = 0; i < 6; i++) {
		
					print (answers2 [i] + " is first answer ");
					if (!wrongList.Contains (answers2 [i])) {
						//Color greenish = new Color(0.0,0.6,0.0,1.0);
						player1Answers [i].color = new Color ((float)0.0, (float)0.6, (float)0.0, (float)1.0); //set color to green because the answer is right
					}
					player1Answers [i].text = "-" + answers2 [i];

					if (i < 3) {
						Debug.Log (intentListPro.Count + " is intent pros count" );
						Debug.Log ("it also contains " + intentListPro[i + 3]);
						answerKey1 [i].text = "-" + intentListPro [i + 3];
					
					}
					if (i >= 3) {
						answerKey1 [i].text = "-" + intentListCon [i];
					}

				}

			}	
			roundScore.text = "Round Score : " + score;
			
			totalScore.text = "Total Score : " + score;	
		
		}
	
				
	}

	public void displayScore2() {
		//print ("IM UP IN DISPLAY SCORE 2");
		//if (answers2 != null || answers2.Count > 0) {
			print ("Displaying score 2");

				for (int i = 0; i < 6; i++) {
			
					print (answers2 [i] + " is first answer ");
					if (wrongList.Contains (answers2 [i])) {
						player1Answers [i].color = Color.red;
					}
					player1Answers [i].text = "-" + answers2 [i];
					if (i >= 3) {
						answerKey1 [i].text = "-" + intentListCon [i];
					
					}
					if (i < 3) {
						answerKey1 [i].text = "-" + intentListPro [i + 3];
					}
				
				}

			
			
				for (int i = 0; i < 6; i++) {
					print (answers [i] + " is first answer ");
					if (wrongList.Contains (answers [i])) {
						player1Answers [i].color = Color.red;
					}
					player1Answers [i].text = "-" + answers [i];
					if (i >= 3) {
						answerKey1 [i].text = "-" + intentListCon [i - 3];
					
					}
					if (i < 3) {
						answerKey1 [i].text = "-" + intentListPro [i];
					}
				
				}
			

			roundScore.text = "Round Score : " + score;
		
			totalScore.text = "Total Score : " + score;


		//}



}
	public void sendIntention(string[] intent){
		print ("IM SENDING INTENTION");
		//intentions = new string[6];
		//intentList = new List<string> ();

			for (int i = 0; i < intent.Length; i++) {
			print (intent [i]);
			//intentions [i] = intent [i];

			if (i >= 3) {
				intentListCon.Add (intent [i]);
			}
			if (i < 3) {

				intentListPro.Add (intent [i]);
			}

		}
		//print (intentList.Count + " is how big the list is");
	}


	public void updateScoreToClient(int score) {

		this.score = score;


	}

	public static void setIntention(int playerNumber, int intentionNumber) {
		if (playerNumber == 0) {
			player1IntentionStatic = intentionsList [intentionNumber];
		} else {
			player2IntentionStatic = intentionsList [intentionNumber];
		}
	}

	public void syncPlayer2Intention(string intention) {
		player2Intention = intention;
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
		player1IntentionStatic = "";
		player2IntentionStatic = "";
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
	
	public static void updateServerSpin() {
		allowP1IntentionSpin = true;
		allowScenarioSpin = true;
	}
	
	public static void updateClientSpin() {
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
			player1Intention = player1IntentionStatic;
			player1Role = player1RoleStatic;
			player2Role = player2RoleStatic;
			currScenario = currScenarioStatic;
			currScenarioTitle = currScenarioTitleStatic;
		} else {
			player2Intention = player2IntentionStatic;
			myPlayer.GetComponent<PlayerNetworking> ().sendPlayer2Intention ();
		}
	}

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
		resetPlayersHit ();
		currCheckpoint = 1;

	}

	private void bothPlayersHit() {
		checkpointCleared = true;
		deactivateWaitingForOtherPlayerPanel ();
		CancelInvoke ();
	}

	private void resetPlayersHit() {
		if (this.isServer) {
			numPlayersHitCheckpoint = 0;
			checkpointCleared = false;
		} else {
			myPlayer.GetComponent<PlayerNetworking> ().updateBothPlayersHit ();
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
		}
	}

	private void waitingToResetGame() {
		if (numPlayersHitCheckpoint >= 2) {
			bothPlayersHit();
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

		if (currCheckpoint == 6) {
			if(this.isServer){
				updateServerSpin();
			} else {
				updateClientSpin();
//				Skip checkpoint for client
			}
		}

		if (currCheckpoint == 8) {
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
}
