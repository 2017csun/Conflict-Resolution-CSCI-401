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

    //  Start a host and register the game
    //  @returns game code for other host to connect to
	public void startServer () {
        myClient = this.StartHost();

		gameCode = "ilikecereal";
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

	//	Request the list of matches matching gameTypeName
	public void connectToServer (string gameTypeName) {
		networkMatch.ListMatches(0, 20, gameTypeName, OnMatchList);
	}

	//	Check for exactly 1 match
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

	//	Called when match has been joined
	public void OnMatchJoined (JoinMatchResponse join) {
		if (join.success) {
			Debug.Log("Match joined successfully!");
			NetworkClient myClient = new NetworkClient();
			myClient.RegisterHandler(MsgType.Connect, OnConnectedToServer);
			myClient.Connect(new MatchInfo(join));
		}
		else {
			Debug.LogError("MATCH JOIN FAILED IDK WHAT TO DOOOOO");
		}
	}

	//	Once connected, load level and spawn player
	public void OnConnectedToServer (NetworkMessage msg) {
		Debug.Log("Connected!");
		spawnPlayer();
	}

	private void spawnPlayer () {
		Application.LoadLevel("Eric_Scene");
		GameObject spawnLocation = GameObject.FindGameObjectWithTag("PlayerSpawn");
		GameObject player = Network.Instantiate(
			this.playerPrefab,
			spawnLocation.transform.position,
			spawnLocation.transform.rotation,
			0	//	Group number
		) as GameObject;
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
