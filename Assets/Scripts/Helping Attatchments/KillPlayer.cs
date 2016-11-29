using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	private LevelManager levelManager;
	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			levelManager.RespawnPlayer ();
		}
	}
}
