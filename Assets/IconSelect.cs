using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IconSelect : MonoBehaviour {


public void ChangeToScene (string sceneToChangeTo) {
		Application.LoadLevel (sceneToChangeTo);
		// Update is called once per frame
	}
}
