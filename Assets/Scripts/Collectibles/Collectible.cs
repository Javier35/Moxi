using UnityEngine;
using System.Collections;
//using UnityEngine.UI;

public class Collectible : MonoBehaviour {

	public LevelManager levelManager;
	public string collectibleType = "";
	private Collider2D thisCollider;
	protected bool respawnable = true;
	private bool bounced = false;

	public void SetLevelManager(){
		levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		thisCollider = gameObject.GetComponent<BoxCollider2D> ();
	}

	void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.tag != "Platform") {
			var otherCollider = col.gameObject.GetComponent<Collider2D> ();
			Physics2D.IgnoreCollision (otherCollider, thisCollider);
		} else {

			if (!bounced) {
				bounced = true;
				Vector2 contactPoint = col.contacts[0].normal;

				GetComponent<Rigidbody2D> ().AddForce (col.contacts[0].normal * 1.5f, ForceMode2D.Impulse );
			}
		}
	}

	public void setRespawnable(bool isRespawnable){
		respawnable = isRespawnable;
	}
}
