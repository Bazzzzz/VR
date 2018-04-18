using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
	public bool isAvailable = false;
	public Vector3 standardPosition;
	public GoblinController gc;

	// Use this for initialization
	void Start () {
		if (GetComponent<Rigidbody> ().isKinematic == false) {
			GetComponent<Rigidbody> ().isKinematic = true;
		}
		standardPosition = new Vector3(-22.29f, 12f, 38.46f);
	}

	void onCollisionEnter(Collision coll) {
		if ("Floor".Equals(coll.gameObject.tag) && isAvailable) {
			isAvailable = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (gc != null) {
			if (!isAvailable && gc.isDead) {
	
				Debug.Log ("Resetting ball..");
				transform.position = Vector3.MoveTowards (transform.position, standardPosition, 2);
				GetComponent<Rigidbody> ().isKinematic = false;
				if (transform.position == standardPosition) {
					isAvailable = true;
				}
			}
		}

	}
}
