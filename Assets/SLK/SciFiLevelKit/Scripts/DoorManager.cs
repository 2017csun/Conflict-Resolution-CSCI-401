using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class DoorManager : MonoBehaviour {

	public Door door1;
	public Door door2;
	
	void OnTriggerEnter() {
        if (door1)
            door1.OpenDoor();
        if (door2)
            door2.OpenDoor();
	}
}
