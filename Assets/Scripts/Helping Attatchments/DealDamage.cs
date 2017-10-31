using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DealDamage : MonoBehaviour {


	[SerializeField]private string[] damageableByMe = new string[] {"Player"};
	public int damage = 1;
	public bool ableToDamage = true;

	void Awake(){
	}

	void OnTriggerStay2D(Collider2D col){
		if (damageableByMe.Contains(col.tag) && ableToDamage) {
			var damageManager = col.gameObject.GetComponentInParent<DamageManager> ();
			damageManager.ReceiveDamage (damage);
		}
	}

	public void EnableDamaging(){
		ableToDamage = true;
	}

	public void DisableDamaging(){
		ableToDamage = false;
	}
}
