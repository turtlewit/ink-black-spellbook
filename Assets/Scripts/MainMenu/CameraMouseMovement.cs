using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseMovement : MonoBehaviour {
	
	public float movement_ammount;
	Vector3 initial_position;
	// Use this for initialization
	void Start () {
		initial_position = new Vector3((float) (transform.position.x - (.5 * movement_ammount)),(float) (transform.position.y - (.5 * movement_ammount)), transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {

		float percent_x = Input.mousePosition.x / Screen.width;
		float percent_y = Input.mousePosition.y / Screen.height;
		transform.position = new Vector3(initial_position.x + (movement_ammount * percent_x), initial_position.y + (movement_ammount * percent_y), transform.position.z);
	}
}
