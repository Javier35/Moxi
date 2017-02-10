using UnityEngine;
using System.Collections;
//using UnityEngine.UI;

public class Collectible : MonoBehaviour {

	public LevelManager levelManager;
	public string collectibleType = "";
	private Collider2D thisCollider;
	protected bool respawnable = true;

	public void SetLevelManager(){
		levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		thisCollider = gameObject.GetComponent<BoxCollider2D> ();
	}

	void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.tag != "Platform") {
			var otherCollider = col.gameObject.GetComponent<Collider2D> ();
			Physics2D.IgnoreCollision (otherCollider, thisCollider);
		}
	}

	public void setRespawnable(bool isRespawnable){
		respawnable = isRespawnable;
	}
}
