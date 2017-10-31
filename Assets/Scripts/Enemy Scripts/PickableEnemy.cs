using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableEnemy : Pickable {


	//this script must be accompanied by a "Receive Damage" script
	DealDamage siblingDamageObject;
	EnemyDamageManager damageManager;

	private Transform playerEnemyHolder;

	void Start () {
		damageManager = GetComponentInParent<EnemyDamageManager> ();
		siblingDamageObject = this.transform.parent.GetComponentInChildren<EnemyDealDamage> ();
	}

	void Update(){
		if (siblingDamageObject.ableToDamage)
			SetPickable(false);
		else
			SetPickable(true);
	}

	override public void DestroySelf(){
		damageManager.InstantDestroy ();
	}
}
