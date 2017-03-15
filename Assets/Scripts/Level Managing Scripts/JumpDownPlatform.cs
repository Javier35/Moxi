using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDownPlatform : SpecialTerrain {

	private Collider2D thisCollider;
	public bool passable = false;

	void Start () {
		thisCollider = gameObject.GetComponent<BoxCollider2D> ();
	}

	public override void StandEvent (GameObject gObject){}

	public override void JumpEvent (GameObject gObject){
		gObject.GetComponent<PlatformerCharacter2D> ().m_JumpForce = 0;
		var playerCollider = gObject.GetComponent<Collider2D> ();
		Physics2D.IgnoreCollision (playerCollider, thisCollider);
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag == "player"){
			var playerCollider = other.gameObject.GetComponent<Collider2D> ();
			Physics2D.IgnoreCollision (playerCollider, thisCollider, false);
		}
	}
}