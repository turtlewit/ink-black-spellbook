using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_movement : MonoBehaviour {

	float speed;
	Vector2 _direction;
	bool isReady;
	
	//set default values
	void Awake() {
		speed = 5f;
		isReady = false;
		
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	//set bullet's direction
	public void SetDirection(Vector2 direction) {
		//set direction normalized to get a unit vector
		_direction = direction.normalized;
		isReady = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isReady) {
			//get bullet's current position
			Vector2 position = transform.position;
			
			//compute new position
			position += _direction*speed*Time.deltaTime;
			transform.position = position;
			
			//remove bullet from game if it goes out of screen
			Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
			Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
			
			//if bullet goes outside, then destroy it
		if((transform.position.x < min.x) || (transform.position.y > max.x) ||
			transform.position.y < min.y || (transform.position.y > max.y)) {
				Destroy(gameObject);
			}
		}
	}
}
