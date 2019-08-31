using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetGame2 : MonoBehaviour {
	// Use this for initialization
	void Start () { }

	// Update is called once per frame
	void Update () {
		// Go to TitleScene
		if (Input.GetKeyDown (KeyCode.E)) {
			AudioManager.Instance.musicSource.Stop ();
			AudioManager.Instance.musicSource.clip = AudioManager.Instance.music[0];
			AudioManager.Instance.musicSource.Play ();
			resetGame ();
			AudioManager.firstTime = true;
			TransitionManager.Instance.GoToScene (0);
		}

		// Go to GameScene
		if (Input.GetKeyDown (KeyCode.P)) {
			AudioManager.Instance.musicSource.Stop ();
			AudioManager.Instance.musicSource.clip = AudioManager.Instance.music[1];
			AudioManager.Instance.musicSource.Play ();
			resetGame ();
			TransitionManager.Instance.GoToScene (1);
		}
	}

	private void resetGame () {
		TransitionManager.playerLives = 4;
		TransitionManager.flyingBossLives = 8;
	}
}