using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseMovement : MonoBehaviour {
	
	public float movement_ammount;	// Must be set in unity inspector
	
	Vector3 initial_position; // Offset starting position of the camera.

	// Use this for initialization
	void Start () {

		// Take the x and y values of the current camera position
		// and offset them by 1/2 of the maximum camera offset.
		// This allows the camera to stay in it's initial place
		// when the mouse is in the center of the screen.

		float x = (float)(transform.position.x - (.5 * movement_ammount));
		float y = (float)(transform.position.y - (.5 * movement_ammount));
		initial_position = new Vector3(
			x,
			y, 
			transform.position.z); // No need to change Z
	}
	
	// Update is called once per frame
	void Update () {
		
		// Convert the mouse position into a percent value.
		// This value is used to determine what percent
		// of the offset will be applied to the camera this frame
		// based on the position of the mouse.

		float percent_x = Input.mousePosition.x / Screen.width;
		float percent_y = Input.mousePosition.y / Screen.height;
		
		// Take the initial position (which has been offset 
		// by 1/2 of movement_ammount) and add the percent 
		// value we just calculated.

		float x = initial_position.x + (movement_ammount * percent_x);
		float y = initial_position.y + (movement_ammount * percent_y);

		// Set the camera's position to the newly calculated values

		transform.position = new Vector3(
			x, 
			y, 
			transform.position.z);

		// The result is that the maximum distance that the camera
		// can be offset is exactly movement_ammount.
	}
}
