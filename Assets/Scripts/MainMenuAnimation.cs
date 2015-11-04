using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuAnimation : MonoBehaviour {
	public MovieTexture movie;
	// private AudioSource audio;

	void Start () {
		GetComponent<RawImage> ().texture = movie as MovieTexture;
		movie.Play ();
		movie.loop = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
