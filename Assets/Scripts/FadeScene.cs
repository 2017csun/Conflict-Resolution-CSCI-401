using UnityEngine;
using System.Collections;

using Image = UnityEngine.UI.Image;

public class FadeScene : MonoBehaviour {

	public float timeBeforeFadeIn = 0;

    private Image myImage;
    private Color myColor;
    private bool fadingIn;

	void Start () {
        fadingIn = false;
        myImage = this.GetComponent<Image>();
        myColor = myImage.color;
		Invoke ("fadeIn", timeBeforeFadeIn);
	}

	void Update () {
        if (fadingIn) {
            myColor.a -= 0.3f * Time.deltaTime;
            if (myColor.a <= 0) {
                fadingIn = false;
                myColor.a = 0;
				this.gameObject.SetActive(false);
            }
            myImage.color = myColor;
        }
	}

    public void fadeIn() {
        fadingIn = true;
    }
}
