using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PersonController : MonoBehaviour {

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

    private Text playerLives;
    private bool spike = true;
    private bool hit = true;

    private GameObject playerSpawner;

    private GameObject hitZone;

    // Use this for initialization
    void Start () {
        speed = 8f;
        rb = GetComponent<Rigidbody2D> ();
        jumpForce = 124;
        jump = false;
        facingRight = true;
        anim = GetComponent<Animator> ();
    }

    // Update is called once per frame - used for button/key presses
    void Update () {
        if (TransitionManager.Instance.getCurrentScene () != 0) {
            playerLives = GameObject.Find ("Canvas").transform.Find ("playerLives").GetComponent<Text> ();
            // playerLives.text = TransitionManager.playerLives.ToString ();
            updateLives ('0');
            playerSpawner = GameObject.Find ("spawnPlayer");
        }

        if (TransitionManager.Instance.getCurrentScene () == 8) {
            hitZone = GameObject.Find ("dronEnemy/hitZone");
        }
        if (TransitionManager.Instance.getCurrentScene () == 9 || TransitionManager.Instance.getCurrentScene () == 10) {
            playerLives.text = "";
        }

        horizontalMotion = Input.GetAxis ("Horizontal"); //get the horizontal motion, if any
        verticalMotion = Input.GetAxis ("Vertical"); //get the vertical motion, if any

        if (TransitionManager.Instance.getCurrentScene () != 0) {
            if (Input.GetKeyDown (KeyCode.Space)) {
                jump = true;
                AudioManager.Instance.soundFxSource.PlayOneShot (AudioManager.Instance.soundFx[0]);
            }
        }
    }

    void FixedUpdate () {
        if (jump && isGrounded) //space is pressed and the player isn't already jumping and the player is grounded
        {
            spike = true;
            hit = true;
            anim.SetBool ("isGrounded", false);
            rb.AddForce (new Vector2 (0, jumpForce)); //add the force to have the player jump    
        } else if (!isGrounded) {
            jump = false;
            anim.SetBool ("isGrounded", true);
        }
        if (horizontalMotion > 0f || horizontalMotion < 0f) //if the player is walking
        {
            spike = true;
            hit = true;
            anim.SetBool ("Idle", false); //control the animation - change the boolean in the animator
        } else {
            anim.SetBool ("Idle", true);
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
            AudioManager.Instance.soundFxSource.PlayOneShot (AudioManager.Instance.soundFx[1]);
            TransitionManager.Instance.NextScene ();
            // Destroy(GameObject.Find ("red-robot-game-sprites-vector-21861409_0"));
        }
        if (other.gameObject.tag == "PreviousScene") {
            TransitionManager.goingBack = true;
            TransitionManager.Instance.PreviousScene ();
        }
        if (other.gameObject.name == "spikeplatform") {
            if (spike) {
                spike = false;
                AudioManager.Instance.soundFxSource.PlayOneShot (AudioManager.Instance.soundFx[2]);
                updateLives ('-');
            }
        }
        // if (other.gameObject.tag == "DeathZone") {
        //     this.transform.position = playerSpawner.transform.position;
        //     updateLives ('-');
        // }

        if (TransitionManager.Instance.getCurrentScene () == 8) {
            if (other.gameObject.tag == "enemyHitZone" && hit) {
                AudioManager.Instance.soundFxSource.PlayOneShot (AudioManager.Instance.soundFx[6]);
                TransitionManager.flyingBossLives--;
                hit = false;
            }
        }
    }

    private void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.tag == "DeathZone") {
            AudioManager.Instance.soundFxSource.PlayOneShot (AudioManager.Instance.soundFx[2]);
            this.transform.position = playerSpawner.transform.position;
            updateLives ('-');
        }
    }

    private void updateLives (char option) {
        if (option == '+') {
            TransitionManager.playerLives++;
        } else if (option == '-') {
            TransitionManager.playerLives--;
        } else if (option == '0') { }

        playerLives.text = "";
        for (int i = 0; i < TransitionManager.playerLives; ++i) {
            playerLives.text += "❤︎ ";
        }

        if (TransitionManager.playerLives <= 0) {
            AudioManager.Instance.soundFxSource.PlayOneShot (AudioManager.Instance.soundFx[3]);
            restartGame ();
        }
    }

    private void restartGame () {
        TransitionManager.playerLives = 4;
        playerLives.text = "";
        TransitionManager.Instance.GoToScene (9);
        // Destroy(this.gameObject);
    }
}