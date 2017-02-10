using UnityEngine;
using System.Collections;

public class LifeCollectible : Collectible {

	void Start(){
		collectibleType = "life";
		SetLevelManager ();
	}

	void OnTriggerEnter2D (Collider2D col){

		if (col.gameObject.tag == "Player") {
			//levelManager.GetComponent<Inventory> ().lives++;
			Inventory.GainALife();

			if(respawnable)
				levelManager.collectiblesToSpawn.Add (this.gameObject);

			gameObject.SetActive (false);
		}
	}
}
