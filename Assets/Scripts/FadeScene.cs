using UnityEngine;
using System.Collections;

using Image = UnityEngine.UI.Image;

public class FadeScene : MonoBehaviour {

    private Image myImage;
    private Color myColor;
    private bool fadingIn;

	void Start () {
        fadingIn = false;
        myImage = this.GetComponent<Image>();
        myColor = myImage.color;
	}
	
	void Update () {
        if (fadingIn) {
            myColor.a -= 0.3f * Time.deltaTime;
            if (myColor.a <= 0) {
                fadingIn = false;
                myColor.a = 0;
            }

            myImage.color = myColor;
        }
	}

    public void fadeIn() {
        fadingIn = true;
    }
}
