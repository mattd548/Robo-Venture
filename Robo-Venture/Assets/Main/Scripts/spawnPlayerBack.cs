using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlayerBack : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (TransitionManager.goingBack) {
			GameObject.Find ("spawnPlayer").SetActive (false);
			GameObject.Find ("spawnPlayer").SetActive (false);
		} else {
			GameObject.Find ("spawnPlayer").SetActive (true);
			GameObject.Find ("spawnPlayer").SetActive (true);
		}

		TransitionManager.player.transform.position = this.transform.position;
	}

	// Update is called once per frame
	void Update () { }

	// Update is called once per frame
	void FixedUpdate () { }
}