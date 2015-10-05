using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollViewScr : MonoBehaviour {

	private float hScrollbarValue;
	public Vector2 scr = Vector2.zero;
	private string innerText = "Found me!";

	public void OnGUI () {
//		scr = GUI.HorizontalScrollbar (new Rect (600, 350, 100, 30),hScrollbarValue, 1.0f, 0.0f, 10.0f );
		innerText = GUI.TextArea (new Rect (0, 0, 200, 200), innerText);






		scr = GUI.BeginScrollView(new Rect(600, 400,200,200), scr, new Rect(0,0,190,400));
		                          innerText = GUI.TextArea (new Rect (0,0,200,200), innerText);


		GUI.EndScrollView ();

}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
