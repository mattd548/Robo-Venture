using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Rigidbody2D rb;
	public float jumpForce;
	public bool jump;
	private bool facingRight;
	private bool isWalking;
	public Transform groundCheck;
	private Animator anim;
	public LayerMask groundLayers;
	public bool isGrounded;

	private float horizontalMotion, verticalMotion;

	// Use this for initialization
	void Start () {
		speed = 2f;
		rb = GetComponent<Rigidbody2D> ();
		jumpForce = 170;
		jump = false;
		facingRight = true;
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame - used for button/key presses
	void Update () {
		horizontalMotion = Input.GetAxis ("Horizontal"); //get the horizontal motion, if any
		verticalMotion = Input.GetAxis ("Vertical"); //get the vertical motion, if any
		if (Input.GetKeyDown (KeyCode.Space)) {
			jump = true;
		}
	}

	void FixedUpdate () {
		if (TransitionManager.Instance.getCurrentScene () > 0) {
			if (jump && isGrounded) //space is pressed and the player isn't already jumping and the player is grounded
			{
				rb.AddForce (new Vector2 (0, jumpForce)); //add the force to have the player jump            
			} else if (!isGrounded) {
				jump = false;
			}
			if (horizontalMotion > 0f || horizontalMotion < 0f) //if the player is walking
			{
				anim.SetBool ("isWalking", true); //control the animation - change the boolean in the animator
			} else {
				anim.SetBool ("isWalking", false);
			}
			//the following is for flipping the player sprite so it is always facing the right direction
			if (horizontalMotion < 0 && facingRight) //need to change direction
			{
				Flip ();
			} else if (horizontalMotion > 0 && !facingRight) {
				Flip ();
			}
			Vector3 motion = new Vector3 (speed * horizontalMotion * Time.deltaTime, 0, 0); //standard for vector motion (this is only for x axis)
			this.transform.position = this.transform.position + motion;
			isGrounded = Physics2D.OverlapCircle (groundCheck.position, .05f, groundLayers);

			if (isGrounded) {
				anim.SetBool ("isGrounded", true);
				// anim.SetBool ("isJumping", false);
				Debug.Log ("isGrot: " + isGrounded);
			} else {
				anim.SetBool ("isGrounded", false);
				// anim.SetBool ("isJumping", true);
				Debug.Log ("isGro:F " + isGrounded);
			}

			// Destroy Player if isDying == true
			if ((gameObject.transform.rotation.eulerAngles.z < -150 || gameObject.transform.rotation.eulerAngles.z > 150) && isGrounded) {
				anim.SetBool ("isDying", true);
				Destroy (gameObject, 3.0f);
				// Consider using yield return new WaitForSeconds (2);
			}
		}
	}

	void Flip () {
		facingRight = !facingRight;
		Vector3 playerScale = this.transform.localScale;
		playerScale.x *= -1;
		this.transform.localScale = playerScale;
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "NextScene") {
			TransitionManager.goingBack = false;
			TransitionManager.Instance.NextScene ();
			// Destroy(GameObject.Find ("red-robot-game-sprites-vector-21861409_0"));
		}

		if (other.gameObject.tag == "PreviousScene") {
			TransitionManager.goingBack = true;
			TransitionManager.Instance.PreviousScene();
		}
	}

}