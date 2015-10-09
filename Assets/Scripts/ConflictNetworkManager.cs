using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;

public class ConflictNetworkManager : NetworkManager {

	public static float networkTimeout = 10f;
	private float currTime = 0;

	private HostData[] hostData;
	private bool isRefreshingHosts = false;

	private string gameCode;
    private NetworkClient myClient;
    private NetworkMatch networkMatch;

	void Start () {
        networkMatch = this.gameObject.AddComponent<NetworkMatch>();
        networkMatch.SetProgramAppID((AppID)379051);
		gameCode = "";
	}

	//--------------------------------------------------
	//	Server Code
	//--------------------------------------------------

	private void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("A PLAYER HAS CONNECTED!!");
	}

    //  Start a host and register the game
    //  @returns game code for other host to connect to
	public void startServer () {
        myClient = this.StartHost();

		Debug.Log("Creating match under name: " + gameCode);

		//	Create the matchmaker request
        CreateMatchRequest create = new CreateMatchRequest();
        create.name = gameCode;
        create.size = 2;
        create.advertise = true;
        create.password = "";

        networkMatch.CreateMatch(create, OnMatchCreate);        
	}

    public override void OnMatchCreate (CreateMatchResponse matchResponse) {
        if (matchResponse.success) {
            Debug.Log("Create match succeeded!");
            Utility.SetAccessTokenForNetwork(
                matchResponse.networkId,
                new NetworkAccessToken(matchResponse.accessTokenString)
            );
            NetworkServer.Listen(new MatchInfo(matchResponse), 35000);
        }
        else {
            Debug.LogError("Create match failed :(");
        }
    }

	public override void OnStartHost () {
		base.OnStartHost();

		Debug.Log("Host started!");
	}

	//--------------------------------------------------
	//	Client Code
	//--------------------------------------------------

	public void connectToServer (string gameTypeName) {
		networkMatch.ListMatches(0, 20, gameTypeName, OnMatchList);
	}

	public override void OnMatchList (ListMatchResponse matchListResponse) {
		List<MatchDesc> matches = matchListResponse.matches;

		if (matches.Count > 1) {
			Debug.LogError("THERE ARE MULTIPLE MATCHES IDK WHAT TO DOOOOO");
		}
		else if (matches.Count == 1) {
			networkMatch.JoinMatch(matches[0].networkId, "", OnMatchJoined);
		}
		else {
			Debug.LogError("MATCH NOT FOUND!!");
		}
	}

	public void OnMatchJoined (JoinMatchResponse join) {
		Debug.Log("Match joined successfully!");
		if (join.success) {
			NetworkClient myClient = new NetworkClient();
			myClient.RegisterHandler(MsgType.Connect, OnConnected);
			myClient.Connect(new MatchInfo(join));
		}
	}

	public void OnConnected (NetworkMessage msg) {
		Debug.Log("Connected!");
	}

	//--------------------------------------------------
	//	Utility functions
	//--------------------------------------------------

	public string generateMatchKey () {
		int codeLength = 6;
		string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		
		//  Generate random string
		for (int i = 0; i < codeLength; ++i) {
			char randomChar = chars[Random.Range(0, chars.Length)];
			gameCode += randomChar;
		}

		gameCode = "ilikecereal";
		return gameCode;
	}
}
