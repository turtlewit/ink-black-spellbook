using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class boss_state : MonoBehaviour {

	//**********************************
	// stuff for managing the state the boss is in
	public enum States {
	IDLE,
	MOVE,
	ATTACK_SHOOT,
	ATTACK_HIT,
	DEATH
	};

	private States state_now;
	//*******************************
	
	// Basic gameplay
	public int boss_max_health = 100;
	public string destination_tag = "waypoint";
	public float action_duration = 20;
	public float action_timer;
	private int boss_health;
	private GameObject[] waypoints;

	// Use this for initialization
	void Start () {
		boss_health = boss_max_health;
		action_timer = action_duration;
		//possible destinations existing in the scene
		waypoints = GameObject.FindGameObjectsWithTag(destination_tag);
		state_now = States.IDLE;
	}
	
	// Update is called once per frame
	void Update () {
		//constantly check if the boss is losing health
		if (boss_health==0) {
			state_now = States.DEATH;
		}
			
		//these things should happen and everything but move should occupy some time
		if (state_now==States.MOVE&&action_timer<=0) {
			move_state();
			//reset the timer so actions after move_state will take time again
			action_timer = action_duration;
		} else if (state_now==States.IDLE) {
			idle_state();
		} else if (state_now==States.ATTACK_SHOOT) {
			shoot_state();
		} else if (state_now==States.ATTACK_HIT) {
			hit_state();
		} else if (state_now==States.DEATH) {
			death();
		}
		action_timer -= Time.deltaTime;
		Debug.Log(state_now);
		/*
		switch (state_now) {
			case States.IDLE : idle_state(); break;
			case States.MOVE : move_state(); break;
			case States.ATTACK_SHOOT : shoot_state(); break;
			case States.ATTACK_HIT : hit_state(); break;
			case States.DEATH : death(); break;
			default : break;
		}
		*/
		
		
	}
	
	//******************************
	// Code for each state the boss is in
	
	void idle_state() {
		//logic for when the boss is vulnerable
		
		state_now = States.MOVE;
	}
	
	void move_state() {
		//choose a random action for after movement: idle, shoot, hit
		int random_choice = random_val(3);
		if (random_choice==0) {
			state_now = States.IDLE;
		} else if (random_choice==1) {
			state_now = States.ATTACK_SHOOT;
		} else if (random_choice==2) {
			state_now = States.ATTACK_HIT;
		}

		//teleport to the next destination, determined by random waypoints
		//case: direct attack, should teleport directly to the player
		if (state_now==States.ATTACK_HIT) { 
			GameObject player_object = GameObject.FindWithTag("Player");
			Vector3 move_dest = player_object.transform.position;
			float add_width = player_object.GetComponent<BoxCollider2D>().size.x;
			transform.position = new Vector3(move_dest.x+add_width, move_dest.y, move_dest.z);
		} else {
			transform.position = waypoints[random_val(3)].transform.position;
		}
		
	}
	
	void shoot_state() {
		//logic for when the boss aims and shoots at the player
		
		state_now = States.MOVE;
	}
	
	void hit_state() {
		//logic for when the boss gets up close to the player
		
		state_now = States.MOVE;
	}
	
	void death() {
		//logic for when the boss' health is gone
	}
	//***********************************
	
	
	
	//*******************************
	// Helper functions
	
	int random_val (int max_num) {
		//return random value
		System.Random random = new System.Random();
		int randomNumber = random.Next(0, max_num);
		return randomNumber;
	}
	
	//*********************
	
}
