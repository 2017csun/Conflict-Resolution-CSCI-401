using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;

public class ConflictNetworkManager : NetworkManager {

	public static float networkTimeout = 10f;
	private float currTime = 0;

	private HostData[] hostData;
	private bool isRefreshingHosts = false;

	private string serverToConnect;
    private NetworkClient myClient;
    private NetworkMatch networkMatch;

	void Start () {
        networkMatch = this.gameObject.AddComponent<NetworkMatch>();
        networkMatch.SetProgramAppID((AppID)379051);
	}
	
	void OnMasterServerEvent (MasterServerEvent mse) {
		//	TODO: HANDLE REGISTRATION FAILURES
		if (mse == MasterServerEvent.RegistrationSucceeded) {
			Debug.Log("Registration succeeded!");
		}
	}
	
	void Update () {
		if (isRefreshingHosts) {
			currTime += Time.deltaTime;
			if (currTime > networkTimeout) {
				Debug.Log("Network timed out");
			}

			//	If host list request is done
			if (MasterServer.PollHostList().Length > 0) {
				hostData = MasterServer.PollHostList();
				//	Find the game we want to connect to 
				for (int i = 0; i < hostData.Length; ++i) {
					if (hostData[i].gameType == serverToConnect) {
						Network.Connect(hostData[i]);
						isRefreshingHosts = false;
						currTime = 0;
						break;
					}
				}
			}
		}
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

        //string gameCode = "";
        //int codeLength = 6;
        //string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        ////  Generate random string
        //for (int i = 0; i < codeLength; ++i) {
        //    char randomChar = chars[Random.Range(0, chars.Length)];
        //    gameCode += randomChar;
        //}

        //gameCode = "eric_dong_is_awesome";
        //Debug.Log("Registering under code: " + gameCode);
        //CreateMatchRequest create = new CreateMatchRequest();
        //create.name = gameCode;
        //create.size = 2;
        //create.advertise = true;
        //create.password = "";

        //networkMatch.CreateMatch(create, OnMatchCreate);
        //MasterServer.RegisterHost(gameCode, "Conflict Resolution", "Fun times at USC");
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

	//--------------------------------------------------
	//	Client Code
	//--------------------------------------------------

	public void connectToServer (string gameTypeName) {
		MasterServer.RequestHostList(gameTypeName);
		serverToConnect = gameTypeName;
		isRefreshingHosts = true;
	}
}
