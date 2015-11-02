using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworking : NetworkBehaviour {

    private GameObject[] playerIcons;

    [SerializeField]
    Camera FPSCharacterCam;

    [SerializeField]
    AudioListener audioListener;

    void Update () {
        if (playerIcons == null) {
            playerIcons = GameObject.FindGameObjectsWithTag("Icon");
        }
    }

    public override void OnStartLocalPlayer () {
        base.OnStartLocalPlayer();

        this.GetComponent<CharacterController>().enabled = true;
        this.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        FPSCharacterCam.enabled = true;
        audioListener.enabled = true;
    }

    [Command]
    public void CmdSetBodyIcon (string iconName) {
        //  Get the icon being set
        GameObject icon = null;
        for (int i = 0; i < playerIcons.Length; ++i) {
            if (playerIcons[i].name.Equals(iconName)) {
                icon = playerIcons[i];
            }
        }
        if (icon == null) {
            Debug.LogError("Can't find the icon named " + iconName);
            return;
        }

        icon.transform.localScale += new Vector3(.2f, .2f, .2f);
        icon.GetComponent<RotateSlowly>().enabled = false;    //  Stop the rotating script

        icon.transform.parent = this.transform;
        icon.transform.localPosition = new Vector3(0, 0, 0);
        icon.transform.localRotation = Quaternion.Euler(0, 90, 0);
        icon.SetActive(true);

        NetworkServer.Spawn(icon);
        RpcSyncIconBody(
            icon,
            this.gameObject,
            icon.transform.localPosition,
            icon.transform.localRotation,
            icon.transform.localScale
        );
    }

    [ClientRpc]
    public void RpcSyncIconBody (GameObject icon, GameObject parent, Vector3 localPos, Quaternion localRote, Vector3 localScale) {
        icon.transform.parent = parent.transform;
        icon.transform.localPosition = localPos;
        icon.transform.localRotation = localRote;
        icon.transform.localScale = localScale;
        icon.GetComponent<RotateSlowly>().enabled = false;
    }

    //  Update the player's body to be the icon
    public void updateBodyToIcon (GameObject icon) {
        CmdSetBodyIcon(icon.name);
    }

    //  Send a new player name to the server game engine
    public void sendNameToServer (string name) {
        CmdSendNameToServer(name);
    }
    //  The actual Command call
    [Command]
    public void CmdSendNameToServer (string name) {
        GameObject engine = GameObject.FindGameObjectWithTag("Engine");
        if (engine == null) {
            Debug.LogError("Error: Game Engine object has not been tagged as 'Engine'");
            return;
        }

        engine.GetComponent<GameEngine>().updateNamesFromClient(name);
    }

    //  Send a new player icon to the server game engine
    public void sendIconToServer (string name) {
        CmdSendIconToServer(name);
    }
    //  The actual Command call
    [Command]
    public void CmdSendIconToServer (string name) {
        GameObject engine = GameObject.FindGameObjectWithTag("Engine");
        if (engine == null) {
            Debug.LogError("Error: Game Engine object has not been tagged as 'Engine'");
            return;
        }

        engine.GetComponent<GameEngine>().updateIconFromClient(name);
    }
}
