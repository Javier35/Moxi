using UnityEngine;
using System.Collections;
//using UnityEngine.UI;

public class Collectible : Destroyable {

	public LevelManager levelManager;
	public string collectibleType = "";
	private Collider2D thisCollider;
	protected bool spawned = false;
	private bool bounced = false;

	public void SetLevelManager(){
		levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		thisCollider = gameObject.GetComponent<BoxCollider2D> ();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
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

	public void setSpawned(bool isSpawned){
		spawned = isSpawned;
	}

	override public void DestroySelf(){
		if (!spawned) {
			levelManager.collectiblesToSpawn.Add (this.gameObject);
			gameObject.SetActive (false);
		} else {
			Destroy (gameObject);
		}
	}

	public void startSpawnedBehavior(){
		StartCoroutine (configureSpawnedBehavior());
	}

	public IEnumerator configureSpawnedBehavior(){

		setSpawned(true);
		GetComponents<BoxCollider2D> ()[0].enabled = true;
		GetComponent<Float> ().enabled = false;
		GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;

		yield return new WaitForSeconds (3.5f);
		StartCoroutine (Flicker (2.2f));
		yield return new WaitForSeconds (2.2f);
		DestroySelf ();
	}
}
