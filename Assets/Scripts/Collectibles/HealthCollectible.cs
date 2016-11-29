using UnityEngine;
using System.Collections;

public class HealthCollectible : Collectible {
	[SerializeField]private int healAmmount = 1;

	void Start(){
		collectibleType = "health";
		SetLevelManager ();
	}

	void OnTriggerEnter2D (Collider2D col){
		
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<PlayerDamageManager> ().Heal (healAmmount);
			levelManager.collectiblesToSpawn.Add (this.gameObject);
			gameObject.SetActive (false);
		}
	}
}
