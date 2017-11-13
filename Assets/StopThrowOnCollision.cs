using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopThrowOnCollision : MonoBehaviour {

	public Pickable pickable;
	Rigidbody2D rbody;

	void Start(){

		rbody = GetComponent<Rigidbody2D> ();
		if (rbody == null) {
			rbody = GetComponentInParent<Rigidbody2D> ();
		}

		pickable = GetComponent<Pickable> ();
		if (pickable == null) {
			pickable = GetComponentInParent<Pickable> ();
		}
	}


	void OnCollisionStay2D(Collision2D col){
		if(pickable.beingThrown && col.gameObject.layer == LayerMask.NameToLayer ("Platform")){
			pickable.beingThrown = false;
//			rbody.velocity = Vector3.zero;
		}
	}
}
