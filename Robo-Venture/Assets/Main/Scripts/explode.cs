using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player"); //only do this in Start or Awake        
	}

	// Update is called once per frame
	void Update () { 
	}

	private void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Player") {
			Destroy (this.gameObject);
		}
	}
}