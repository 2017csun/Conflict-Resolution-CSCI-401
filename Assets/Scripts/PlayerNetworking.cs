using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerNetworking : NetworkBehaviour {

    private GameObject[] playerIcons;

    private GameEngine gameEngine;

    [SerializeField]
    Camera FPSCharacterCam;

    void Start () {
        GameObject go = GameObject.FindGameObjectWithTag("Engine");
        if (go == null) {
            Debug.LogError("Error: Game Engine object has not been tagged as 'Engine'");
            return;
        }

        gameEngine = go.GetComponent<GameEngine>();
    }

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
        this.GetComponent<FirstPersonController>().enabled = true;
        FPSCharacterCam.enabled = true;
        audioListener.enabled = true;
    }

    [Command]
    public void CmdSetBodyIcon (string iconName, bool fromServer) {
        //  Get the icon being set
        GameObject icon = GameObject.Find(iconName);
        if (icon == null) {
            Debug.LogError("Can't find the icon named " + iconName);
            return;
        }

        icon.transform.localScale += new Vector3(.2f, .2f, .2f);
        icon.GetComponent<RotateSlowly>().enabled = false;    //  Stop the rotating script

        icon.transform.parent = this.transform;
        icon.transform.localPosition = new Vector3(0, 0, 0);
        icon.transform.localRotation = Quaternion.Euler(0, 90, 0);

        //  Synch up the body on client
        RpcSyncIconBody(
            icon.name,
            icon.transform.localPosition,
            icon.transform.localRotation,
            icon.transform.localScale,
			fromServer
        );
    }

    [ClientRpc]
    public void RpcSyncIconBody (
		string iconName, Vector3 localPos, Quaternion localRote, Vector3 localScale, bool fromServer
	)
	{
        if (this.isServer) {
            //  Don't do anything on the server's client
            return;
        }

		//	Find the icon
		GameObject icon = GameObject.Find(iconName);

		//	Find the player parent
		GameObject parent = null;
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player")) {
			if (fromServer && !go.GetComponent<FirstPersonController>().isActiveAndEnabled) {
				parent = go;
				break;
			}
			else if (!fromServer && go.GetComponent<FirstPersonController>().isActiveAndEnabled) {
				parent = go;
				break;
			}
		}

		//  Set its layer and its children's layers to "Player2"
		string iconLayer = fromServer ? "Player1" : "Player2";
		Debug.Log("Changing " + iconName + " to " + iconLayer);
		icon.layer = LayerMask.NameToLayer(iconLayer);
		GameEngine.fullChangeLayer(icon.transform, iconLayer);

        icon.transform.parent = parent.transform;
        icon.transform.localPosition = localPos;
        icon.transform.localRotation = localRote;
        icon.transform.localScale = localScale;
        icon.GetComponent<RotateSlowly>().enabled = false;
    }

    //  Update the player's body to be the icon
    public void updateBodyToIcon (GameObject icon) {
        CmdSetBodyIcon(icon.name, this.isServer);

        //  Destroy this local icon if client
//        if (!this.isServer) {
//            Destroy(icon);
//        }
    }

    //  Send a new player to the server game engine
    public void sendPlayerToServer (string playerName, int playerIconIndex, int playerID) {
        CmdSendPlayerToServer(playerName, playerIconIndex, playerID);
    }
    //  The actual Command call
    [Command]
    public void CmdSendPlayerToServer (string playerName, int iconIndex, int playerID) {
        gameEngine.updatePlayerAcrossNetwork(playerName, iconIndex, playerID);
    }

    //  Receive a new player from server
    public void receivePlayerFromServer (string playerName, int playerIconIndex, int playerID) {
        RpcReceivePlayerFromServer(playerName, playerIconIndex, playerID);
    }
    //  The actual RPC
    [ClientRpc]
    public void RpcReceivePlayerFromServer (string playerName, int iconIndex, int playerID) {
        if (this.isServer) {
            //  Don't do anything on the server's client
            return;
        }
        gameEngine.updatePlayerAcrossNetwork(playerName, iconIndex, playerID);
    }

    //  Call to make server pick the random players
    public void getRandomPlayersFromServer () {
        CmdServerChooseRandomPlayers();
    }
    //  Command call for server to pick the random players
    [Command]
    public void CmdServerChooseRandomPlayers () {
        gameEngine.ServerChooseRandomPlayers(false);

        //  Send the players to the client through RPC
        RpcGetRandomPlayersFromServer(gameEngine.playerOneClass.playerID, gameEngine.playerTwoClass.playerID);
    }
    //  RPC so client can get the picked players from the server
    [ClientRpc]
    public void RpcGetRandomPlayersFromServer (int player1ID, int player2ID) {
        if (this.isServer) {
            //  Don't do anything on the server's client
            return;
        }

        //  Find the player class objects
        PlayerClass p1 = null;
        PlayerClass p2 = null;
        foreach (PlayerClass play in gameEngine.allPlayers) {
            if (play.playerID == player1ID) {
                p1 = play;
            }
            if (play.playerID == player2ID) {
                p2 = play;
            }
        }

        //  Set the players on client game engine and render them
        gameEngine.playerOneClass = p1;
        gameEngine.playerTwoClass = p2;
        gameEngine.displayRandomPlayers();
    }

    //  Update the synched var currAvailableID on server
    public void updateCurrAvailableID (int newID) {
        CmdUpdateCurrAvailableID(newID);
    }
    [Command]
    public void CmdUpdateCurrAvailableID (int newID) {
        gameEngine.currAvailableID = newID;
    }
	public void updateScore(int score){
		CmdSetScore (score);
		
		
	}
	public void CmdSetScore(int score) {
		
		gameEngine.updateScoreToClient (score);
	}

	public void sendPlayer2Intention () {
		CmdUpdatePlayer2Intention(gameEngine.getPlayer2Intention());
	}

	[Command]
	public void CmdUpdatePlayer2Intention (string intention) {
		gameEngine.syncPlayer2Intention(intention);
	}
}
