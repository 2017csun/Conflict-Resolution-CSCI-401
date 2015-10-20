using UnityEngine;
using System.Collections;

public class Floating : MonoBehaviour {
    public GameObject checkpointEffect;

    [HideInInspector]
    public GameObject gameEngine;

    public float bobRadius;
    public float bobSpeed;

    private Vector3 startPoint;
    private bool goingUp;

	void Start () {
        startPoint = this.transform.position;
        goingUp = Random.Range(0, 2) == 0;
        gameEngine = GameObject.Find("GameEngine");
	}
	
	void Update () {
        //  Have the light float up and down
        Vector3 delta = new Vector3(0, bobSpeed * Time.deltaTime, 0);
        if (goingUp) {
            this.transform.position += delta;
            if (Vector3.Distance(this.transform.position, startPoint) > bobRadius) {
                this.transform.position -= delta;
                goingUp = false;
            }
        }
        else {
            this.transform.position -= delta;
            if (Vector3.Distance(this.transform.position, startPoint) > bobRadius) {
                this.transform.position += delta;
                goingUp = true;
            }
        }
	}

    void OnTriggerEnter (Collider col) {
        Instantiate(checkpointEffect, this.transform.position, Quaternion.identity);

        gameEngine.GetComponent<GameEngine>().activateNameInputPanel();

        Destroy(this.gameObject);
    }
}
