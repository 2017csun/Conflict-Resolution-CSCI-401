using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;


public class saveName : MonoBehaviour {

	public List<string> names;
	public Text[] places;
	public Transform[] go;
	string currentname;
	public int num = 1;
	public int reset;
	public List<GameObject> gameOlist;
	public List<Dictionary<string,GameObject>>players;
	public Dictionary<string, GameObject> currentdict;

	void Start() {
		reset = 0;
		num = 1;
		names = new List<string>();
		List<GameObject> gameOlist = new List<GameObject> ();
		GameObject o = GameObject.Find ("Summary/EachPlayer");
		//GameObject p = GameObject.Find ("/Icons");
		//	GameObject gameObject = GameObject.Find("Canvas");
		places = o.GetComponentsInChildren<Text>();
		go = GetComponentsInChildren<Transform>();
		//places.text[enter the index of the text object here].text = "hey";
		players = new List<Dictionary<string,GameObject>> ();


	}
	public void nameSave(InputField name) {
	
		name.placeholder.GetComponent<Text> ().text = "Enter Name";

		currentname = name.text;
		names.Add (currentname);

		for (int i = 0; i < names.Count; i++) {

		places[i].text = names[i];
		 
		}
		//places.

		//PlayerPrefs.SetString ("Player" + num, currentname);
		//currentname = name.text;
		
		//PlayerPrefs.Save ();
		//Debug.Log ( "YODI " + PlayerPrefs.GetString("Player" + num));
		//texty.text = "Cheese"; //PlayerPrefs.GetString ("Player" + num);

		
		//num++;
		name.text = " ";

	}

	
	public void saveIcon(Transform obj) {
		/*
		//go = obj.GetComponentsInChildren<GameObject> ();
		foreach (var child  in obj) {
			if (child.== true) {
				gameOlist.Add (i);
				Dictionary<string, GameObject> d = new Dictionary<string , GameObject > ();
				d.Add (currentname, i);
				
				players.Add (d);
				currentdict = d;
			}*

		}

		*/
		
		
	}



	void Update () {

		//Debug.Log ("THE NAME IS " + currentname);

		//places [0].transform.position =  Camera.main.transform.position + Camera.main.transform.forward * .8f;
		if (players != null) {
			for (int i = 0; i < players.Count; i++) {
				Text newtext = gameObject.AddComponent<Text> ();
				//Component o = new Component<GameObject>();
				//o = gameObject.AddComponent<GameObject>();
				newtext.text = names [i];
				//o = gameOlist[i];
				//newtext.transform.position = Vector3(gameObject.transform.position);
				//o.transform.position = Vector3(gameObject.transform.position);


			}

		}





	}





	/*

	//public Text playerText;
	private int currentplayer = 1;
	//public InputField textIn;
	public string currentName;
	public Text texty;
	public int num = 1;
	public List<string> names;
	//public ArrayList alist = new ArrayList ();
   public void nameSave(InputField name) {
		currentName = name.text;
		names.Add (currentName);
		PlayerPrefs.SetString ("Player" + num, currentName);
		currentName = name.text;

		PlayerPrefs.Save ();
		Debug.Log ( "YODI " + PlayerPrefs.GetString("Player" + num));
		texty.text = "Cheese"; //PlayerPrefs.GetString ("Player" + num);
		name.placeholder.GetComponent<Text>().text = "Enter Name";

		num++;
		name.text = " ";
	//Text newtext = gameObject.AddComponent<Text> ();

		//newtext.text = name.text;
	//var se= new InputField.SubmitEvent();
	//se.AddListener(SubmitName);
	//input.onEndEdit = se;
	/*
	public ArrayList alist = new ArrayList ();

	public GameObject go = new GameObject();

	public GUIText gt = new GUIText ();
	public void SaveName(string name) {
		alist.Add (name);
		go = GameObject.Find("Panel");
		 sc : GUIText;
		 sc = go.AddComponent ("GUIText");
		sc.text = name;





	}


	
	// Use this for initialization
	void Start () {
		names = new List<string>();
		texty = new Text ();
		//playerText.
		//playerText.text = GameObject.Find ("/Canvas/Summary/Text1").
		//txt = gameObject.GetComponent<Text>();
		//txt = GameObject.Find ("/Canvas/Summary/Text1");
		//txt.text="Player" + currentplayer;
	}
	/*
	// Update is called once per frame
	void Update () {
		txt.text= "Player : " + currentplayer;  
		currentplayer = PlayerPrefs.GetInt("TOTALSCORE"); 
		PlayerPrefs.SetString ("Player" + num, name.text); 
	}
	*/
	/*
	public void getName(Text t) {
		string str = PlayerPrefs.GetString ("Player " + num, currentName);
		//GameObject o = GameObject.Find("/Canvas/Summary/TextObject");
		//o = (Text) playerText;
		Debug.Log ("num be " + num);
		if (PlayerPrefs.HasKey ("Player" + num)) {
			//playerText = gameObject.GetComponent<Text>();
			//Debug.Log ("Still " + PlayerPrefs.GetString ("Player" + num));
			//GUI.Label(Rect (200, 200, 200, 50), new GUIContent(PlayerPrefs.GetString("Player" + num)));
			//yes.text = PlayerPrefs.GetString ("Player" + num);
			//es = gameObject.AddComponent<Text>();
			//o = GameObject.Find ("/Canvas/Summary/Text1");
			//o.
			//txt = gameObject.GetComponent<Text>(); 
			//str = "cheese";
			t.text = str;// = PlayerPrefs.GetString ("Player " + num);
			Debug.Log ("STR " + str);
			//o = GameObject.Find("/Canvas/Summary/Text1");
			//o PlayerPrefs.GetString ("Player" + num);
		} 

		else {
			Debug.Log ("NADA");
		}

	}

*/






}