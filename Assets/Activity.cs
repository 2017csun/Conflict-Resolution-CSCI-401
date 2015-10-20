using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Activity : MonoBehaviour {

    public GameObject panel;

	// Use this for initialization
	void Start () {
		panel.SetActive (false);
	}
	
    public void enablePanel() {
        panel.SetActive(true);
	}

    public void disablePanel() {
        panel.SetActive(false);
	}

}