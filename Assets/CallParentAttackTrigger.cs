using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallParentAttackTrigger : MonoBehaviour {

	AttackTriggerManager parentAttackManager;
	void Start () {
		parentAttackManager = GetComponentInParent<AttackTriggerManager> ();
	}

	void OnTriggerEnter2D(Collider2D col){
		parentAttackManager.ParentTriggerEnter (col);
	}
}
