﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void TriggerDoorSuccess() {
		gameObject.SetActive (false);
	}
	public void TriggerDoorUnsuccess() {
		gameObject.SetActive (true);
	}
}
