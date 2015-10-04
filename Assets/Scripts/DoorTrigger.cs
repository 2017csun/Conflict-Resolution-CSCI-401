using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {

    public GameObject[] doors;

    void OnTriggerEnter (Collider col) {
        for (int i = 0; i < doors.Length; ++i) {
            doors[i].GetComponent<Door>().OpenDoor();
        }
    }
}
