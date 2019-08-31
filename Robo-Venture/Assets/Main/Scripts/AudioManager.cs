using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

	public AudioSource musicSource;
	public AudioSource soundFxSource;

	public AudioClip[] music;
	public AudioClip[] soundFx;

	public static bool firstTime = true;

	private static AudioManager instance;
	public static AudioManager Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindObjectOfType<AudioManager> ();
			}
			return instance;
		}
	}

	protected virtual void Awake () {
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		}
		instance = this;

		if (TransitionManager.Instance.getCurrentScene () == 1) {
			musicSource.Stop ();
			musicSource.clip = music[1];
			musicSource.Play ();
		}
	}

	// Use this for initialization
	void Start () {
		musicSource.clip = music[0];
		musicSource.Play ();
		DontDestroyOnLoad (this);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return) && firstTime) {
			musicSource.Stop ();
			musicSource.clip = music[1];
			musicSource.Play ();
			firstTime = false;
		}
	}
}