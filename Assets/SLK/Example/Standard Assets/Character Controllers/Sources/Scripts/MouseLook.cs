using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {

    public float sensitivity = 15f;

	public float minimumY = -60F;
	public float maximumY = 60F;

    private float vertRote = 0;
    private float horizRote = 0;

    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }

	void Update ()
	{
        //	Left/Right rotation
        horizRote = Mathf.Lerp(
            horizRote,
            horizRote + (Input.GetAxis("Mouse X")*sensitivity),
            Time.deltaTime
        );

        //	Up/Down rotation
        vertRote = Mathf.Lerp(
            vertRote,
            vertRote - (Input.GetAxis("Mouse Y") * sensitivity),
            Time.deltaTime
        );
        vertRote = Mathf.Clamp(vertRote, minimumY, maximumY);

        this.transform.rotation = Quaternion.Euler(vertRote, horizRote, 0.0f);
	}
}