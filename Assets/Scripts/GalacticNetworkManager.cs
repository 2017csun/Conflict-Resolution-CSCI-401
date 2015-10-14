using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;

public class GalacticNetworkManager : NetworkManager {

    private string gameCode;
    private NetworkClient myClient;
    private NetworkMatch networkMatch;

    private bool isServer;

	// Use this for initialization
	void Start () {
        networkMatch = this.gameObject.AddComponent<NetworkMatch>();
        this.StartMatchMaker();
        networkMatch.SetProgramAppID((AppID)379051);
        gameCode = "";
	}

    void OnApplicationQuit () {
        this.StopMatchMaker();
        this.StopHost();
    }

    //--------------------------------------------------
    //	Server Code
    //--------------------------------------------------

    public void startServer () {
        isServer = true;
        myClient = this.StartHost();

        gameCode = "foodisgud";
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
            ;
            Debug.Log("Listening at " + matchResponse.address + ":" + matchResponse.port);
            NetworkServer.Listen(matchResponse.address, matchResponse.port);
        }
        else {
            Debug.LogError("Create match failed :(");
        }
    }

    //--------------------------------------------------
    //	Client Code
    //--------------------------------------------------

    //	Request the list of matches matching gameTypeName
    public void connectToServer (string gameTypeName) {
        isServer = false;
        networkMatch.ListMatches(0, 20, "", OnMatchList);
    }

    //	Check for exactly 1 match
    public override void OnMatchList (ListMatchResponse matchListResponse) {
        List<MatchDesc> matches = matchListResponse.matches;

        if (matches.Count > 1) {
            Debug.LogError("THERE ARE MULTIPLE MATCHES IDK WHAT TO DOOOOO");
        }
        else if (matches.Count == 1) {
            Debug.Log("Joining " + matches[0].name);
            networkMatch.JoinMatch(matches[0].networkId, "", OnMatchJoined);
        }
        else {
            Debug.LogError("MATCH NOT FOUND!!");
        }
    }

    //	Called when match has been joined
    public new void OnMatchJoined (JoinMatchResponse join) {
        if (join.success) {
            Debug.Log("Match joined successfully!");
            myClient = new NetworkClient();
            Debug.Log("Connect to " + join.address + ":" + join.port);
            myClient.Connect(join.address, join.port);
        }
        else {
            Debug.LogError("MATCH JOIN FAILED IDK WHAT TO DOOOOO");
        }
    }

    //	Once connected, load level and spawn player
    public override void OnClientConnect (NetworkConnection conn) {
        if (this.isServer) {
            return;
        }

        Debug.Log("Connected!");
        ClientScene.AddPlayer(conn, 0);
        Application.LoadLevel("Eric_Scene");
        //spawnPlayer();
    }

    private void spawnPlayer () {
        Application.LoadLevel("Eric_Scene");
        GameObject spawnLocation = GameObject.FindGameObjectWithTag("PlayerSpawn");
        GameObject player = Instantiate(
            this.playerPrefab,
            spawnLocation.transform.position,
            spawnLocation.transform.rotation
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
