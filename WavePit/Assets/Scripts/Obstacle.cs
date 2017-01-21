using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public float spinRate = 1.0f;
    public float radius = 1.0f;
    public float angle = 0;
    public float height = -0;
	// Use this for initialization
	void Start () {
        height = gameObject.transform.position.y;
        Vector2 pos2 = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
        angle = Mathf.Atan2(pos2.y, pos2.x);
        radius = pos2.magnitude;
    }
	
	// Update is called once per frame
	void Update () {
        angle += spinRate * Time.deltaTime;
        gameObject.transform.position = radius * new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) + new Vector3(0, height, 0);
        gameObject.transform.localRotation = Quaternion.Euler(0, -Mathf.Rad2Deg * angle, 0); 
	}
}
