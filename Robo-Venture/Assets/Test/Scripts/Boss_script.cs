using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_script : MonoBehaviour {

    public float speed = 200f;
    public float jumpForce = 550f;
    private bool isGrounded = false;
    public Transform groundCheck, spawnPoint;
    public LayerMask groundLayers;
    private float groundCheckRadius = .2f;
    private bool isFacingRight, idle;
    public GameObject[] plats;
    bool jump, attack;
    private int score;
    public Rigidbody2D bullet;
    private Animator anim;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    private float bulletspeed = 10f;

    private GameObject player;

    // Use this for initialization
    void Start () {
        //initialize your stuffs here.
        anim = GetComponent<Animator> ();
        isFacingRight = true;
        idle = true;
        anim.SetBool ("idle", true);
        score = 0;
        // player = GameObject.Find ("Player");
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown ("Jump")) {
            jump = true;
        }

        if (Input.GetKeyDown (KeyCode.R)) {
            attack = true;
            //anim.Play("roll attack");
            //transform.Translate(Vector3.right);
        }
        if (Input.GetKeyDown (KeyCode.G)) {
            Fire ();
            //Rigidbody2D bulletInstance = Instantiate (bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
            //bulletInstance.velocity = new Vector2 (speed, 0); 
            //transform.TransformDirection(new Vector3(0, 0, speed)); //transform.forward * speed;

        }
    }

    void FixedUpdate () {
        isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundLayers);
        anim.SetBool ("isGrounded", isGrounded);
        if (jump) {
            if (isGrounded) {
                this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (this.GetComponent<Rigidbody2D> ().velocity.x, 0);
                this.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
            } else {
                jump = false;
            }
        }
        float moveX = Input.GetAxis ("Horizontal");
        //anim.SetFloat ("speed", moveX);
        //float moveY = Input.GetAxis ("Vertical"); needed for non jumping
        Vector2 moving = new Vector2 (moveX * speed * Time.deltaTime, GetComponent<Rigidbody2D> ().velocity.y);
        if (moveX == 0f) { //REMEMBER THE f!!
            idle = true;
        } else {
            idle = false;
        }
        anim.SetBool ("idle", idle);
        if ((moveX > 0.0f && !isFacingRight) || (moveX < 0.0f && isFacingRight)) {
            Flip ();
        }
        anim.SetBool ("isAttacking", attack);
        if (attack) {
            //transform.Translate(Vector3.right);
            Vector2 move = new Vector2 (speed * Time.deltaTime, GetComponent<Rigidbody2D> ().velocity.y);
        } else {
            attack = false;
        }
        //Vector2 moving = new Vector2 (moveX * maxSpeed, moveY * maxSpeed); //updates both x and y (for non jumping
        GetComponent<Rigidbody2D> ().velocity = moving;

    }

    void Flip () {
        isFacingRight = !isFacingRight;
        Vector3 playerScale = this.transform.localScale;
        playerScale.x *= -1;
        this.transform.localScale = playerScale;
    }

    void Fire () {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject) Instantiate (
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        //    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletspeed, 0);
        //transform.TransformDirection(new Vector3(0, 0, bulletspeed));
        //// Destroy the bullet after 2 seconds
        //Destroy(bullet, 3.0f);

        // bullet.transform.position = Vector3.MoveTowards (bullet.transform.position, player.transform.position, bulletspeed * Time.deltaTime);

    }

    void OnTriggerEnter2D (Collider2D other) {
        Debug.Log ("I hit something!");
        if (other.gameObject.tag == "Surprise") {
            Rigidbody2D rb = plats[0].GetComponent<Rigidbody2D> ();
            rb.isKinematic = false;
            rb.gravityScale = 1;
        }
        if (other.gameObject.tag == "Collectible") {
            score++;
            Destroy (other.gameObject);
            Debug.Log ("The score is now " + score);
        }

    }

    void OnCollisionEnter2D (Collision2D collision) {
        Debug.Log ("There has been a collision!");
        if (collision.gameObject.layer == 9) {
            Debug.Log ("I hit a platform!");
            this.transform.parent = collision.transform;
        }
        if (collision.gameObject.tag == "Enemy") {
            //Debug.Log("You hit the soccer ball!");
            //Let's destroy what we collided with - enemy
            //Destroy(this.gameObject);    suicide :(
            GetComponent<Rigidbody2D> ().velocity = Vector2.zero; //stop all motion from previous "life"
            this.transform.position = spawnPoint.position;
        }
    }

    void OnCollisionExit2D (Collision2D collision) {
        if (collision.gameObject.layer == 9) {
            Debug.Log ("I am leaving a platform!");
            this.transform.parent = null;
        }
    }
}