using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed;
    private GameObject player;

    // Use this for initialization
    void Start () {
        speed = -1.6f;
        player = GameObject.FindWithTag ("Player"); //only do this in Start or Awake      
    }

    // Update is called once per frame
    void Update () {
        TransitionManager.hurtPlayer = false;
        this.transform.position += new Vector3 (speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.tag == "PlatformZoneLeft" || other.gameObject.tag == "PlatformZoneRight") {
            speed *= -1;
        }
    }

    private void OnCollisionEnter2D (Collision2D other) {
        if (!TransitionManager.hurtPlayer) {
            if (other.gameObject.tag == "Player") {
                AudioManager.Instance.soundFxSource.PlayOneShot (AudioManager.Instance.soundFx[2]);
                TransitionManager.playerLives--;
            }
        }
    }

}