using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public float speed;
	private GameObject player;

	// Use this for initialization
	void Start () {
		speed = 1.0f;
		player = GameObject.FindWithTag ("Player"); //only do this in Start or Awake        
	}

	// Update is called once per frame
	void Update () {
		TransitionManager.hurtPlayer = false;
		//this.transform.position += new Vector3(speed * Time.deltaTime, 0,0);
		this.transform.position = Vector3.MoveTowards (this.transform.position, player.transform.position, speed * Time.deltaTime);
	}

	private void OnTriggerEnter2D (Collider2D other) {
		// if (other.gameObject.tag == "Player") {
		// 	TransitionManager.playerLives--;
		// }
	}

	private void OnCollisionEnter2D (Collision2D other) {
		if (!TransitionManager.hurtPlayer) {
			// if (other.gameObject.tag == "Player") {
			// 	TransitionManager.playerLives--;
			// }
		}
	}

}