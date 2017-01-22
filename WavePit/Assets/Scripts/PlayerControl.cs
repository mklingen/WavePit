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
    public Animation runAnimation;
    public SpriteRenderer winScreen;
    public SpriteRenderer loseScreen;
    public Animator animator;
    public AudioClip dieClip;
    public AudioClip winClip;
    public AudioSource winLoseSource;
	// Use this for initialization
	void Start () {
        controller = player.GetComponent<CharacterController>();
        loseScreen.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        float right = Input.GetAxis("Horizontal");
        float up = Input.GetAxis("Vertical");

        if (Mathf.Abs(right) > 0.01f || Mathf.Abs(up) > 0.01f)
        {
            animator.SetFloat("AnimationSpeed", 1.0f);
        }
        else
        {
            animator.SetFloat("AnimationSpeed", 0.0f);
        }

        animator.SetBool("FlipAnimation", right < 0);

        Vector3 moveDirection = Camera.main.transform.right * right + Camera.main.transform.forward * up;

        float curr_speed = Mathf.Min(moveDirection.magnitude * speed, speed);
        velocity = moveDirection.normalized * curr_speed;
        controller.SimpleMove(velocity);
        Vector3 cameraOffset = (player.transform.position - cameraTarget.transform.position).normalized * cameraDist;
        cameraOffset.y = cameraHeight;
        Vector3 currCamPos = (player.transform.position + cameraOffset) * cameraSpeed + Camera.main.transform.position * (1.0f - cameraSpeed);
        Camera.main.transform.position = currCamPos;
        Camera.main.transform.LookAt(0.5f * (cameraTarget.transform.position + player.transform.position));
        gameObject.transform.LookAt(new Vector3(0, gameObject.transform.position.y, 0));
	}

    IEnumerator waitForDeath()
    {
        float time = 0;
        winLoseSource.clip = dieClip;
        winLoseSource.Play();
        while (time < 5 * 0.1f)
        {
            time += Time.deltaTime;
            loseScreen.gameObject.SetActive(true);
            Time.timeScale = 0.1f;
            yield return null;
        }
        Time.timeScale = 1.0f;
        loseScreen.gameObject.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    IEnumerator waitForWin()
    {
        winLoseSource.clip = winClip;
        winLoseSource.Play();
        float time = 0;
        FindObjectOfType<SoundWaveGenerator>().GetComponentInChildren<Animator>().SetTrigger("Hug");
        animator.gameObject.SetActive(false);
        while (time < 5 * 0.1f)
        {
            time += Time.deltaTime;
            winScreen.gameObject.SetActive(true);
            Time.timeScale = 0.1f;
            yield return null;
        }
        Time.timeScale = 1.0f;
        winScreen.gameObject.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "DeathZone")
        {
            StartCoroutine("waitForDeath");
        }
        else if (collision.transform.tag == "WinZone")
        {
            StartCoroutine("waitForWin");
        }
    }
}
