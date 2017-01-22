using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {


	AudioSource steps;
	bool isPlaying;
	bool isJumping; 
	bool Grounded;

	public AudioClip[] footsteps1;


	void Start () {

		steps = GetComponent<AudioSource> ();


	}


	void Update () {

		RaycastHit hit;
		Ray landingRay = new Ray(transform.position, Vector3.down);



		if (Input.GetAxis ("Horizontal") > 0.1f && steps.isPlaying == false && isPlaying == false)
		{




			StartCoroutine("FootStep");


		}



		if ( Input.GetAxis( "Horizontal" ) < -0.1f && steps.isPlaying == false && isPlaying == false)
		{

			StartCoroutine("FootStep");

		}



		if ( Input.GetAxis( "Vertical" ) > 0.1f && steps.isPlaying == false && isPlaying == false)
		{

			StartCoroutine("FootStep");

		}


		if ( Input.GetAxis( "Vertical" ) < -0.1f && steps.isPlaying == false && isPlaying == false)
		{

			StartCoroutine("FootStep");
		}
			

	}





	IEnumerator FootStep(){


			isPlaying = true;


				steps.clip = footsteps1 [Random.Range (0, footsteps1.Length)];
				steps.pitch = Random.Range (0.7f, 0.8f);
				steps.volume = 0.8f;
				steps.Play ();


			yield return new WaitForSeconds (0.31f);
			isPlaying = false;
		}

	}







