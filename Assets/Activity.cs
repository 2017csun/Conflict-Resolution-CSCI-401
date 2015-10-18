using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Activity : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
public	void Update () {
	//	gameObject.SetActive (true);


	}

public void enablePanel(GameObject go) {
		go.SetActive (true);
	}

public void disablePanel(GameObject go) {

		go.SetActive (false);
	}

}