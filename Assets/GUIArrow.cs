using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIArrow : MonoBehaviour {
	private GameObject goTarget;
	
	void Update () {
		if (goTarget != null) {
			PositionArrow();  
		}
		else {
			goTarget = GameObject.FindGameObjectWithTag("Checkpoint");
		}
	}
	
	void PositionArrow()
	{
		this.gameObject.GetComponent<Image>().enabled = false;
		Vector3 targetLoc = goTarget.GetComponent<Floating>().startPoint;
		
		Vector3 v3Pos = Camera.main.WorldToViewportPoint(targetLoc);
		GameObject dummyCam = Camera.main.transform.parent.FindChild("DummyCamera").gameObject;
		Vector3 v3PosDummy = dummyCam.GetComponent<Camera>().WorldToViewportPoint(targetLoc);

//		if (v3Pos.x >= 0.0f && v3Pos.x <= 1.0f && v3Pos.y >= 0.0f && v3Pos.y <= 1.0f) {
//			return; // Object center is visible
//		}
		
		this.gameObject.GetComponent<Image>().enabled = true;

		//	Get horizontal field of view
		float vFOVInRads = Camera.main.fieldOfView * Mathf.Deg2Rad;
		float hFOVInRads = 2 * Mathf.Atan( Mathf.Tan(vFOVInRads / 2) * Camera.main.aspect);
		float hFOV = hFOVInRads * Mathf.Rad2Deg;

		//	Get Y rotation needed to face target
		Vector3 vecToTarget = targetLoc - Camera.main.transform.position;
		//	Project forward vec and vecToTarget onto same plane
		Vector3 vecToTargetProject = Vector3.ProjectOnPlane(vecToTarget, Vector3.up);
		Vector3 camForwardProject = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
		Quaternion fromTo = Quaternion.FromToRotation(camForwardProject, vecToTargetProject);
		float yRote = fromTo.eulerAngles.y;
		if (yRote > 180) {
			yRote -= 360;
		}

		//	Get X rotation needed to face target
		Debug.Log("Need to rotate: " + yRote);
		Quaternion forward = Camera.main.transform.rotation;
		forward *= Quaternion.AngleAxis(yRote, Vector3.up);
		fromTo = Quaternion.Angle(forward, vecToTarget);
		Debug.Log(fromTo.eulerAngles);
//		forward *= Quaternion.AngleAxis(yRote, Vector3.up);
//		Camera.main.transform.rotation = forward;

//		Debug.Log("xRote: " + xRote + ", yRote: " + yRote);

		//	Get rotation from forward to the target
//		}
//		float yViewCoord = Mathf.Clamp(fromToVec.x / (Camera.main.fieldOfView) + 0.5f, 0, 1);
//		float xViewCoord = Mathf.Clamp(fromToVec.y / (hFOV) + 0.5f, 0, 1);
//		Debug.Log(fromToVec);
//		Debug.Log("X view coord: " + xViewCoord);

		//	Clamp so coords are on edge of screen
//		Debug.Log("View pos: " + v3Pos);
//		Debug.Log("View pos dummy: " + v3PosDummy);
//		v3Pos.x = Mathf.Clamp(v3Pos.x, 0, 1);
//		v3Pos.y = Mathf.Clamp(v3Pos.y, 0, 1);
//		v3PosDummy.x = Mathf.Clamp(v3PosDummy.x, 0, 1);
//		v3PosDummy.y = Mathf.Clamp(v3PosDummy.y, 0, 1);
//
//		Vector3 screenPos;
//		if (v3Pos.z > 0) {
//			screenPos = new Vector3(v3Pos.x * Screen.width, v3Pos.y * Screen.height, 0);
//		}
//		else {
//			screenPos = new Vector3(v3PosDummy.x * Screen.width, v3PosDummy.y * Screen.height, 0);
//		}
//
//		Debug.Log("Screen pos: " + screenPos);
//
//		//	Set position
//		this.transform.position = new Vector3(v3Pos.x * Screen.width, v3Pos.y * Screen.height, 0);

		//	Set rotation
//		Quaternion v3Quat = Quaternion.FromToRotation(
//			Vector3.right,
//			new Vector3(v3Pos.x - 0.5f, v3Pos.y - 0.5f, 0)
//		);
////		Debug.Log(v3Quat.eulerAngles);
//
//        RectTransform rectTrans = this.gameObject.GetComponent<RectTransform>();
//		rectTrans.rotation = Quaternion.Euler(0, 0, v3Quat.eulerAngles.z - 90);
	}
}