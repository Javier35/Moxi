using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableEnemy : Pickable {


	Animator anim;
	EnemyDamageManager enemyDamageManager;

	private Transform playerEnemyHolder;

	void Start () {
		anim = GetComponent<Animator> ();
		enemyDamageManager = GetComponent<EnemyDamageManager> ();
	}

	void Update(){
		
		if (transform.root.name == "Moxi" || beingThrown == true) {
			enemyDamageManager.ResetStunTime ();
			enemyDamageManager.SetInvincible (true);
		} else {
			enemyDamageManager.SetInvincible (false);
		}
			
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Damage") && (!beingThrown)) {
			SetPickable (true);
		} else {
			SetPickable (false);
		}
	}
}
