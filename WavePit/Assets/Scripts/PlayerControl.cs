using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public GameObject cameraTarget;
    public GameObject player;
    public CharacterController controller;
    public float speed = 6.0f;
    public Vector3 velocity = Vector3.zero;
    public float cameraHeight = 30.0f;
    public float cameraDist = 10.0f;
    public float cameraSpeed = 0.05f;
	// Use this for initialization
	void Start () {
        controller = player.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 moveDirection = Camera.main.transform.right * Input.GetAxis("Horizontal") + Camera.main.transform.forward * Input.GetAxis("Vertical");


        float curr_speed = Mathf.Min(moveDirection.magnitude * 5, speed);
        velocity = moveDirection.normalized * curr_speed;
        controller.SimpleMove(velocity);
        Vector3 cameraOffset = (player.transform.position - cameraTarget.transform.position).normalized * cameraDist;
        cameraOffset.y = cameraHeight;
        Vector3 currCamPos = (player.transform.position + cameraOffset) * cameraSpeed + Camera.main.transform.position * (1.0f - cameraSpeed);
        Camera.main.transform.position = currCamPos;
        Camera.main.transform.LookAt(0.5f * (cameraTarget.transform.position + player.transform.position));
	}

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "DeathZone")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else if (collision.transform.tag == "WinZone")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
