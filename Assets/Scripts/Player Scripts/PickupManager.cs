using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour {

	private Transform enemyHolder;

	// Use this for initialization
	void Start () {
		enemyHolder = this.gameObject.transform.parent.Find("EnemyHolder");
	}
	
	void OnTriggerStay2D(Collider2D col){

		if (enemyHolder.childCount == 0) {
			var pickable = col.gameObject.GetComponent<Pickable> ();

			if(pickable == null)
				pickable = col.gameObject.GetComponentInParent<Pickable> ();

			if (pickable != null) {
				if (pickable.IsPickable ()) {

					var projectieReference = Instantiate(pickable.GetProjectile(), enemyHolder, false);
//					Debug.Log (enemyHolder.position);
//					Debug.Break ();
					var grabPoint = pickable.GetGrabPoint();
					projectieReference.transform.localPosition = new Vector3 (-grabPoint.x, -grabPoint.y, 0);
					pickable.DestroySelf ();
				}
			}
		}
	}
}

