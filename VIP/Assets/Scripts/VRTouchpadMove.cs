using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRTouchpadMove : MonoBehaviour {

	[SerializeField]
	private Transform rig;

	//private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	private SteamVR_Controller.Device device;
	private SteamVR_TrackedController controller;
	private SteamVR_TrackedObject trackedObj;

	private Vector2 axis = Vector2.zero;

	// Use this for initialization
	void Start () {
		controller = GetComponent<SteamVR_TrackedController> ();
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
		controller.PadClicked += movePlayer;
	}
	
	// Update is called once per frame
	void Update () {
//		if (controller == null) {
//			Debug.Log (" Controller not initialized");
//			return;
//		}
//		var device = SteamVR_Controller.Input ((int)trackedObj.index);
//
//		if (controller.GetTouch (touchpad)) {
//			axis = device.GetAxis (Valve.VR.EVRButtonId.k_EButton_Axis0);
//
//			if (rig != null) {
//				rig.position += (transform.right * axis.x + transform.forward * axis.y) * Time.deltaTime;
//				rig.position = new Vector3 (rig.position.x, 0, rig.position.z);
//			}
//		}
	}
	private void movePlayer(object sender, ClickedEventArgs e) {
		if (device.GetAxis().x != 0 || device.GetAxis().y != 0) {
			if (rig != null) {
				rig.position += (transform.right * device.GetAxis().x + transform.forward * device.GetAxis().y) * Time.deltaTime;
				rig.position = new Vector3 (rig.position.x, 0, rig.position.z);
			}
		}
	
	}

}
