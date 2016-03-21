using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	public LevelManager levelManager;

	public LifeManager lifeManager;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();

		lifeManager = FindObjectOfType<LifeManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Player") {
			levelManager.RespawnPlayer();
			lifeManager.takeLife();
		}
	}
}
