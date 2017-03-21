using UnityEngine;
using System.Collections;

public class AttackTriggerManager : MonoBehaviour {

	private int damage;
	private BoxCollider2D attackBox;

	private ArrayList enemyRefs = new ArrayList();

	void Awake(){
		damage = gameObject.GetComponentInParent<PlayerDamageManager> ().damage;
		attackBox = gameObject.GetComponent<BoxCollider2D> ();
		attackBox.enabled = false;
	}

	void OnTriggerEnter2D(Collider2D col){
		

		var hitHandler = col.gameObject.GetComponent<HitHandler> ();
		if (hitHandler != null) {
			hitHandler.HitEvent (damage);
			CameraShake.Shake (0.1f, 0.015f);

			if (col.gameObject.tag == "Enemy") {
				enemyRefs.Add (col.gameObject.transform.parent.gameObject);
			}
		}

	}

	private void ShowHitEffect(){
		GetComponentInChildren<Animator> ().SetTrigger ("Hit1");
	}

	public void ResetEnemyDamage(){
		if (enemyRefs.Count > 0) {
			foreach(var item in enemyRefs)
			{
				var go = (GameObject)item;
				go.GetComponent<EnemyDamageManager> ().ResetDamage ();
			}
			enemyRefs.Clear ();
		}
	}
}
