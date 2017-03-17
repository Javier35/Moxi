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

		var enemyScript = gameObject.GetComponent<Enemy> ();
		enemyScript.faceLeft = enemyScript.originalFaceLeft;

		gameObject.GetComponent<EnemyDamageManager> ().ResetVariables ();
		gameObject.transform.position = spawnPosition;
		gameObject.SetActive(true);
		gameObject.GetComponent<Destroyable> ().stopFlicker ();
		//Debug.Log (transform.position);
	}
}
