using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour {

	protected bool pickable = false;
	private Transform grabPoint;
	Rigidbody2D rbody;

	void Awake(){
		grabPoint = transform.root.Find ("GrabPoint");
		rbody = transform.root.gameObject.GetComponent<Rigidbody2D> ();
	}

	public bool IsPickable(){
		return pickable;
	}

	public void SetPickable(bool pickable){
		this.pickable = pickable;
	}

	public virtual GameObject BecomeHeld(){

//		this.transform.root.position = Vector3.zero;
		rbody.isKinematic = false;
		return this.transform.root.gameObject;
	}

	public Vector3 GetGrabPoint(){

		if (grabPoint == null) {
			return Vector3.zero;
		}
		return grabPoint.localPosition;
	}
}

