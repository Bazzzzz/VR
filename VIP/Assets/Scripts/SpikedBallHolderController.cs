using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBallHolderController : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        if (gameObject.name.Equals("BallHolderX")) { 
            transform.position = new Vector3(-28.5f, 19f, 25f);
        }
        if (gameObject.name.Equals("BallHolderY"))
        {
            transform.position = new Vector3(-28.5f, 19f, 35f);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
