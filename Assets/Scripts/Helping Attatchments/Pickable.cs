using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour {

	protected bool pickable = false;
	private Transform grabPoint;
	protected DamageManager damageManager;
	private int throwDamage = 5;
	Rigidbody2D rbody;

	public BoxCollider2D overlappingTerrainChecker;
	[SerializeField] private LayerMask WhatIsPlatform;

	[HideInInspector]public bool beingThrown = false;
	[HideInInspector]public bool isHardThrown = false;


	void Awake(){
		grabPoint = transform.Find ("GrabPoint");
		rbody = GetComponent<Rigidbody2D> ();
		damageManager = GetComponent<DamageManager> ();
	}

	public bool IsPickable(){
		return pickable;
	}

	public void SetPickable(bool pickable){
		this.pickable = pickable;
	}

	public virtual GameObject BecomeHeld(){
		rbody.isKinematic = false;

		return this.transform.root.gameObject;
	}

	public void BecomeThrown(float throwStrength, bool right, bool hardThrow){
		rbody.isKinematic = false;
		rbody.velocity = Vector2.zero;

		if (checkOverlapping ()) {
			damageManager.DestroySelf ("throw");
		} else {
			beingThrown = true;
			SetPickable (false);
			isHardThrown = hardThrow;

			if (right)
				rbody.AddForce (Vector2.right * throwStrength);
			else
				rbody.AddForce (Vector2.left * throwStrength);
		}
	}

	public void BecomeDropped(){
		rbody.isKinematic = false;
		rbody.velocity = Vector2.zero;

		if (checkOverlapping ()) {
			damageManager.DestroySelf ("throw");
		}
	}

	public void CollisionBehavior(){
		beingThrown = false;
		isHardThrown = false;
	}

	public Vector3 GetGrabPoint(){

		if (grabPoint == null) {
			return Vector3.zero;
		}
		return grabPoint.localPosition;
	}

	void OnTriggerStay2D(Collider2D col){
//		Debug.Log (col.transform.root.name);
		if (beingThrown) {
			if (col.tag == "Enemy" ||  col.tag == "Interactable") {
				beingThrown = false;

				var targetDamageManager = col.gameObject.GetComponent<DamageManager> ();
				if(targetDamageManager == null)
					targetDamageManager = col.gameObject.GetComponentInParent<DamageManager> ();

				targetDamageManager.ReceiveDamage (throwDamage);
				damageManager.DestroySelf ("throw");
			}
		}
	}

	public bool checkOverlapping(){

		Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.root.position, 0.32f, WhatIsPlatform);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject){
				return true;
			}
		}
		return false;
	}

}

