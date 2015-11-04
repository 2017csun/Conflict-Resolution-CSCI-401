using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class MenuScript : MonoBehaviour {

    public GameObject networkManagerPrefab;
    NetworkMatcher networkMatcher;
	public Text gameTitleText;
	public Text keyText;
	public InputField keyInputField;
	public Button exitButton;
	public Button helpButton;
	public Button hostGameButton;
	public Button joinGameButton;
	public Button joinButton;
	public Button startButton;
	public Button hostGameCancelButton;
	public Button joinGameCancelButton;

	public Canvas hostGameMenu;
	public Canvas joinGameMenu;
	// Use this for initialization
	void Start () {

		// networkmanager
        networkMatcher = networkManagerPrefab.GetComponent<NetworkMatcher>();

		// game title
		gameTitleText = gameTitleText.GetComponent<Text> ();

		// host game
		hostGameButton = hostGameButton.GetComponent<Button> ();
		hostGameCancelButton = hostGameCancelButton.GetComponent<Button> ();
		hostGameMenu = hostGameMenu.GetComponent<Canvas> ();
		keyText = keyText.GetComponent<Text> ();
		hostGameMenu.enabled = false;

		startButton = startButton.GetComponent<Button> ();

		// join game
		joinGameButton = joinGameButton.GetComponent<Button> ();
		joinGameCancelButton = joinGameCancelButton.GetComponent<Button> ();
		joinGameMenu = joinGameMenu.GetComponent<Canvas> ();
		keyInputField = keyInputField.GetComponent<InputField> ();
		joinGameMenu.enabled = false;

		joinButton = joinButton.GetComponent<Button> ();


		// help and exit
		helpButton = helpButton.GetComponent<Button> ();
		exitButton = exitButton.GetComponent<Button> ();

	}
	
	public void ExitPressed() {
		Application.Quit ();
	}

	public void CancelPressed() {
		hostGameMenu.enabled = false;
		joinGameMenu.enabled = false;
		hostGameButton.enabled = true;
		joinGameButton.enabled = true;
		helpButton.enabled = true;
		exitButton.enabled = true;
	}
	
	public void HelpPressed() {
		/*
		button1Button.enabled = false;
		button2Button.enabled = false;
		helpButton.enabled = false;
		*/
	}
	
	public void HostGamePressed() {

		// handle GUI
		hostGameMenu.enabled = true;
		hostGameButton.enabled = false;
		joinGameButton.enabled = false;
		helpButton.enabled = false;
		exitButton.enabled = false;

		// handle network
        string key = networkMatcher.generateMatchKey();
		keyText.text = key;

	}

	public void ReenableButtons() {
		joinGameMenu.enabled = true;
		hostGameButton.enabled = false;
		joinGameButton.enabled = false;
		helpButton.enabled = false;
		exitButton.enabled = false;
		joinButton.enabled = true;
		joinGameCancelButton.enabled = true;
	}
	
	public void JoinGamePressed() {

		// handle GUI
		hostGameButton.enabled = false;
		joinGameButton.enabled = false;
		joinGameMenu.enabled = true;
		helpButton.enabled = false;
		exitButton.enabled = false;

		// handle networking

	}

	public void StartPressed() {
		hostGameMenu.enabled = true;
		hostGameButton.enabled = false;
		joinGameButton.enabled = false;
		helpButton.enabled = false;
		exitButton.enabled = false;
		startButton.enabled = false;
		hostGameCancelButton.enabled = false;

	}

	public void JoinPressed() {
		joinGameMenu.enabled = true;
		hostGameButton.enabled = false;
		joinGameButton.enabled = false;
		helpButton.enabled = false;
		exitButton.enabled = false;
		joinButton.enabled = false;
		joinGameCancelButton.enabled = false;
		
		string inputKey = keyInputField.text;
		networkMatcher.connectToServer(inputKey);
	}


}
