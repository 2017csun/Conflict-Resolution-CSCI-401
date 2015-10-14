﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class MenuScript : MonoBehaviour {

    public GameObject networkManagerPrefab;
    NetworkMatcher networkMatcher;
	public Text gameTitleText;
	public Text keyText;
	public Button exitButton;
	public Button helpButton;
	public Button hostGameButton;
	public Button joinGameButton;

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
		hostGameMenu = hostGameMenu.GetComponent<Canvas> ();
		keyText = keyText.GetComponent<Text> ();
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

	}

	public void JoinPressed() {

	}
}
