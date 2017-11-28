using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flying_enemy_move : MonoBehaviour {

	private CharacterController thePlayer;
	public float playerRange;
	public int moveSpeed;
	//public LayerMask playerLayer;
	public bool playerInRange;
	
	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		playerInRange = Physics2D.OverlapCircle(transform.position, playerRange);
		if (playerInRange) {
			transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed*Time.deltaTime);
		}
		//target = GameObject.FindWithTag("Player").transform;
		//Vector3 targetHeading = target.position - transform.position;
		//Vector3 targetDirection = targetHeading.normalized;
		//transform.rotation = Quaternion.LookRotation(targetDirection);
		//transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		//enemyTransform.position += enemyTransform.forward*speed*Time.deltaTime;
		/*transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position
			-transform.position), rotationSpeed*Time.deltaTime);
		
		transform.position += transform.forward*Time.deltaTime*moveSpeed;
		
		GameObject character = GameObject.Find(PLAYER_NAME);
		if(playerInRange) {
			transform.position = Vector3.MoveTowards(transform.position, character.transform.position, moveSpeed*Time.deltaTime);	
		}
		*/
	
	}
	
}
