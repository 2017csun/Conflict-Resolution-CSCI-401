using UnityEngine;
using System.Collections;

//  Destroy attached game object after some time
public class DeathTimer : MonoBehaviour {

    public float timeToDeath;
    private float currTime;

	void Start () {
        currTime = 0;
	}
	
	void Update () {
        currTime += Time.deltaTime;
        if (currTime >= timeToDeath) {
            Destroy(this.gameObject);
        }
	}
}
