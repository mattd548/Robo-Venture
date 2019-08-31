using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private Vector3 offset;
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player"); //only do this in Start or Awake    
		offset = this.transform.position - player.transform.position;
	}

	// Update is called once per frame
	void Update () {

	}

	// LateUpdate is called after Update each frame
	void LateUpdate () {
		// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
		this.transform.position = player.transform.position + offset;
	}
}