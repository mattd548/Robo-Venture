using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour {

	private int currentScene = 0;
	private int MAX_SCENES = 10; // Scenes from 0 to MAX_SCENES - 1

	public static bool goingBack = false;
	public static bool hurtPlayer = false;
	static public GameObject initialSpawner;

	// public GameObject mainPlayer;
	// private static GameObject playerInstance;
	// public static GameObject PlayerInstance {
	// 	get {
	// 		if (playerInstance == null) {
	// 			playerInstance = (GameObject)mainPlayer;
	// 		}
	// 		return playerInstance;
	// 	}
	// }

	public static GameObject player;
	public GameObject robot;
	public static GameObject canvas;
	public static GameObject eventSystem;

	// Initialize game variables will go here ...
	public static int playerLives = 4;
	public static int flyingBossLives = 8;

	private static TransitionManager instance;
	public static TransitionManager Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindObjectOfType<TransitionManager> ();
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

		// if (playerInstance != null && playerInstance != mainPlayer) {
		// 	Destroy (mainPlayer.gameObject);
		// 	return;
		// }
		// playerInstance = mainPlayer;
		// initialSpawner = GameObject.Find ("initialSpawn");
	}

	// Use this for initialization
	void Start () {
		initialSpawner = GameObject.Find ("initialSpawn");
		// player = GameObject.Find ("Player");
		player = (GameObject) Instantiate (robot, initialSpawner.transform.position, initialSpawner.transform.rotation);
		canvas = GameObject.Find ("Canvas");
		eventSystem = GameObject.Find ("EventSystem");
		DontDestroyOnLoad (this);
		DontDestroyOnLoad (player);
		DontDestroyOnLoad (canvas);
		DontDestroyOnLoad (eventSystem);
		DontDestroyOnLoad (initialSpawner);
	}

	// Update is called once per frame
	void Update () {
		if (currentScene == 0) {
			if (Input.GetKeyDown (KeyCode.Return)) {
				updateScene ();
			} else if (Input.GetKeyDown (KeyCode.F)) {
				GoToScene (MAX_SCENES - 3);
			}
		}

		if (currentScene == 0) {
			player.transform.position = initialSpawner.transform.position;
		}
	}

	public void updateScene () {
		// Debug.Log ("Transition: Changing scene from " + currentScene + " to " + (currentScene + 1));
		++currentScene;
		if (currentScene == MAX_SCENES) {
			currentScene = 0;
		}
		SceneManager.LoadScene (currentScene);
	}

	public void NextScene () {
		++currentScene;
		if (currentScene == MAX_SCENES) {
			currentScene = 0;
		}
		SceneManager.LoadScene (currentScene);
	}

	public void PreviousScene () {
		--currentScene;
		if (currentScene <= 1) {
			currentScene = 1;
		}
		SceneManager.LoadScene (currentScene);
	}

	public void GoToScene (int scene) {
		currentScene = scene;
		if (currentScene < 0) {
			currentScene = 0;
		}
		SceneManager.LoadScene (currentScene);
	}

	public int getCurrentScene () {
		return currentScene;
	}
}