using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_basic : Movable {

	public float moveSpeed = 0f;

	private Transform WallCheck;
	private Transform FrontGroundCheck;

	[SerializeField] private LayerMask WhatIsPlatform;

	void Start () {
		WallCheck = transform.Find("WallCheck");
		FrontGroundCheck = transform.Find("FrontGroundCheck");
	}

	bool lastWasDamage = false;
	// Update is called once per frame
	void Update () {
		if (checkIfActive()) {

			if (!animator.GetCurrentAnimatorStateInfo (0).IsTag ("Damage")) {
				lastWasDamage = false;
				if (!CheckFrontGround () || CheckWallCollision ())
					Flip ();
				Move ();
			} else if(!lastWasDamage && animator.GetCurrentAnimatorStateInfo (0).IsName ("Damage")){
				lastWasDamage = true;
				rbody.velocity = Vector3.zero;
			}

		}
	}

	private bool CheckWallCollision (){
		Collider2D[] colliders = Physics2D.OverlapCircleAll(WallCheck.position, 0.1f, WhatIsPlatform);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject){
				return true;
			}
		}
		return false;
	}

	private bool CheckFrontGround (){
		
		Collider2D[] colliders = Physics2D.OverlapCircleAll(FrontGroundCheck.position, 0.1f, WhatIsPlatform);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject.layer == 8){
				//Flip ();
				return true;
			}
		}
		return false;
	}

	private void Move(){
		if (faceLeft) {
			rbody.velocity = new Vector2 (-moveSpeed, rbody.velocity.y);
		} else
			rbody.velocity = new Vector2 (moveSpeed, rbody.velocity.y);
	}
}
