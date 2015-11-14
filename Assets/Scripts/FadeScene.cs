using UnityEngine;
using System.Collections;

using Image = UnityEngine.UI.Image;

public class FadeScene : MonoBehaviour {

	public float timeBeforeFadeIn = 0;

    private Image myImage;
    private Color myColor;

    private bool fadingIn;
    private bool fadingOut;

	void Start () {
        fadingIn = fadingOut = false;
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
        if (fadingOut) {
            myColor.a += 0.4f * Time.deltaTime;
            if (myColor.a > 1) {
                fadingOut = false;
                myColor.a = 1;
            }
            myImage.color = myColor;
        }
	}

    public void fadeIn() {
        fadingIn = true;
    }

    public void whiteFadeOut () {
        myColor = Color.white;
        myColor.a = 0;
        myImage.color = myColor;
        this.gameObject.SetActive(true);
        fadingOut = true;
    }
}
