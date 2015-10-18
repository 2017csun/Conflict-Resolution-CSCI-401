using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class IconSelector : MonoBehaviour {
	
	public GameObject[] gos;
	public int reset;
	public int current;
	// Use this for initialization
	void Start () {
		reset = 0;
		current = 0;
		gos = new GameObject[12];
		
		gos [0] = GameObject.Find ("/Icons/speedChar");
		gos [1] = GameObject.Find ("/Icons/greenAlien");
		gos [2] = GameObject.Find ("/Icons/kyleRobot");
		gos [3] = GameObject.Find ("/Icons/stone");
		gos [4] = GameObject.Find ("/Icons/Astronaut");
		gos [5] = GameObject.Find ("/Icons/bullet");
		gos [6] = GameObject.Find ("/Icons/gun");
		gos [7] = GameObject.Find ("/Icons/pinkAlien");
		gos [8] = GameObject.Find ("/Icons/drone");
		gos [9] = GameObject.Find ("/Icons/laptop");
		gos [10] = GameObject.Find ("/Icons/tv");
		gos [11] = GameObject.Find ("/Icons/tubeGlass");
		
		
	}


	void Update() {
		//if (reset == 1) {
			//print ("That reset though");

		//}


	} 
	public void UpdateIcon() {
		/*if (reset == 1) {
			Debug.Log ("IT HAS BEEN RESET");
			current = 0;
			reset = 0;
		}*/
		if (current == 0) {
			gos[current].SetActive(true);
			//Debug.Log ("In This update");
			
			//astro.SetActive (true);
			current++; 
			
		}
		else {
			//Debug.Log ("this should index should be false " + ((current - 1) % 12));
			//Debug.Log ("this index should be true " + (current % 12));
			//Debug.Log ("Current is now " + current);
			gos[(current - 1) % 12].SetActive(false);
			gos[current % 12].SetActive (true);

			current = current % 12;
			current++;
		}
		
	}

	public void disableIcon (GameObject g) {
		g.SetActive(false);
		current = 0;
		//print (reset);
	}
	public void enableIcon (GameObject g) {
		current = 0;
		g.SetActive(true);
		 
	//	print ("SETACTIVE");
	}

	public void setReset(int r){
		r = reset;

	}
}