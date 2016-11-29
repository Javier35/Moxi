using UnityEngine;
using System.Collections;

public class MoneyCollectible : Collectible {
	
	[SerializeField] private double moneyAmmount = 100;

	void Start(){
		collectibleType = "money";
		SetLevelManager ();
	}

	void OnTriggerEnter2D (Collider2D col){

		if (col.gameObject.tag == "Player") {
			Inventory.GainMoney(moneyAmmount);
			//levelManager.collectiblesToSpawn.Add (this.gameObject);
			gameObject.SetActive (false);
		}
	}
}
