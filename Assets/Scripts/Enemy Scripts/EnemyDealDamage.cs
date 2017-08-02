using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDealDamage : DealDamage {

	Animator anim;
	void Start () {
		anim = GetComponentInParent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(anim.GetCurrentAnimatorStateInfo(0).IsTag("Damage")){
			DisableDamaging();
		}else{
			EnableDamaging();
		}
	}
}
