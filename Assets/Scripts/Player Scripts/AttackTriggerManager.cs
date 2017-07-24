using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackTriggerManager : MonoBehaviour {

	private BoxCollider2D attackBox;
	private List<int> objectsHit = new List<int>();
	[SerializeField] int damageStrength = 1;



	void Awake(){
		attackBox = gameObject.GetComponent<BoxCollider2D> ();
		attackBox.enabled = false;
	}

	void Update (){
		if(attackBox.enabled == false){
			ClearHittedList();
		}
	}

	void OnTriggerEnter2D(Collider2D col){

		var hitHandler = col.gameObject.GetComponent<HitHandler> ();
		if (hitHandler != null) {

			int hittedObjectId = col.gameObject.GetInstanceID ();

			if (!objectsHit.Contains (hittedObjectId)) {
				hitHandler.HitEvent (damageStrength);
				objectsHit.Add (hittedObjectId);
			}
		}

	}

	private void ShowHitEffect(){
		GetComponentInChildren<Animator> ().SetTrigger ("Hit1");
	}

	public void ClearHittedList (){
		objectsHit.Clear ();
	}
}
