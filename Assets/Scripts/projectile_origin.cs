﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_origin : MonoBehaviour {

	public GameObject projectile; //projectile prefab
	public float fire_rate;
	public float projectile_life;
	private string PLAYER_GO_NAME;
	private GameObject project;

	// Use this for initialization
	void Start () {
		InvokeRepeating("FireProjectile", 1f, fire_rate);
		PLAYER_GO_NAME = "character";
	}
	

	// Update is called once per frame
	void Update () {
		//FireProjectile(); bc it fires waaaaay too many times
	}

	
	//function to fire a projectile
	void FireProjectile() {
		GameObject character = GameObject.Find(PLAYER_GO_NAME);
		if (character != null) { //if the player isn't dead
			//instantiate projectile
			project = (GameObject)Instantiate(projectile);
			//initial position
			project.transform.position = transform.position;
			//compute direction towards player
			Vector2 direction = character.transform.position - project.transform.position;
			//set projectile direction
			project.GetComponent<projectile_movement>().SetDirection(direction);
			//set code to destroy projectile after it travels projectile_life seconds
			Destroy(project, projectile_life);
		}
	}
	
	
	
}
