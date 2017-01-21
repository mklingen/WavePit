using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveGenerator : MonoBehaviour {

    public GameObject soundwave;
    public float soundGenerateTime = 1.5f;
    public float currentTime = 0;
    public GameObject player;
    public ParticleSystem particles;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;

        if (currentTime > soundGenerateTime)
        {
            var sound = Instantiate(soundwave, gameObject.transform.position, gameObject.transform.rotation);
            sound.GetComponent<Soundwave>().player = player;
            sound.GetComponent<Soundwave>().source = gameObject;
            currentTime = 0;
            particles.Play();
        }
	}
}
