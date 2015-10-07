using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour {
	
	public Button exitButton;
	public Button helpButton;
	public Button button1Button;
	public Button button2Button;
	// Use this for initialization
	void Start () {
		button1Button = button1Button.GetComponent<Button> ();
		button2Button = button2Button.GetComponent<Button> ();
		helpButton = helpButton.GetComponent<Button> ();
		exitButton = exitButton.GetComponent<Button> ();
	}
	
	public void ExitPressed() {
		Application.Quit ();
	}
	
	public void HelpPressed() {
		/*
		button1Button.enabled = false;
		button2Button.enabled = false;
		helpButton.enabled = false;
		*/
	}
	
	public void Button1Pressed() {
        Application.LoadLevel(2);
		/*
		exitButton.enabled = false;
		button2Button.enabled = false;
		helpButton.enabled = false;
		*/
	}
	
	public void Button2Pressed() {
		button2Button.GetComponentInChildren<Text> ().text = "SPACESHIP";
		/*
		button1Button.enabled = false;
		exitButton.enabled = false;
		helpButton.enabled = false;
		*/
	}
}
