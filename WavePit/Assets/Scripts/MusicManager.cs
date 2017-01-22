using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioClip intro2d;
    public AudioClip loop2d;
    public AudioClip loopYeti;
    public AudioSource yetiSource;
    public AudioSource audioSource;
    public bool playingIntro = true;

	// Use this for initialization
	void Start () {
        playingIntro = true;
        audioSource.clip = intro2d;
        audioSource.loop = false;
        audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
	    if (playingIntro && !audioSource.isPlaying)
        {
            playingIntro = false;
            audioSource.clip = loop2d; 
            audioSource.loop = true;
            audioSource.Play();
             
            yetiSource.clip = loopYeti;
            yetiSource.loop = true;
            yetiSource.Play();
        }
	}
}
