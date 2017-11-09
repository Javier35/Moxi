using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackTriggerManager : MonoBehaviour {

	private List<int> objectsHit = new List<int>();
	private Animator anim;
	int damageStrength = 1;



	void Awake(){
		
		anim = gameObject.GetComponentInParent<Animator> ();
	}

	void Update (){
		if(! anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")){
			ClearHittedList();
		}

		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			damageStrength = 1;
		} else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("SecondAttack")) {
			damageStrength = 5;
		}else if(anim.GetCurrentAnimatorStateInfo (0).IsName ("ThirdAttack")) {
			damageStrength = 5;
		}
	}

	public void ParentTriggerEnter(Collider2D col){

		var hitHandler = col.gameObject.GetComponent<HitHandler> ();
		if(hitHandler == null)
			hitHandler = col.gameObject.GetComponentInParent<HitHandler> ();
		
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
