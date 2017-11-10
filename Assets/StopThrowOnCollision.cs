using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopThrowOnCollision : MonoBehaviour {

	public Pickable pickable;

	void Start(){
		pickable = GetComponent<Pickable> ();
		if (pickable == null) {
			pickable = GetComponentInParent<Pickable> ();
		}
	}


	void OnCollisionStay2D(Collision2D col){
		if(pickable.beingThrown && col.gameObject.layer == LayerMask.NameToLayer ("Platform")){
			pickable.beingThrown = false;
		}
	}
}
