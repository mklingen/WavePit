﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundwave : MonoBehaviour {
    public GameObject source;
    public GameObject player;
    public float growthRate = 0.1f;
    public float maxLife = 1.0f;
    public float currentLife = 0.0f;
    public float currentSize = 0;
    public int raycastLayer = 0;
    public float pushForce = 10;
    public bool pushingplayer = false;
    public float playerdist = 0;
    public Vector3 currentPushForce = Vector3.zero;
    public Material psychoMaterial;
    public AudioSource splashSource;
    public ParticleSystem splashParticles;


    // Use this for initialization
    void Start () {
        currentLife = maxLife;
	}
	
	// Update is called once per frame
	void Update () {
        currentSize += (growthRate) * Time.deltaTime;
        gameObject.transform.localScale = Vector3.one * currentSize;
        currentLife -= Time.deltaTime;
        psychoMaterial.SetFloat("_timeOffset", Time.realtimeSinceStartup);
        psychoMaterial.SetFloat("_waveRadius", currentSize);
        if (currentLife < 0)
        {
            Destroy(gameObject);
        }
        playerdist = (player.transform.position - source.transform.position).magnitude;
        RaycastHit hitInfo;
        int mask = 1 << raycastLayer;
        if (Mathf.Abs(playerdist - currentSize * 0.5f) < 5.0f)
        {
            Vector3 dir = player.transform.position - source.transform.position;
            dir = dir.normalized;
            Vector3 start = source.transform.position;
            Vector3 end = player.transform.position;
            bool rayCastSuccess = Physics.Raycast(start, dir, out hitInfo, (start - end).magnitude, mask);

            if (rayCastSuccess)
            {
                if (!splashSource.isPlaying)
                {
                    splashSource.Play();
                    splashSource.transform.position = hitInfo.point;
                    splashParticles.transform.position = hitInfo.point;
                    splashParticles.Play();
                    Debug.DrawRay(start, hitInfo.point, Color.red, 1.0f, true);
                }
            }
            else
            {
                pushingplayer = true;
                Vector3 moveForce = player.transform.position - source.transform.position;
                moveForce *= (1.0f / (moveForce.magnitude));
                moveForce *= pushForce;
                pushForce *= 0.9f;
                moveForce.y += pushForce * 0.005f;
                currentPushForce += moveForce * Time.deltaTime;
                player.GetComponent<CharacterController>().Move(currentPushForce * Time.deltaTime);
                currentPushForce *= 0.9f;
            }
        }
	}
}
