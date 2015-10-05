using UnityEngine;
using UnityEngine.UI;

using System.Collections;



public class saveName : MonoBehaviour {
	public InputField textIn;
	public int num = 0;
	//public ArrayList alist = new ArrayList ();
   public void nameSave(InputField name) {
		num++;
		//textIn = new InputField ();
		 
	//textIn = gameObject.GetComponent<InputField> ();
		Debug.Log (name.text);
		//Debug.Log (textIn.text);
	//alist.Add (input.text);
		Debug.Log ("Hi");
		name.text = " ";

		name.placeholder.GetComponent<Text>().text = "Enter Name";
		PlayerPrefs.SetString ("Player " + num, name.text);
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


		*/


	}

}