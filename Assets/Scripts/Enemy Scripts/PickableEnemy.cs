using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableEnemy : Pickable {


	DealDamage siblingDamageObject;
	EnemyDamageManager damageManager;

	private Transform playerEnemyHolder;

	void Start () {
		siblingDamageObject = this.GetComponent<EnemyDealDamage> ();
		damageManager = GetComponentInParent<EnemyDamageManager> ();
	}

	void Update(){

		if (transform.root.name == "Moxi") {
			damageManager.ResetStunTime ();
		} else {
			if (siblingDamageObject.ableToDamage)
				SetPickable(false);
			else
				SetPickable(true);
		}
	}
}
