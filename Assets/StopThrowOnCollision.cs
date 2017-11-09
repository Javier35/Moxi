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


	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.layer == LayerMask.NameToLayer ("Platform")){
			if(pickable.beingThrown)
				pickable.beingThrown = false;
		}
	}
}
