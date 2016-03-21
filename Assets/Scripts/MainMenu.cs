using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public string startLevel;

	public string levelSelect;

	public int playerLives;

	public void NewGame() {
		Application.LoadLevel (startLevel);

		PlayerPrefs.SetInt ("PlayerCurrentLives", playerLives);
	}

	public void LevelSelect() {
		PlayerPrefs.SetInt ("PlayerCurrentLives", playerLives);
		Application.LoadLevel (levelSelect);
	}

	public void QuitGame() {
		//Debug.Log ("quit the game");
		Application.Quit ();
	}
}