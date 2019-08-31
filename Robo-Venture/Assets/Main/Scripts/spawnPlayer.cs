using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlayer : MonoBehaviour {

	public static GameObject instantiatedObj;

	// Use this for initialization
	void Start () {
		TransitionManager.player.transform.position = this.transform.position;
		// instantiatedObj = (GameObject) Instantiate (TransitionManager.player, this.transform.position, this.transform.rotation);
		// Destroy (instantiatedObj);
	}

	// Update is called once per frame
	void Update () { }

	// Update is called once per frame
	void FixedUpdate () { }
}