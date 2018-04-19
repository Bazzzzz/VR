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
	// Use this for initialization
	void Start () {
        doorIsActive = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public IEnumerator TriggerDoorUnactive() {
        doorIsActive = false;
		gameObject.SetActive (false);
        if (audioSourceAmbient.isPlaying)
        {
            audioSourceAmbient.mute = true;
            Debug.Log("AmbientSource muted");
        }
        audioSourceTrigger.PlayOneShot(audioClip);
        if (cameraRig != null)
        {
            Transform currentTransform = cameraRig.transform;

            cameraRig.transform.position = Vector3.Lerp(cameraRig.transform.position, doorLookTransform.position, Time.deltaTime * transitionSpeed);

            Vector3 doorLookAngles = new Vector3(
                Mathf.LerpAngle(cameraRig.transform.rotation.eulerAngles.x, doorLookTransform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle(cameraRig.transform.rotation.eulerAngles.y, doorLookTransform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle(cameraRig.transform.rotation.eulerAngles.z, doorLookTransform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));

            cameraRig.transform.eulerAngles = doorLookAngles;

            yield return new WaitForSeconds(5);

            cameraRig.transform.position = Vector3.Lerp(doorLookTransform.transform.position, currentTransform.position, Time.deltaTime * transitionSpeed);

            Vector3 currentAngles = new Vector3(
                Mathf.LerpAngle(doorLookTransform.transform.rotation.eulerAngles.x, currentTransform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle(doorLookTransform.transform.rotation.eulerAngles.y, currentTransform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle(doorLookTransform.transform.rotation.eulerAngles.z, currentTransform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));

            cameraRig.transform.eulerAngles = currentAngles;
        }
        audioSourceAmbient.mute = false;
        Debug.Log("AmbientSource unmuted");
    }
    public void TriggerDoorActive() {
        doorIsActive = true;
		gameObject.SetActive (true);
	}
}
