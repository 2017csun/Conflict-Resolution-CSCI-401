using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void activateNameInputPanel () {
        Activity act = this.gameObject.GetComponent<Activity>();
        act.enablePanel();
    }

    public void deactivateNameInputPanel () {
        Activity act = this.gameObject.GetComponent<Activity>();
        act.disablePanel();
    }
}
