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

		if(Input.GetKey(KeyCode.DownArrow)){
			var characterComponent = gObject.GetComponent<PlatformerCharacter2D> ();
			var playerCollider = gObject.GetComponent<Collider2D> ();
			Physics2D.IgnoreCollision (playerCollider, thisCollider);
			characterComponent.DoFall ();
			passable = true;
			StartCoroutine (BecomeSolid(playerCollider));
		}
	}

	private IEnumerator BecomeSolid(Collider2D col){
		yield return new WaitForSeconds (1f);
		Physics2D.IgnoreCollision (col, thisCollider, false);
		passable = false;
	}
}