using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveGenerator : MonoBehaviour {

    public GameObject soundwave;
    public float soundGenerateTime = 1.5f;
    public float currentTime = 0;
    public GameObject player;
    public ParticleSystem particles;
    public Material psychoMaterial;
    public ParticleSystem snow;
    public AudioClip hit1;
    public AudioClip hit2;
    public AudioClip hit3;
    public List<AudioClip> hits;
    public bool playedSound = false;
    public float hitSoundWarmupTime;
    public AudioSource splashSource;
    public ParticleSystem splashParticles;
    public GameObject snowBall;


    public enum Mode
    {
        GenerateSounds,
        ThrowSnowballs
    }

    public Mode mode = Mode.GenerateSounds;

    // Use this for initialization
    void Start () {
        hits = new List<AudioClip>() { hit1, hit2, hit3 };
	}
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;
        GetComponent<WindZone>().windMain *= 0.9f;

        if (currentTime > soundGenerateTime - hitSoundWarmupTime && !playedSound)
        {
            var source = GetComponent<AudioSource>();
            source.clip = hits[(int)Random.Range(0, 3)];
            source.Play();
            playedSound = true;
        }
        if (currentTime > soundGenerateTime)
        {
            if (mode == Mode.GenerateSounds)
            {
                GetComponent<WindZone>().windMain = -1000;
                var sound = Instantiate(soundwave, gameObject.transform.position, gameObject.transform.rotation);
                sound.GetComponent<Soundwave>().player = player;
                sound.GetComponent<Soundwave>().source = gameObject;
                sound.GetComponent<Soundwave>().psychoMaterial = psychoMaterial;
                sound.GetComponent<Soundwave>().splashSource = splashSource;
                sound.GetComponent<Soundwave>().splashParticles = splashParticles;
                mode = Mode.ThrowSnowballs;
                particles.Play();
            }
            else
            {
                var ball = Instantiate(snowBall, gameObject.transform.position, gameObject.transform.rotation);
                ball.GetComponent<Snowball>().target = player.transform.position;
                mode = Mode.GenerateSounds;
            }
            currentTime = 0;
            playedSound = false;
        }
	}
}
