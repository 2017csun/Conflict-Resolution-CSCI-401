using UnityEngine;
using System.Collections;

public class FadeLight : MonoBehaviour {

	public Light myLight;
	public float fadeTime;
	private float currTime;
	private float startIntensity;
	
	void Start () {
		currTime = 0;
		startIntensity = myLight.intensity;
	}

	void Update () {
		currTime += Time.deltaTime;
		myLight.intensity = Mathf.Lerp(0, startIntensity, 1.0f - currTime/fadeTime);
	}
}
