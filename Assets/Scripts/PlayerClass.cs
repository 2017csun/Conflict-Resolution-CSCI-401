using UnityEngine;
using System.Collections;

public class PlayerClass {
    public GameObject playerObject;
    public string playerName;
    public int playerID;
    public GameObject playerIcon;

    public PlayerClass () { }

    public PlayerClass (string name, int ID) {
        playerName = name;
        playerID = ID;
    }

    public PlayerClass (PlayerClass other) {
        this.playerIcon = other.playerIcon;
        this.playerName = other.playerName;
        this.playerID = other.playerID;
        this.playerIcon = other.playerIcon;
    }
}
