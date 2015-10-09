using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour {

	public Button exitButton;
	public Button helpButton;
	public Button hostGameButton;
	public Button joinGameButton;

	public Canvas hostGameMenu;
	public Canvas joinGameMenu;
	// Use this for initialization
	void Start () {

		// host game
		hostGameButton = hostGameButton.GetComponent<Button> ();
		hostGameMenu = hostGameMenu.GetComponent<Canvas> ();
		hostGameMenu.enabled = false;

		// join game
		joinGameButton = joinGameButton.GetComponent<Button> ();
		joinGameMenu = joinGameMenu.GetComponent<Canvas> ();
		joinGameMenu.enabled = false;


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
		hostGameMenu.enabled = true;
		hostGameButton.enabled = false;
		joinGameButton.enabled = false;
		helpButton.enabled = false;
		exitButton.enabled = false;

		/*
		exitButton.enabled = false;
		button2Button.enabled = false;
		helpButton.enabled = false;
		*/
	}
	
	public void JoinGamePressed() {
		hostGameButton.enabled = false;
		joinGameButton.enabled = false;
		joinGameMenu.enabled = true;
		helpButton.enabled = false;
		exitButton.enabled = false;
		/*
		button1Button.enabled = false;
		exitButton.enabled = false;
		helpButton.enabled = false;
		*/
	}
}
