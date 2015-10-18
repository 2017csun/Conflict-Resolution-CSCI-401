using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class NamesDisplayed : MonoBehaviour {



	public List<Dictionary<string,string>>players;
	public Dictionary<string, string> currentdict;
	// Use this for initialization
	void Start () {
		players = new List<Dictionary<string,string>> ();

	
	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < players.Count; i++) {
			Text newtext = gameObject.AddComponent<Text> ();
			//newtexttransform
			//Debug.Log ("players " + players[i].TryGetValue

		}
	
	}

	public void addToList (string name) {


		Dictionary<string,string> d = new Dictionary<string , string > ();
		d.Add (name, "iconpath");
		players.Add (d);
		currentdict = d;
	}




	public void getDict () {








	}
}


