using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GameEngine : MonoBehaviour {

    [HideInInspector]
    public GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}

    public void activateNameInputPanel () {
        //  Display the UI
        Activity act = this.gameObject.GetComponent<Activity>();
        act.enablePanel();

        //  Disable player controls
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void deactivateNameInputPanel () {
        //  Hide the UI
        Activity act = this.gameObject.GetComponent<Activity>();
        act.disablePanel();

        //  Enable player controls
        player.GetComponent<FirstPersonController>().enabled = true;
    }
}
