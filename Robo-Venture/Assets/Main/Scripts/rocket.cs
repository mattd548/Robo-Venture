using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour {

	public float speed;
	public float rotateSpeed = 200f;
	private GameObject player;
	private Rigidbody2D rb;

	private GameObject explode;
	private GameObject explodeInstance;
	private GameObject explodeSpawner;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		speed = 3.5f;
		player = GameObject.FindWithTag ("Player"); //only do this in Start or Awake   

		explodeSpawner = GameObject.Find ("rocket/explosion");
		explode = GameObject.Find ("explode");
	}

	// Update is called once per frame
	void Update () {
		TransitionManager.hurtPlayer = false;
		//this.transform.position += new Vector3(speed * Time.deltaTime, 0,0);
		// this.transform.position = Vector3.MoveTowards (this.transform.position, player.transform.position, speed * Time.deltaTime);
	}

	private void FixedUpdate () {
		Vector2 direction = (Vector2) (player.transform.position - rb.transform.position);
		direction.Normalize ();
		float rotateAmount = Vector3.Cross (direction, transform.up).z;
		rb.velocity = transform.up * speed;
		rb.angularVelocity = -rotateAmount * rotateSpeed;
	}

	private void OnTriggerEnter2D (Collider2D other) {
		// if (other.gameObject.tag == "Player") {
		// 	AudioManager.Instance.soundFxSource.PlayOneShot (AudioManager.Instance.soundFx[2]);
		// 	AudioManager.Instance.soundFxSource.PlayOneShot (AudioManager.Instance.soundFx[4]);
		// 	explodeInstance = (GameObject) Instantiate (explode, this.transform.position, this.transform.rotation);
		// 	Destroy (explodeInstance, 0.4f);
		// 	TransitionManager.playerLives--;
		// }
	}

	private void OnCollisionEnter2D (Collision2D other) {
		if (!TransitionManager.hurtPlayer) {
			if (other.gameObject.tag == "Player") {
				AudioManager.Instance.soundFxSource.PlayOneShot (AudioManager.Instance.soundFx[2]);
				AudioManager.Instance.soundFxSource.PlayOneShot (AudioManager.Instance.soundFx[4]);
				explodeInstance = (GameObject) Instantiate (explode, this.transform.position, this.transform.rotation);
				Destroy (explodeInstance, 0.4f);
				TransitionManager.playerLives--;
				Destroy (this.gameObject);
			}
		}
	}
}