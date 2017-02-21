﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Throwing : Enemy {

	[SerializeField]private GameObject molotov;
	public float moveSpeed = 2.4f;
	bool canThrow = true;
	float distance;
	bool moveLeft = true;

	void Start(){
		InvokeRepeating ("ToggleDirection", 0.8f, 0.8f);
	}

	void Update(){

		FacePlayer ();

		if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("Damage") &&
			!animator.GetCurrentAnimatorStateInfo (0).IsName ("Death")) {

			Move ();

			distance = Mathf.Abs(this.transform.position.x  - player.transform.position.x);
			if(canThrow && distance <= 3.4f){

				canThrow = false;
				ThrowProjectile ();

				Invoke ("ResetThrow", 1.4f);
			}
		}else if(animator.GetCurrentAnimatorStateInfo (0).IsName ("Damage")){
			rbody.velocity = new Vector2 (0, rbody.velocity.y);
		}
	}

	void ThrowProjectile(){

		GameObject spawnedProjectile = (GameObject)Instantiate (molotov, this.transform.position, this.transform.rotation);
		var projectileBody = spawnedProjectile.GetComponent<Rigidbody2D> ();

		distance *= 10;
		float totalTime = 2f;


		if (faceLeft) {
			projectileBody.AddForce (new Vector2 ( (-1 * distance)/totalTime , (20f*totalTime) ));
		} else {
			projectileBody.AddForce (new Vector2 ( ( 1 * distance)/totalTime , (20f*totalTime) ));
		}
	}

	void ResetThrow(){
		canThrow = true;
	}

	void ToggleDirection(){
		moveLeft = !moveLeft;
	}

	private void Move(){
		if (moveLeft) {
			rbody.velocity = new Vector2 (-moveSpeed, rbody.velocity.y);
		} else {
			rbody.velocity = new Vector2 (moveSpeed, rbody.velocity.y);
		}
	}
}