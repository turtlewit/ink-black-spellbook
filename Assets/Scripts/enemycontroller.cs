using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemycontroller : MonoBehaviour {

    public Transform player;
    public float max_health = 30.0f;
    public float move_speed = .01f;
    int min_dist = 10;
    int max_dist = 10;

    private Vector3 smoothVelocity = Vector3.zero;
    public float walkingdistance = 1.0f;
    public float smoothTime = 10.0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //follow();
		
	}

    void follow()
    {
       

        transform.LookAt(player);
        float distance = Vector3.Distance(transform.position, player.position);

        if(distance <= walkingdistance)
        {
            transform.position = Vector3.SmoothDamp(transform.position, player.position, ref smoothVelocity, smoothTime);
        }
        //Debug.Log("HERE1");
        //if(Vector3.Distance(transform.position, player.position) >= min_dist)
        //{
        //    Debug.Log("HERE2");
        //    transform.position += transform.forward * move_speed * Time.deltaTime;
        //}

    }
    void damage(float amount)
    {

        max_health -= amount;
        if (max_health <= 0.0)
            Destroy(gameObject);
    }
}
