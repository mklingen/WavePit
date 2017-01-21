using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public GameObject player;
    public CharacterController controller;
    public float speed = 6.0f;
    public Vector3 velocity = Vector3.zero;
    public Vector3 cameraOffset = Vector3.zero;
    public float cameraSpeed = 0.05f;
	// Use this for initialization
	void Start () {
        controller = player.GetComponent<CharacterController>();
        cameraOffset = Camera.main.transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float curr_speed = Mathf.Min(moveDirection.magnitude * 10, speed);
        velocity = moveDirection.normalized * curr_speed;
        controller.SimpleMove(velocity);
        Vector3 currCamPos = (player.transform.position + cameraOffset) * cameraSpeed + Camera.main.transform.position * (1.0f - cameraSpeed);
        Camera.main.transform.position = currCamPos;
        Camera.main.transform.LookAt(player.transform.position);
	}
}
