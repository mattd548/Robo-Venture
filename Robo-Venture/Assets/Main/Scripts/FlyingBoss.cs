using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingBoss : MonoBehaviour {
	public float speed;
	public float rotateSpeed = 200;
	private Rigidbody2D rb;
	private bool isFacingRight;

	private GameObject bullet;
	private GameObject bulletInstance;
	private GameObject bulletSpawner;

	private GameObject explode;
	private GameObject explodeInstance;

	public float spawnDelay; //amount of time to wait until you spawn again
	private float nextSpawnTime; //when is the next time I should spawn a explosion?
	private float duration; //how long will the explosion stay in the game

	private System.Random rng = new System.Random ();
	private GameObject player;

	private Text flyingBossLives;

	// Use this for initialization
	void Start () {
		speed = 1.7f;
		rb = GetComponent<Rigidbody2D> ();
		isFacingRight = true;
		player = GameObject.FindWithTag ("Player"); //only do this in Start or Awake 

		bulletSpawner = GameObject.Find ("dronEnemy/bulletSpawn");
		bullet = GameObject.Find ("rocket");

		explode = GameObject.Find ("explode2");

		spawnDelay = 4.0f;
		duration = 0.4f;

		AudioManager.Instance.musicSource.Stop ();
		AudioManager.Instance.musicSource.clip = AudioManager.Instance.music[3];
		AudioManager.Instance.musicSource.Play ();
	}

	// Update is called once per frame
	void Update () {
		flyingBossLives = GameObject.Find ("Canvas").transform.Find ("enemyLives").GetComponent<Text> ();
		// playerLives.text = TransitionManager.playerLives.ToString ();

		if (TransitionManager.Instance.getCurrentScene () == 8) {
			updateLives ('0');
		} else {
			flyingBossLives.text = "";
		}

		//this.transform.position += new Vector3(speed * Time.deltaTime, 0,0);
		this.transform.position = Vector3.MoveTowards (this.transform.position, player.transform.position, speed * Time.deltaTime);

		int spawn = rng.Next (0, 115);
		if (spawn == 0) {
			Fire ();
		}

		if (bulletInstance != null) {
			explodeInstance = (GameObject) Instantiate (explode, bulletInstance.transform.position, bulletInstance.transform.rotation);
			Destroy (explodeInstance, 0.4f);
		}
	}

	// private void FixedUpdate () {
	// 	Vector2 direction = (Vector2) (player.transform.position);
	// 	direction.Normalize ();
	// 	float rotateAmount = Vector3.Cross (direction, transform.up).z;
	// 	rb.velocity = transform.up * speed;
	// 	rb.angularVelocity = -rotateAmount * rotateSpeed;
	// }

	private void OnTriggerEnter2D (Collider2D other) { }

	private void OnCollisionEnter2D (Collision2D other) {
		// if (other.gameObject.name == "Player") {
		// 	TransitionManager.playerLives--;
		// }
	}

	void Flip () {
		isFacingRight = !isFacingRight;
		Vector3 playerScale = this.transform.localScale;
		playerScale.x *= -1;
		this.transform.localScale = playerScale;
	}

	void Fire () {
		// Create the Bullet from the Bullet Prefab
		bulletInstance = (GameObject) Instantiate (bullet, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
		AudioManager.Instance.soundFxSource.PlayOneShot (AudioManager.Instance.soundFx[5]);
		// Destroy the bullet after 4 seconds
		Destroy (bulletInstance, 4.0f);
		AudioManager.Instance.soundFxSource.PlayOneShot (AudioManager.Instance.soundFx[4], 4.0f);

		explodeInstance = (GameObject) Instantiate (explode, bulletInstance.transform.position, bulletInstance.transform.rotation);
		Destroy (explodeInstance, 0.4f);
		nextSpawnTime = Time.time + spawnDelay;
	}

	private void updateLives (char option) {
		if (option == '+') {
			TransitionManager.flyingBossLives++;
		} else if (option == '-') {
			TransitionManager.flyingBossLives--;
		} else if (option == '0') { }

		flyingBossLives.text = "";
		for (int i = 0; i < TransitionManager.flyingBossLives; ++i) {
			flyingBossLives.text += "◉ ";
		}

		if (TransitionManager.flyingBossLives <= 0) {
			Destroy (this.gameObject);
			TransitionManager.Instance.GoToScene (10);
		}
	}

}