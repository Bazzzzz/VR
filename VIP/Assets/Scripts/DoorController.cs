using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
    public AudioClip audioClip;
    public AudioSource audioSourceAmbient;
    public AudioSource audioSourceTrigger;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void TriggerDoorSuccess() {
		gameObject.SetActive (false);
        if (audioSourceAmbient.isPlaying)
        {
            audioSourceAmbient.mute = true;
            Debug.Log("AmbientSource muted");
        }
        audioSourceTrigger.PlayOneShot(audioClip);
        audioSourceAmbient.mute = false;
        Debug.Log("AmbientSource unmuted");
    }
    public void TriggerDoorUnsuccess() {
		gameObject.SetActive (true);
	}
}
