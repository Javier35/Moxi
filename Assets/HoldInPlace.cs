using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldInPlace : MonoBehaviour {

	void Update () {

		if (this.transform.childCount > 0) {
			var heldProjectile = this.transform.GetChild (0);
			var heldRbody = heldProjectile.GetComponent<Rigidbody2D> ();
			var grabPoint = heldProjectile.transform.Find ("GrabPoint");

//			heldRbody.MovePosition ();

			heldProjectile.transform.localPosition = new Vector3 (-grabPoint.localPosition.x, -grabPoint.localPosition.y, 0);
		
		
		}
	}
}
