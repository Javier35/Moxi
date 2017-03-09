using UnityEngine;
using System.Collections;

public class PitEffects : MonoBehaviour {

	private LevelManager levelManager;
	private bool respawning = false;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
	}

	void OnTriggerEnter2D(Collider2D other){

		if (respawning == false) {
			respawning = true;

			if (other.tag == "Player") {
				StopPlayerFollows ();
				levelManager.fader.BeginFade (1);
				Invoke ("Respawn", 0.4f);
				Invoke ("ToggleRespawnFlag", 0.6f);
			} else {
				var destroyable = other.GetComponent<Destroyable> ();
				if (destroyable != null) {
					destroyable.DestroySelf ();
				}
			}
		}
	}

	void ToggleRespawnFlag(){
		respawning = !respawning;
	}

	void StopPlayerFollows(){
		var followingObjects = FindObjectsOfType<PlayerFollow> ();
		foreach(PlayerFollow pf in followingObjects){
			pf.StopFollowing ();
		}
	}

	void StartPlayerFollows(){
		var followingObjects = FindObjectsOfType<PlayerFollow> ();
		foreach(PlayerFollow pf in followingObjects){
			pf.StartFollowing ();
		}
	}

	void Respawn(){
		levelManager.RespawnPlayer ();
		StartPlayerFollows ();
	}
}
