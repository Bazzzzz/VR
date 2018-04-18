using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : MonoBehaviour {
    public Animator anim;
	public bool isDead = false;
    private int isDying = Animator.StringToHash("anim_dying");
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            anim.SetTrigger(isDying);
//        }
	}
	public void PlayIsDying() {
		anim.SetTrigger(isDying);
		isDead = true;
	}
}
