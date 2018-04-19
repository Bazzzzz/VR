using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public bool isAvailable = false;
    private Vector3 standardPosition;
    public GoblinController gc;
    private AudioSource audioSource;
    public AudioClip[] audioClips;
    // Use this for initialization
    void Start()
    {
        if (GetComponent<Rigidbody>().isKinematic == false)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        standardPosition = new Vector3(-22.29f, 12f, 38.46f);
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision coll)
    {
        Debug.Log("Ball collision with: " + coll.gameObject.name);
        //audioSource.Stop();
        if ("Floor".Equals(coll.gameObject.tag) && isAvailable)
        {
            isAvailable = true;
            playBounceSound();
        }
        if ("Terrain".Equals(coll.gameObject.tag))
        {
            isAvailable = false;
        }
    }

    private void playBounceSound()
    {
        //if (!audioSource.isPlaying)
        {

            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
    }

    //public IEnumerator CheckMovement()
    //{
    //    Debug.Log("Checking movement");
    //    Vector3 startPos = transform.position;
    //    yield return new WaitForSeconds(1f);
    //    Vector3 finalPos = transform.position;
    //    if (startPos.x != finalPos.x || startPos.y != finalPos.y
    //        || startPos.z != finalPos.z)
    //    {
    //        isMoving = true;
    //    } 
    //}

    // Update is called once per frame
    void Update()
    {
        if (gc != null)
        {
            if (!isAvailable && gc.isDead)
            {

                Debug.Log("Resetting ball..");
                transform.position = Vector3.MoveTowards(transform.position, standardPosition, 2);
                GetComponent<Rigidbody>().isKinematic = false;
                if (transform.position == standardPosition)
                {
                    isAvailable = true;
                }
            }
        }
        //StartCoroutine("CheckMovement");
    }
}
