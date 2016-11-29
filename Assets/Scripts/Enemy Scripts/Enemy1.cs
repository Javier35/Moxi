using UnityEngine;
using System.Collections;

public class Enemy1 : MonoBehaviour {

	public float moveSpeed = 0f;
	public bool moveLeft = true;

	private Rigidbody2D rbody;
	private Animator animator;
	private Transform WallCheck;
	private Transform FrontGroundCheck;

	[SerializeField] private LayerMask WhatIsPlatform;

	void Start () {
		WallCheck = transform.Find("WallCheck");
		FrontGroundCheck = transform.Find("FrontGroundCheck");
		rbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {

		if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("Damage") &&
		   !animator.GetCurrentAnimatorStateInfo (0).IsName ("Death")) {
			CheckWallCollision ();
			if (CheckFrontGround () == false)
				Flip ();
			Move ();
		} else {
			
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Damage")) {
				rbody.velocity = new Vector2 (0, rbody.velocity.y);
			}
		}
	}

	private void CheckWallCollision (){
		Collider2D[] colliders = Physics2D.OverlapCircleAll(WallCheck.position, 0.1f, WhatIsPlatform);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject){
				Flip ();
			}
		}
	}

	private bool CheckFrontGround (){

		bool found = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(FrontGroundCheck.position, 0.1f, WhatIsPlatform);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject.tag == "Platform"){
				//Flip ();
				found = true;
				break;
			}
		}
		return found;
	}

	private void Move(){
		if (moveLeft) {
			rbody.velocity = new Vector2 (-moveSpeed, rbody.velocity.y);
		} else
			rbody.velocity = new Vector2 (moveSpeed, rbody.velocity.y);
	}
	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		moveLeft = !moveLeft;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
		
}