using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {
	public GameObject door;
    
	void OnTriggerEnter(Collider other) {
		Debug.Log ("OnEnterTrigger by: " + other.gameObject.tag);
		if ("TriggerBall".Equals(other.gameObject.tag)) {
			if (door != null) {
				door.GetComponent<DoorController> ().TriggerDoorSuccess ();

				//other.GetComponent<Rigidbody> ().isKinematic = true;
			}
		}
	}
	void OnTriggerExit(Collider other) {
		Debug.Log ("OnExitTrigger by: " + other.gameObject.tag);

		if ("TriggerBall".Equals(other.gameObject.tag)) {
			if (door != null) {
				door.GetComponent<DoorController> ().TriggerDoorUnsuccess ();
			}
		}
	}
}
