using UnityEngine;
using System.Collections;

public class EnemyRespawn : MonoBehaviour {


	private Vector3 spawnPosition;

	void Awake(){
		spawnPosition = gameObject.transform.position;
		//Debug.Log (spawnPosition);
	}

	void Die(){
		gameObject.SetActive (false);
	}

	public void Respawn(){
		gameObject.SetActive(true);
		gameObject.GetComponent<EnemyDamageManager> ().ResetVariables ();
		gameObject.transform.position = spawnPosition;
		gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		//Debug.Log (transform.position);
	}
}
