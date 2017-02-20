using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	protected Rigidbody2D rbody;
	protected Animator animator;
	protected GameObject player;

	[SerializeField] protected bool faceLeft = true;

	void Awake () {
		player = GameObject.Find ("Moxi");
		rbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();

		if (!faceLeft) {
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}

	}

	protected void Flip () {
		
		faceLeft = !faceLeft;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	protected void FacePlayer(){
		if (player.transform.position.x < this.transform.position.x && !faceLeft) {
			Flip ();
		}else if(player.transform.position.x > this.transform.position.x && faceLeft){
			Flip ();
		}
	}
}
