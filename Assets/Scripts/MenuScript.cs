using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class MenuScript : MonoBehaviour {

    public GameObject networkManagerPrefab;
    NetworkMatcher networkMatcher;
	public Text keyText;
	public Text joinLoadText;
	public InputField keyInputField;
	public Button exitButton;
	public Button helpButton;
	public Button hostGameButton;
	public Button joinGameButton;
	public Button joinButton;
	public Button startButton;
	public Button hostGameCancelButton;
	public Button joinGameCancelButton;
	public Button joinFailOKButton;

	public Canvas helpMenu;
	public Canvas hostGameMenu;
	public Canvas joinGameMenu;
	public Canvas joinLoadMenu;
	// Use this for initialization
	void Start () {

		// networkmanager
        networkMatcher = networkManagerPrefab.GetComponent<NetworkMatcher>();

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

		
		joinLoadMenu = joinLoadMenu.GetComponent<Canvas> ();
		joinLoadMenu.enabled = false;
		joinFailOKButton = joinFailOKButton.GetComponent<Button> ();
		joinFailOKButton.enabled = false;
		joinFailOKButton.gameObject.SetActive (false);
		joinLoadText = joinLoadText.GetComponent<Text> ();


		// help
		helpButton = helpButton.GetComponent<Button> ();
		helpMenu = helpMenu.GetComponent<Canvas> ();
		helpMenu.enabled = false;

		// exit
		exitButton = exitButton.GetComponent<Button> ();

	}
	
	public void ExitPressed() {
		Application.Quit ();
	}

	public void CancelPressed() {
		hostGameMenu.enabled = false;
		joinGameMenu.enabled = false;
		helpMenu.enabled = false;
		hostGameButton.enabled = true;
		joinGameButton.enabled = true;
		helpButton.enabled = true;
		exitButton.enabled = true;
	}
	
	public void HelpPressed() {
		hostGameButton.enabled = false;
		joinGameButton.enabled = false;
		helpButton.enabled = false;
		exitButton.enabled = false;
		helpMenu.enabled = true;
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
		joinLoadMenu.enabled = false;
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
		hostGameButton.enabled = false;
		joinGameButton.enabled = false;
		helpButton.enabled = false;
		exitButton.enabled = false;
		joinButton.enabled = false;
		joinGameCancelButton.enabled = false;

		joinLoadMenu.enabled = true;
		joinGameMenu.enabled = false;
		
		string inputKey = keyInputField.text;
		networkMatcher.connectToServer(inputKey);
	}

	public void JoinFailed() {
		joinLoadText.text = "Joining failed!";
		joinFailOKButton.gameObject.SetActive (true);
		joinFailOKButton.enabled = true;
	}

    public void MultipleMatchJoinFailed()
    {
        joinLoadText.text = "Multiple Match!";
        joinFailOKButton.gameObject.SetActive(true);
        joinFailOKButton.enabled = true;
    }


}
