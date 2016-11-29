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

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Enemy") {
			col.gameObject.GetComponent<EnemyDamageManager> ().ReceiveDamage (damage);
			enemyRefs.Add (col.gameObject);
		
			ShowHitEffect();
			CameraShake.Shake (0.2f, 0.025f);
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
