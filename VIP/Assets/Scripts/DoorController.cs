using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
    public AudioClip audioClip;
    public AudioSource audioSourceAmbient;
    public AudioSource audioSourceTrigger;
    public bool doorIsActive;
    public Transform doorLookTransform;
    public GameObject cameraRig;
    public float transitionSpeed = 1.0f;

    private Transform standardTransform;
	// Use this for initialization
	void Start () {
        doorIsActive = true;
        standardTransform = cameraRig.transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void HandleTriggerDoorUnactive()
    {
        StartCoroutine("TriggerDoorInactive");
    }
	private void TriggerDoorInactive() {
        doorIsActive = false;
        gameObject.transform.Translate(new Vector3(0, 4, 0));
        if (audioSourceAmbient.isPlaying)
        {
            audioSourceAmbient.mute = true;
            Debug.Log("AmbientSource muted");
        }
        audioSourceTrigger.PlayOneShot(audioClip);
        //if (cameraRig != null)
        //{
        //    //Transform currentTransform = cameraRig.transform;
        //    Debug.Log("doorLookTransform position: " + doorLookTransform.position);
        //    Debug.Log("StandardTransform position: " + standardTransform.position);

        //    cameraRig.transform.position = Vector3.Lerp(standardTransform.position, doorLookTransform.position, Time.deltaTime * transitionSpeed);

        //    Vector3 doorLookAngles = new Vector3(
        //        Mathf.LerpAngle(standardTransform.rotation.eulerAngles.x, doorLookTransform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
        //        Mathf.LerpAngle(standardTransform.rotation.eulerAngles.y, doorLookTransform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
        //        Mathf.LerpAngle(standardTransform.rotation.eulerAngles.z, doorLookTransform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));

        //    cameraRig.transform.eulerAngles = doorLookAngles;

        //    yield return new WaitForSeconds(2f);
        //    Debug.Log("Current position: " + cameraRig.transform.position);
        //    Debug.Log("StandardTransform position: " + standardTransform.position);

        //    cameraRig.transform.position = Vector3.Lerp(cameraRig.transform.position, standardTransform.position, Time.deltaTime * transitionSpeed);

        //    Vector3 currentAngles = new Vector3(
        //        Mathf.LerpAngle(cameraRig.transform.rotation.eulerAngles.x, standardTransform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
        //        Mathf.LerpAngle(cameraRig.transform.rotation.eulerAngles.y, standardTransform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
        //        Mathf.LerpAngle(cameraRig.transform.rotation.eulerAngles.z, standardTransform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));

        //    cameraRig.transform.eulerAngles = currentAngles;
        //}
        audioSourceAmbient.mute = false;
        Debug.Log("AmbientSource unmuted");
    }
    public void HandleTriggerDoorActive()
    {
        StartCoroutine("TriggerDoorActive");
    }
    private void TriggerDoorActive() {
        doorIsActive = true;
        gameObject.transform.Translate(new Vector3(0, -4, 0));
    }
}
