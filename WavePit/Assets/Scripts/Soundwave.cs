using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundwave : MonoBehaviour {
    public GameObject source;
    public GameObject player;
    public float growthRate = 0.1f;
    public float maxLife = 1.0f;
    public float currentLife = 1.0f;
    public float currentSize = 1;
    public int raycastLayer = 0;
    public float pushForce = 10;
    public bool pushingplayer = false;
    public float playerdist = 0;
    public Vector3 currentPushForce = Vector3.zero;
	// Use this for initialization
	void Start () {
        currentLife = maxLife;
	}
	
	// Update is called once per frame
	void Update () {
        currentSize += (growthRate) * Time.deltaTime;
        gameObject.transform.localScale = Vector3.one * currentSize;
        currentLife -= Time.deltaTime;

        if (currentLife < 0)
        {
            Destroy(gameObject);
        }
        playerdist = (player.transform.position - source.transform.position).magnitude;
        RaycastHit hitInfo;
        int mask = 1 << raycastLayer;
        if (Mathf.Abs(playerdist - currentSize * 0.5f) < 5.0f &&
            !Physics.Raycast(source.transform.position, player.transform.position - source.transform.position, out hitInfo, currentSize * 0.5f, mask))
        {
            pushingplayer = true;
            Vector3 moveForce = player.transform.position - source.transform.position;
            moveForce = moveForce.normalized;
            moveForce *= pushForce;
            pushForce *= 0.5f;
            currentPushForce += moveForce * 0.1f;
            player.GetComponent<CharacterController>().Move(currentPushForce * Time.deltaTime);
        }
	}
}
