using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimationPanel : MonoBehaviour {
    public float animationTime;
    private float currTime;

    private bool beginHeightAnim;
    private bool beginWidthAnim;

    private float startWidth;
    private float currWidth;
    private float endWidth;
    private float startHeight;
    private float currHeight;
    private float endHeight;

    private RectTransform myRectTransform;

	void Awake () {
        startWidth = currWidth = 5;
        startHeight = 0;
        currTime = 0;
        beginHeightAnim = false;
        beginWidthAnim = false;
        myRectTransform = this.GetComponent<RectTransform>();
	}
	
	void Update () {
        currTime += Time.deltaTime;

        //  Animations for height and width
        if (beginHeightAnim) {
            //  Lerp height based on how much time until (animationTime/2)
            currHeight = Mathf.Lerp(startHeight, endHeight, currTime / (animationTime / 2f));
            if (currHeight >= endHeight) {
                //  Reached height, start width anim
                currTime = 0;
                currHeight = endHeight;
                beginHeightAnim = false;
                beginWidthAnim = true;
            }
            myRectTransform.sizeDelta = new Vector2(currWidth, currHeight);
        }
        else if (beginWidthAnim) {
            //  Lerp width based on how much time until (animationTime/2)
            currWidth = Mathf.Lerp(startWidth, endWidth, currTime / (animationTime / 2));
            if (currWidth >= endWidth) {
                //  Reached width, done!
                currWidth = endWidth;
                beginWidthAnim = false;
            }
            myRectTransform.sizeDelta = new Vector2(currWidth, currHeight);
        }
	}

    public void beginAnimation (int toW, int toH, float alpha) {
        if (this.gameObject.activeSelf) {
            Debug.LogError("Error: Discard the animation panel before starting a new one!");
            return;
        }

        //  Set the alpha
        Color c = this.GetComponent<Image>().color;
        c.a = alpha;
        this.GetComponent<Image>().color = c;

        this.gameObject.SetActive(true);

        //  Animate height first, then width
        endWidth = toW;
        endHeight = toH;
        beginHeightAnim = true;
    }

    //  Hide and reset the panel
    public void discardPanel () {
        this.gameObject.SetActive(false);
        currWidth = startWidth;
        currHeight = startHeight;
        currTime = 0;
    }
}
