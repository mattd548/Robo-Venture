using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

        public float speed;
        private GameObject player;

        // Use this for initialization
        void Start () {
                speed = .5f;
                player = GameObject.Find ("Player"); //only do this in Start or Awake        
        }

        // Update is called once per frame
        void Update () {
                //this.transform.position += new Vector3(speed * Time.deltaTime, 0,0);
                this.transform.position = Vector3.MoveTowards (this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
}