using UnityEngine;
using System.Collections;

public class CollisionManager : MonoBehaviour {

	private Collider2D thisCollider;
	[SerializeField]private string[] ignoreCollisionsWith = {"Enemy"};
	// Use this for initialization
	void Start () {
		thisCollider = gameObject.GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D col){
		//if col's tag is in ignoreCollisionsWith, then ignore the collisions
		if (System.Array.IndexOf(ignoreCollisionsWith, col.gameObject.tag) != -1) {
			
			var otherCollider = col.gameObject.GetComponent<Collider2D> ();
			Physics2D.IgnoreCollision (otherCollider, thisCollider);
		}
	}
}
