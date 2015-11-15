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

		//	Get horizontal field of view
		float vFOVInRads = Camera.main.fieldOfView * Mathf.Deg2Rad;
		float hFOVInRads = 2 * Mathf.Atan( Mathf.Tan(vFOVInRads / 2) * Camera.main.aspect);
		float hFOV = hFOVInRads * Mathf.Rad2Deg;

		//	Get Y rotation needed to face target

		Vector3 vecToTarget = goTarget.GetComponent<Floating>().startPoint - Camera.main.transform.position;
		//	Project forward vec and vecToTarget onto same plane
		Vector3 vecToTargetProject = Vector3.ProjectOnPlane(vecToTarget, Vector3.up);
		Vector3 camForwardProject = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
		Quaternion fromTo = Quaternion.FromToRotation(camForwardProject, vecToTargetProject);
		float yRote = fromTo.eulerAngles.y;
		if (yRote > 180) {
			yRote -= 360;
		}

		//	Get X rotation needed to face target

		//	Zero out y component of vecToTarget and get the angle
		float angleTarget = Vector3.Angle(vecToTarget, new Vector3(vecToTarget.x, 0, vecToTarget.z));
		angleTarget *= vecToTarget.y > 0 ? 1 : -1;
		//	Zero out y component of camera forward and get the angle
		float angleCam = Vector3.Angle(
			Camera.main.transform.forward,
			new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z)
		);
		angleCam *= Camera.main.transform.forward.y > 0 ? 1 : -1;

		float xRote = angleTarget - angleCam;

		//	Get view space coords based on camera FOV
		float u = Mathf.Clamp(yRote/hFOV + 0.5f, 0, 1);
		float v = Mathf.Clamp(xRote/Camera.main.fieldOfView + 0.5f, 0, 1);

		//	Hide if u,v is within the screen
		if (u > 0 && u < 1 && v > 0 && v < 1) {
			return;
		}
		this.gameObject.GetComponent<Image>().enabled = true;

		//	Set position to u, v
		this.transform.position = new Vector3(u * Screen.width, v * Screen.height, 0);

		//	Set rotation
		Quaternion v3Quat = Quaternion.FromToRotation(
			Vector3.right,
			new Vector3(u - 0.5f, v - 0.5f, 0)
		);
        RectTransform rectTrans = this.gameObject.GetComponent<RectTransform>();
		rectTrans.rotation = Quaternion.Euler(0, 0, v3Quat.eulerAngles.z - 90);
	}
}