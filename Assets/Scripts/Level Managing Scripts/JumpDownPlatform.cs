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

	public override bool JumpEvent (GameObject gObject){

		if(Input.GetKey(KeyCode.DownArrow)){
			var characterComponent = gObject.GetComponent<PlatformerCharacter2D> ();
			characterComponent.DoFall ();
			var playerCollider = gObject.GetComponent<Collider2D> ();
			Physics2D.IgnoreCollision (playerCollider, thisCollider);
			passable = true;
		}
		return false;
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			var playerCollider = other.gameObject.GetComponent<Collider2D> ();
			Physics2D.IgnoreCollision (playerCollider, thisCollider, false);
			passable = false;
		}
	}
}