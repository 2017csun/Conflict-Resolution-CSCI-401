using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworking : NetworkBehaviour {

    [SerializeField]
    Camera FPSCharacterCam;

    [SerializeField]
    AudioListener audioListener;

    public override void OnStartLocalPlayer () {
        base.OnStartLocalPlayer();

        this.GetComponent<CharacterController>().enabled = true;
        this.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        FPSCharacterCam.enabled = true;
        audioListener.enabled = true;
    }

    [Command]
    public void CmdSetBodyIcon (string iconName) {
        GameObject icon = GameObject.Find(iconName);
        if (icon == null) {
            //  TODO: NEED TO BE ABLE TO FIND GAMEOBJECT THAT IS INACTIVE

            Debug.LogError("Can't find icon with name: " + iconName);
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
        Debug.Log("Updating " + icon.name);
        CmdSetBodyIcon(icon.name);
    }
}
