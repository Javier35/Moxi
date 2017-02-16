using UnityEngine;
using System.Collections;

public class PitEffects : MonoBehaviour {

	private LevelManager levelManager;
	private GameObject cam;
	private bool respawning = false;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
		cam = GameObject.Find ("Main Camera");
	}

	void OnTriggerEnter2D(Collider2D other){

		if (respawning == false) {
			respawning = true;

			if (other.tag == "Player") {
				FreeCamera ();
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

	void LockCamera(){
		cam.GetComponent<Camera2DFollow> ().StartFollowing ();
	}

	void FreeCamera(){
		cam.GetComponent<Camera2DFollow> ().StopFollowing ();
		CameraShake.Shake (0.35f, 0.04f);
	}

	void Respawn(){
		levelManager.RespawnPlayer ();
		LockCamera ();
	}
}
