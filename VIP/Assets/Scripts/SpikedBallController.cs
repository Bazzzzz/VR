using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBallController : MonoBehaviour {
    private Rigidbody rb;
    public float force = 1;
    private float counter = 0;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();       
    }
	
	// Update is called once per frame
	void Update () {
        counter++;
        if (counter % 2 == 0)
        {
            rb.AddForce(Vector3.forward * force * rb.mass);
        } else
        {
            rb.AddForce(Vector3.back * force);
        }
    }
}
