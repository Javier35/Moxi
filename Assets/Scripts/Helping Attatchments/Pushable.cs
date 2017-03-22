using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : Movable {

	PlatformerCharacter2D playerBehaviorReference;

	void Start(){
		playerBehaviorReference = GameObject.Find ("Moxi").GetComponent<PlatformerCharacter2D> ();
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player") {

			ContactPoint2D contact = collision.contacts [0];

			if (playerBehaviorReference.m_Grounded) {
				if (Vector3.Dot (contact.normal, Vector3.left) > 0.1 && !playerBehaviorReference.m_FacingRight) {

					var newSpeed = playerBehaviorReference.GetOriginalMaxSpeed() * 0.3f;
					playerBehaviorReference.SetMaxSpeed (newSpeed);
					rbody.velocity = new Vector2 (-newSpeed, rbody.velocity.y);

				}else if(Vector3.Dot (contact.normal, Vector3.right) > 0.1 && playerBehaviorReference.m_FacingRight){
					var newSpeed = playerBehaviorReference.GetOriginalMaxSpeed() * 0.3f;
					playerBehaviorReference.SetMaxSpeed (newSpeed);
					rbody.velocity = new Vector2 (newSpeed, rbody.velocity.y);
				}
			}
		}
	}

	void OnCollisionExit2D(Collision2D collision){
		if (collision.gameObject.tag == "Player") {
			playerBehaviorReference.RestoreMaxSpeed ();
			rbody.velocity = new Vector2 (0, rbody.velocity.y);
		}
	}
}
