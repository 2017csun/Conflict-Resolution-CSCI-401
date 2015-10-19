using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetPos : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		//		GetComponentInParent(Canvas<>);'
		//transform.position.Set(Camera.main.transform.position.x,Camera.main.transform.position.y,Camera.main.transform.position.z);
		//print (transform.position);
		
		//this.transform.rotation = Camera.main.transform.rotation;
		
		Debug.Log (" HI " + gameObject + transform.position);
		
	}
	
	// Update is called once per frame
	public void Update () {
		//transform.position.Set(Camera.main.transform.position.x,Camera.main.transform.position.y,Camera.main.transform.position.z + 5.0f);;
		
		transform.position = Camera.main.transform.position + Camera.main.transform.forward * .8f;
		//transform.rotation = Camera.main.transform.rotation;
		//Debug.Log (" HI " + gameObject);
	}
}
