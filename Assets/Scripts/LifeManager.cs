using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour {

	public int startingLives;
	private int lifeCounter;

	private Text theText;

	public GameObject gameOverScreen;

	public PlayerController player;

	public string mainMenu;

	public float waitAfterGameOver;

	// Use this for initialization
	void Start () {
		theText = GetComponent<Text> ();

		lifeCounter = startingLives;

		player = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (lifeCounter < 0) {
			gameOverScreen.SetActive(true);
			player.gameObject.SetActive(false);
						
			if (Input.GetButton("Jump")) {
				Application.LoadLevel(mainMenu);
			}
		}

		theText.text = "x " + lifeCounter;

		if (gameOverScreen.activeSelf) {
			waitAfterGameOver -= Time.deltaTime;
		}

		if (waitAfterGameOver < 0) {
			Application.LoadLevel(mainMenu);
		}
	}

	public void giveLife() {
		lifeCounter++;
	}

	public void takeLife() {
		lifeCounter--;
	}
}
