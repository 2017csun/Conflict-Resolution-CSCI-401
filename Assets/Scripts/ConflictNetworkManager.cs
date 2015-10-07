using UnityEngine;
using System.Collections;

public class ConflictNetworkManager : UnityEngine.Networking.NetworkManager {

    public GameObject playerToSpawn;
    public GameObject playerSpawnLocation;

	public static float networkTimeout = 10f;
	private float currTime = 0;

	private HostData[] hostData;
	private bool isRefreshingHosts = false;

	private string serverToConnect;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnMasterServerEvent (MasterServerEvent mse) {
		//	TODO: HANDLE REGISTRATION FAILURES
		if (mse == MasterServerEvent.RegistrationSucceeded) {
			Debug.Log("Registration succeeded!");
            if (Network.isServer) {
                //  Spawn the server's player
                Instantiate(
                    playerToSpawn,
                    playerSpawnLocation.transform.position,
                    playerSpawnLocation.transform.rotation
                );
            }
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

	public void startServer (string gameTypeName) {
        NetworkConnectionError response = Network.InitializeServer(2, 25001, !Network.HavePublicAddress());
        if ( response != NetworkConnectionError.NoError) {
            Debug.LogError("Error initializing server: " + response);
        }
        else {
            MasterServer.RegisterHost(gameTypeName, "Conflict Resolution", "Fun times at USC");
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
