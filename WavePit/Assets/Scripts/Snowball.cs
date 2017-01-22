using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{

    public Vector3 target;
    public Vector3 velocity;
    public float timeToHit;
    public float extraHeight;
    public float g;
    // Use this for initialization
    void Start()
    {
        Vector3 diff = target - gameObject.transform.position;
        Vector2 diff2d = new Vector2(diff.x, diff.z);
        float vel2d = diff2d.magnitude / timeToHit;
        diff2d = diff2d.normalized;
        g = -4 * (gameObject.transform.position.y - 2 * extraHeight + target.y) / (timeToHit * timeToHit);
        float vy = (-3 * gameObject.transform.position.y - 4 * extraHeight + target.y) / (timeToHit);
        velocity = new Vector3(vel2d * diff2d.x, -vy, vel2d * diff2d.y);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += velocity * Time.deltaTime;
        velocity -= Vector3.up * Time.deltaTime * g;
        if (gameObject.transform.position.y < -30)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Yes");
        Debug.Log(collision.gameObject.tag);
        Debug.DrawLine(gameObject.transform.position, collision.gameObject.transform.position);
        if (collision.gameObject.tag == "Obstacle")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
