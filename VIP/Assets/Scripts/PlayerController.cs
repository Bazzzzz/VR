using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Vector3 standardPosition;
	// Use this for initialization
	void Start () {
        standardPosition = new Vector3(-22.6f, 4.37f, 56.46f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision coll)
    {
        Debug.Log("Player collision with: " + coll.gameObject.name);
        if ("Terrain".Equals(coll.gameObject.tag))
        {
            transform.position = Vector3.MoveTowards(transform.position, standardPosition, 2);
        }
    }
}
