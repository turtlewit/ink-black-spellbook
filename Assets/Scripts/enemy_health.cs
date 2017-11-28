using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_health : MonoBehaviour {

	public int enemy_max_health;
	public float rad_of_damage;
	public string attack_tag = "Player";
	public int enemy_current_health;
	public int time_betw_damage;

	// Use this for initialization
	void Start () {
		enemy_current_health = enemy_max_health;
		InvokeRepeating("CheckDamage", 1f, time_betw_damage);
	}
	
	// Update is called once per frame
	void Update () {
		if (enemy_current_health<=0) {
			Destroy(gameObject);
		}
		/*
		RaycastHit2D hit = Physics2D.Raycast(transform.position);
		if (hit.gameObject.tag == "Player" && hit.distance <0.5f) {
			DecreaseHealth();
		}
		*/		
	}
	/*
	void OnCollisionEnter(Collision other) {
		if(other.gameObject.tag == attack_tag) {
			DecreaseHealth();
		}
	}
	*/
	
	void CheckDamage() {
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, rad_of_damage);
		foreach (Collider2D c in colliders) {
			if(c.tag==attack_tag) {
				DecreaseHealth();
			}
		}
	}
	
	void DecreaseHealth() {
		Debug.Log(enemy_current_health);
		enemy_current_health -= 1;
	}
}
