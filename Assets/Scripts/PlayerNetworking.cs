using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworking : NetworkBehaviour {

    [SerializeField]
    Camera FPSCharacterCam;

    [SerializeField]
    AudioListener audioListener;

	// Use this for initialization
	void Start () {
        if (this.isLocalPlayer) {
            this.GetComponent<CharacterController>().enabled = true;
            this.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            FPSCharacterCam.enabled = true;
            audioListener.enabled = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
