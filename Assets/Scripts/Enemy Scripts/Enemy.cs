using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	protected Rigidbody2D rbody;
	protected Animator animator;
	protected GameObject player;

	protected bool visible = false;
	protected bool near = false;

	public string activationCondition = "visible";

	[SerializeField] public bool faceLeft = true;
	[HideInInspector]public bool originalFaceLeft;

	void Awake () {
		player = GameObject.Find ("Moxi");
		rbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();

		if (!faceLeft) {
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
		originalFaceLeft = faceLeft;

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

	void OnBecameVisible() {
		visible = true;
	}

	void OnBecomeInvisible() {
		visible = false;
	}

	public void SetIsNear(bool isNear){
		near = isNear;
	}

	protected bool checkIfActive(){
		if (activationCondition == "visible" && visible ||
		    activationCondition == "proximity" && near ||
		    activationCondition == "always") {
			return true;
		}
		return false;
	}
}
