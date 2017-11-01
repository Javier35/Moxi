using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour {

	private Transform enemyHolder;
	private Animator anim;

	// Use this for initialization
	void Start () {
		enemyHolder = this.gameObject.transform.parent.Find("EnemyHolder");
		anim = GetComponentInParent<Animator> ();
	}

	void OnTriggerStay2D(Collider2D col){

		if (enemyHolder.childCount == 0 && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !anim.GetCurrentAnimatorStateInfo (0).IsTag ("Damage")) {
			var pickable = col.gameObject.GetComponent<Pickable> ();

			if (pickable != null) {
				if (pickable.IsPickable ()) {

					var projectieReference = pickable.BecomeHeld();
					projectieReference.transform.parent = enemyHolder;

					var grabPoint = pickable.GetGrabPoint();
					projectieReference.transform.localPosition = new Vector3 (-grabPoint.x, -grabPoint.y, 0);
				}
			}
		}
	}
}

