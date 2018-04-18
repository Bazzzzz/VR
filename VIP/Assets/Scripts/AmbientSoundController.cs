﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundController : MonoBehaviour {
    public AudioClip[] audioClips;
    private AudioSource source;

    private bool soundIsPlaying;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;
    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        soundIsPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!soundIsPlaying)
        {
            StartCoroutine("playAmbientSound");
        }
    }

    private IEnumerator playAmbientSound()
    {
        int randomNumber = Random.Range(0, audioClips.Length);
        soundIsPlaying = true;
        source.clip = audioClips[(int)randomNumber];


        float vol = Random.Range(volLowRange, volHighRange);

        //source.PlayOneShot(audioClips[randomNumber], vol);
        // Pick a random pitch to play it at
        int randomPitch = Random.Range(1, 10);
        source.pitch = (int)randomPitch;
        source.volume = vol;
        // Play the sound
        source.Play();

        float randomWaitSeconds = Random.Range(1, 5);

        yield return new WaitForSeconds(source.clip.length + randomWaitSeconds);
        soundIsPlaying = false;
    }
}
