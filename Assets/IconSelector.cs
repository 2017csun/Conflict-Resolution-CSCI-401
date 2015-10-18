using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class IconSelector : MonoBehaviour {

	public GameObject[] gos;
	public bool doit;
	public GameObject p;
	public GameObject[] objects;
	public GameObject[] wos;
	public Vector3 start;
	//public GameObject[] gos;// = new GameObject[12];
	public int reset;
	public int current;
	// Use this for initialization
	void Start () {
		reset = 0;
		doit = false;
		current = 0;
		objects = new GameObject[1];
		wos = new GameObject[12];
		p = GameObject.Find ("Test");// InitializeArray<GameObject>(12);
		objects[0] = GameObject.Find ("/Icons");
		//start = new Vector3 (0.0,0.0,0.0);
		gos =new GameObject[12];;
		gos [0] = GameObject.Find ("/Icons/speedChar");
		gos[1] = GameObject.Find ("/Icons/greenAlien");
		gos [2] = GameObject.Find ("/Icons/kyleRobot");
		gos [3] = GameObject.Find ("/Icons/stones");
		gos [4] = GameObject.Find ("/Icons/Astronaut");
		gos [5] = GameObject.Find ("/Icons/bullet");
		gos [6] = GameObject.Find ("/Icons/gun");
		gos [7] = GameObject.Find ("/Icons/pinkAlien");
		gos [8] = GameObject.Find ("/Icons/drone");
		gos [9] = GameObject.Find ("/Icons/laptop");
		gos [10] = GameObject.Find ("/Icons/tv");
		gos [11] = GameObject.Find ("/Icons/tubeGlass");

		wos [0] = GameObject.Find ("/Icons/speedChar");
		wos [1] = GameObject.Find ("/Icons/greenAlien");
		wos [2] = GameObject.Find ("/Icons/kyleRobot");
		wos [3] = GameObject.Find ("/Icons/stone");

		wos [4] = GameObject.Find ("/Icons/Astronaut");
		wos [5] = GameObject.Find ("/Icons/bullet");
		wos [6] = GameObject.Find ("/Icons/gun");
		wos [7] = GameObject.Find ("/Icons/pinkAlien");
		wos [8] = GameObject.Find ("/Icons/drone");
		wos [9] = GameObject.Find ("/Icons/laptop");
		wos [10] = GameObject.Find ("/Icons/tv");
		wos [11] = GameObject.Find ("/Icons/tubeGlass");

	}

	T[] InitializeArray<T>(int length) where T : new()
	{
		T[] array = new T[length];
		for (int i = 0; i < length; ++i)
		{
			array[i] = new T();
		}
		
		return array;
	}
	void Update() {

			//print ("That reset though");

		//}
		 /*if (doit == true) {
			for (int i = 0; i < wos.Length; i++) {

					wos [i].transform.position = Camera.main.transform.position + Camera.main.transform.forward * .8f;
			}
		}*/
	} 
	public void UpdateIcon() {
		/*if (reset == 1) {
			Debug.Log ("IT HAS BEEN RESET");
			current = 0;
			reset = 0;
		}*/
		if (current == 0) {
			//gos[0] = GameObject.Find ("/Icons/speedChar");
			wos[current].SetActive(true);
			wos[current].transform.position = Camera.main.transform.position + Camera.main.transform.forward * .8f;
			//Debug.Log ("In This update");
			
			//astro.SetActive (true);
			current++; 
			
		}
		else {
			//Debug.Log ("this should index should be false " + ((current - 1) % 12));
			//Debug.Log ("this index should be true " + (current % 12));
			//Debug.Log ("Current is now " + current);
			wos[(current - 1) % 12].SetActive(false);
			wos[current % 12].SetActive (true);
		
			wos[current % 12].transform.position = Camera.main.transform.position + Camera.main.transform.forward * .8f;
			//start = Camera.main.transform.position + Camera.main.transform.forward * .8f;
			doit = true;
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